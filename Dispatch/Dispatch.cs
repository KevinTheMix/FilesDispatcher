using System.Collections.Concurrent;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace Dispatch
{
    public partial class Dispatch : Form
    {
        #region Variables
        private const string StatsFolder = "- Dispatch Stats";
        private const string ExplorerProcessFileName = "explorer";
        private const string OriginalCountFileName = "original_count.txt";
        private const int MaxRetriesCount = 10;
        private const int MaxSkipCount = 40;
        private const int TreatmentThrottleMilliseconds = 800;
        private BlockingCollection<FileMove> filesToMove = new BlockingCollection<FileMove>(1);
        private Timer buttonsReenablingTimer = new Timer() { Interval = TreatmentThrottleMilliseconds };
        private ProcessStartInfo processStartInfo;
        private Process? process;
        private Dictionary<DateTime, MoveCount>? counts;
        private DateTime today;
        private string? lastFilePath;
        private string? countsFilePath;
        private int originalFilesCount;
        private int inFolderCount;
        private int skippedCount ;
        private MoveCount sessionCount;
        private MoveCount weekCount;
        private MoveCount monthCount;
        private MoveCount yearCount;
        #endregion

        #region Constructor
        public Dispatch()
        {
            InitializeComponent();

            this.today = DateTime.Today;    // So the whole program agrees on what the current day is.
            this.sessionCount = new MoveCount();
            this.weekCount = new MoveCount();
            this.monthCount = new MoveCount();
            this.yearCount = new MoveCount();

            this.processStartInfo = new ProcessStartInfo();
            this.processStartInfo.FileName = ExplorerProcessFileName;
            this.processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            this.processStartInfo.CreateNoWindow = true;
            this.buttonsReenablingTimer.Tick += ButtonsReenablingTimer_Tick;

            // Optional.
            SetInDirectory(@"V:\@ Sort\# Musique");
            SetOutFolder(@"V:\@ Sort\@ Very Good");

            Task.Run(() => ConsumeFile());
        }
        private void Dispatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveStats();
        }
        #endregion

        #region Methods
        private void SaveStats()
        {
            if (String.IsNullOrEmpty(this.countsFilePath) || this.counts == null)
            {
                return;
            }

            using (StreamWriter countsFile = new StreamWriter(this.countsFilePath, false))
            {
                foreach (var kvp in this.counts)
                {
                    countsFile.WriteLine($"{kvp.Key.ToString("MM.dd")}\t{kvp.Value.KeptCount}\t{kvp.Value.DeletedCount}");
                }
            }
        }
        private void ButtonsReenablingTimer_Tick(object? sender, EventArgs e)
        {
            this.buttonsReenablingTimer.Stop();
            EnableButtons();
        }
        private void InitializeCounts(string inDirectory)
        {
            string statsDirectory = Path.Combine(inDirectory, StatsFolder);
            if (!Directory.Exists(statsDirectory))
            {
                Directory.CreateDirectory(statsDirectory);
            }

            this.originalFilesCount = ReadOriginalCount(inDirectory, statsDirectory);
            this.inFolderCount = Directory.EnumerateFiles(inDirectory, "*.*", SearchOption.TopDirectoryOnly).Count();
            this.sessionCount.Reset();
            this.skippedCount = 0;

            this.countsFilePath = Path.Combine(statsDirectory, $"{this.today.Year}.txt");
            if (File.Exists(countsFilePath))
            {
                this.counts = ParseCounts(File.ReadAllLines(countsFilePath));
                if (!counts.ContainsKey(today))
                {
                    this.counts.Add(today, new MoveCount());
                }

                // From last Monday.
                int numberOfDaysFromMonday = CalculatePositiveModulo(today.DayOfWeek - DayOfWeek.Monday, 7);
                DateTime lastMonday = today.AddDays(-numberOfDaysFromMonday);
                this.weekCount = this.counts.Where(kvp => kvp.Key >= lastMonday).Sum();
                // Last 7 days.
                //this.weekCount = this.counts.Where(kvp => kvp.Key >= this.today.AddDays(-6)).Sum();
                this.monthCount = this.counts.Where(kvp => kvp.Key.Month == today.Month).Sum();
                this.yearCount = this.counts.Sum();
            }
            else
            {
                this.counts = new Dictionary<DateTime, MoveCount> { { this.today, new MoveCount { KeptCount = 0, DeletedCount = 0 } } };
                this.weekCount.Reset();
                this.monthCount.Reset();
                this.yearCount.Reset();
            }

            RefreshCountLabels();
        }
        private int ReadOriginalCount(string inDirectory, string statsDirectory)
        {
            string originalCountFilePath = Path.Combine(statsDirectory, OriginalCountFileName);
            if (File.Exists(originalCountFilePath))
            {
                return Int32.Parse(File.ReadAllText(originalCountFilePath));
            }
            else
            {
                int originalCount = Directory.EnumerateFiles(inDirectory, "*.*", SearchOption.AllDirectories).Count();
                File.WriteAllText(originalCountFilePath, originalCount.ToString());
                return originalCount;
            }
        }
        private int CalculatePositiveModulo(int i, int n) => (i % n + n) % n;
        private Dictionary<DateTime, MoveCount> ParseCounts(string[] countLines)
        {
            var countsByDay = new Dictionary<DateTime, MoveCount>();
            foreach (string countLine in countLines)
            {
                if (String.IsNullOrEmpty(countLine))
                {
                    continue;
                }

                string[] lineFrags = countLine.Split("\t");
                if (lineFrags.Length != 3)
                {
                    continue;
                }

                DateTime date = DateTime.ParseExact($"{today.Year}.{lineFrags[0]}", "yyyy.MM.dd", System.Globalization.CultureInfo.CurrentCulture);
                int keptCount = Int32.Parse(lineFrags[1]);
                int deletedCount = Int32.Parse(lineFrags[2]);
                countsByDay.Add(date, new MoveCount { KeptCount = keptCount, DeletedCount = deletedCount });
            }
            return countsByDay;
        }
        private void IncrementCounts(MoveAction action)
        {
            this.sessionCount.Increment(action);
            this.counts[this.today].Increment(action);
            this.weekCount.Increment(action);
            this.monthCount.Increment(action);
            this.yearCount.Increment(action);
            this.Invoke(() => RefreshCountLabels());
        }
        private void RefreshCountLabels()
        {
            this.lblSessionCount.Text = $"{this.sessionCount} = {String.Format("{0:0.00}%", 100.0 * this.sessionCount.Count / this.originalFilesCount)}";
            this.lblTodayCount.Text = $"{this.counts[this.today]} = {String.Format("{0:0.00}%", 100.0 * this.counts[this.today].Count / this.originalFilesCount)}";
            this.lblWeekCount.Text = $"{this.weekCount} = {String.Format("{0:0.00}%", 100.0 * this.weekCount.Count / this.originalFilesCount)}";
            this.lblMonthCount.Text = $"{this.monthCount} = {String.Format("{0:0.00}%", 100.0 * this.monthCount.Count / this.originalFilesCount)}";
            this.lblYearCount.Text = $"{this.yearCount} = {String.Format("{0:0.00}%", 100.0 * this.yearCount.Count / this.originalFilesCount)}";
            //this.Text = $"Dispatch ({this.originalFilesCount - this.sessionCount.Count}/{this.originalFilesCount})";
            this.Text = $"Dispatch ({this.inFolderCount - this.sessionCount.Count}/{this.originalFilesCount})";
        }
        private void SetInDirectory(string inDirectory)
        {
            this.inFolderBrowser.SelectedPath = inDirectory;
            this.tbxInFolder.Text = inDirectory;

            SaveStats();

            InitializeCounts(inDirectory);
            this.btnRun.Text = $"Start";

            if (AreAllSet())
            {
                this.btnRun.Enabled = true;
            }
        }
        private void SetOutFolder(string outFolder)
        {
            this.outFolderBrowser.SelectedPath = outFolder;
            this.tbxOutFolder.Text = outFolder;

            if (AreAllSet())
            {
                this.btnRun.Enabled = true;
            }
        }
        private bool AreAllSet()
        {
            return !String.IsNullOrEmpty(this.inFolderBrowser.SelectedPath) && !String.IsNullOrEmpty(this.outFolderBrowser.SelectedPath);
        }
        private void btnBrowseInFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = this.inFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetInDirectory(this.inFolderBrowser.SelectedPath);
            }
        }
        private void btnBrowseOutFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = this.outFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetOutFolder(this.outFolderBrowser.SelectedPath);
            }

        }

        private void EnableButtons()
        {
            // See https://stackoverflow.com/a/142069
            this.btnMoveOut.Invoke(new MethodInvoker(delegate { this.btnMoveOut.Enabled = true; }));
            this.btnMoveDelete.Invoke(new MethodInvoker(delegate { this.btnMoveDelete.Enabled = true; }));
        }
        private void DisableButtons()
        {
            // See https://stackoverflow.com/a/142069
            this.btnMoveOut.Invoke(new MethodInvoker(delegate { this.btnMoveOut.Enabled = false; }));
            this.btnMoveDelete.Invoke(new MethodInvoker(delegate { this.btnMoveDelete.Enabled = false; }));
        }
        private string? PickRandomFilePath()
        {
            IEnumerable<string> mp3s = Directory.EnumerateFiles(this.inFolderBrowser.SelectedPath, "*.*", SearchOption.TopDirectoryOnly);
            if (!mp3s.Any())
            {
                return null;
            }

            Random random = new Random();
            int chosenMp3Index = random.Next(1, mp3s.Count());
            return mp3s.ElementAt(chosenMp3Index - 1);
        }
        private void RunRandom()
        {
            string? nextFilePath = PickRandomFilePath();
            if (String.IsNullOrEmpty(nextFilePath)) // Previous was the last one, and it's being treated.
            {
                //int remainingCount = Directory.EnumerateFiles(this.inFolderBrowser.SelectedPath, "*.*", SearchOption.TopDirectoryOnly).Count();
                this.btnRun.Enabled = false;
                this.btnMoveOut.Enabled = false;
                this.btnMoveDelete.Enabled = false;
                MessageBox.Show("Only one file remains; close the application that plays it so that it can be treated");
                return;
            }

            while (this.filesToMove.Any(f => f.Path == nextFilePath))  // We landed on a file already marked as treated.
            {
                nextFilePath = PickRandomFilePath();
            }
            this.lastFilePath = nextFilePath;

            Run();
        }
        private void Run()
        {
            Task.Run(() =>
            {
                // See https://stackoverflow.com/a/240610
                this.processStartInfo.Arguments = $"\"{this.lastFilePath}\"";
                this.process = Process.Start(this.processStartInfo);
                if (this.process != null)
                {
                    this.process.WaitForExit();
                    //exitCode = this.process.ExitCode;

                    //if (!Process.GetProcessesByName(VlcProcessName).Any())
                    //{
                    //    DisableButtons();
                    //}
                }
            });
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // See https://stackoverflow.com/a/22788658
            switch (keyData)
            {
                case Keys.Back:
                case Keys.Left: Run(); break;   // Replay.
                case Keys.Right: this.btnRun.PerformClick(); break;
                case Keys.Add:
                //case Keys.Insert:
                case Keys.Y: this.btnMoveOut.PerformClick(); break;
                case Keys.Delete:
                case Keys.Subtract:
                case Keys.N: this.btnMoveDelete.PerformClick(); break;
                default: break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsStarted())
            {
                if (++this.skippedCount == MaxSkipCount)
                {
                    this.btnRun.Enabled = false;
                }
            }
            this.btnRun.Text = $"Next\n(skip: {MaxSkipCount - this.skippedCount})";
            RunRandom();
            EnableButtons();
        }
        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            if (IsStarted())
            {
                DisableButtons();
                this.filesToMove.Add(new FileMove { Action = MoveAction.Keep, Path = this.lastFilePath });
                RunRandom();
                this.buttonsReenablingTimer.Start();

                // A little treat for being decisive ;)
                --this.skippedCount;
                if ((MaxSkipCount - this.skippedCount) == 1)
                {
                    this.btnRun.Enabled = true;
                }

                this.btnRun.Text = $"Next\n(skip: {MaxSkipCount - this.skippedCount})";
            }
        }
        private void btnMoveDelete_Click(object sender, EventArgs e)
        {
            if (IsStarted())
            {
                DisableButtons();
                this.filesToMove.Add(new FileMove { Action = MoveAction.Delete, Path = this.lastFilePath });
                RunRandom();
                this.buttonsReenablingTimer.Start();
            }
        }
        private bool IsStarted() => !String.IsNullOrEmpty(this.lastFilePath);

        private void ConsumeFile()
        {
            while (!this.filesToMove.IsCompleted)
            {
                FileMove fileToMove = this.filesToMove.Take();   // This is THE blocking call.
                Thread.Sleep(500);  // Let the opening of another file go first.

                bool wasTreated = false;
                for (int i = 0; i < MaxRetriesCount && !wasTreated; i++)
                {
                    try
                    {
                        switch (fileToMove.Action)
                        {
                            case MoveAction.Keep: File.Move(fileToMove.Path, Path.Combine(this.outFolderBrowser.SelectedPath, Path.GetFileName(fileToMove.Path))); break;
                            case MoveAction.Delete: File.Delete(fileToMove.Path); break;
                        }
                        wasTreated = true;
                    }
                    catch (Exception)   // If the file is still in use.
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (wasTreated)
                {
                    IncrementCounts(fileToMove.Action);
                }
                else
                {
                    MessageBox.Show($"Couln't {fileToMove.Action} file '{Path.GetFileName(fileToMove.Path)}': unable to obtain exclusive access after {MaxRetriesCount} attempts");
                }

            }
        }
        #endregion
    }

    public enum MoveAction
    {
        Keep,
        Delete
    }
    public struct FileMove
    {
        public MoveAction Action { get; set; }
        public string Path { get; set; }
    }
    public class MoveCount
    {
        public int KeptCount { get; set; }
        public int DeletedCount { get; set; }
        public int Count => KeptCount + DeletedCount;
        public MoveCount()
        {
            KeptCount = 0;
            DeletedCount = 0;
        }
        public void Reset()
        {
            KeptCount = 0;
            DeletedCount = 0;
        }
        public void Increment(MoveAction action)
        {
            switch (action)
            {
                case MoveAction.Keep: KeptCount++; break;
                case MoveAction.Delete: DeletedCount++; break;
            }
        }
        public override string ToString()
        {
            return $"{Count} ({KeptCount}+{DeletedCount})";
        }
    }
    public static class MoveCountExtensions
    {
        public static MoveCount Sum(this IEnumerable<KeyValuePair<DateTime, MoveCount>> kvps) => kvps.Select(kvp => kvp.Value).Sum();
        public static MoveCount Sum(this IEnumerable<MoveCount> moveCounts)
        {
            return new MoveCount { KeptCount = moveCounts.Sum(m => m.KeptCount), DeletedCount = moveCounts.Sum(m => m.DeletedCount) };
        }
    }
}