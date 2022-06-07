using Dispatch.Domain.Models;
using Dispatch.Domain.Models.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dispatch.Domain
{
    public class SkipArgs : EventArgs
    {
        public int Count { get; set; }
    }
    public class WarningArgs : EventArgs
    {
        public string? Message { get; set; }
    }
    public class Engine
    {
        #region Variables
        private const string ExplorerProcessFileName = "explorer";
        private BlockingCollection<FileMove> filesToMove = new BlockingCollection<FileMove>(1);
        private ProcessStartInfo processStartInfo;
        private Process? process;
        private string? inDirectory;
        private string? outDirectory;
        private string? countsFilePath;
        private int skipCount;
        #endregion

        #region Properties
        public event EventHandler<SkipArgs>? SkipCountUpdated;
        public event EventHandler<WarningArgs>? WarningThrown;
        public event Action? CountsUpdated;
        public event Action? EndReached;

        public Dictionary<DateTime, MoveCount>? Counts { get; set; }
        public DateTime Today { get; set; }
        public MoveCount SessionCount { get; set; }
        public MoveCount WeekCount { get; set; }
        public MoveCount MonthCount { get; set; }
        public MoveCount YearCount { get; set; }
        public string? CurrentFilePath { get; set; }
        /// <summary>
        /// Total number of files to sort, initialized once by browser the "In" directory recursively on the very first run.
        /// </summary>
        public int OriginalFilesCount { get; set; }
        /// <summary>
        /// Number of files currently in "In" folder itself (i.e. non-recursively).
        /// </summary>
        public int inFolderFilesCount { get; set; }
        #endregion

        #region Constructor
        public Engine(string inDirectory, string outDirectory)
        {
            this.inDirectory = inDirectory;
            this.outDirectory = outDirectory;

            this.Today = DateTime.Today;    // So the whole program agrees on what the current day is.
            this.SessionCount = new MoveCount();
            this.WeekCount = new MoveCount();
            this.MonthCount = new MoveCount();
            this.YearCount = new MoveCount();

            this.processStartInfo = new ProcessStartInfo();
            this.processStartInfo.FileName = ExplorerProcessFileName;
            this.processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            this.processStartInfo.CreateNoWindow = true;
            Task.Run(() => ConsumeFileToMove());
        }
        #endregion

        #region Methods
        public void SaveStats()
        {
            if (String.IsNullOrEmpty(this.countsFilePath) || this.Counts == null)
            {
                return;
            }

            using (StreamWriter countsFile = new StreamWriter(this.countsFilePath, false))
            {
                foreach (var kvp in this.Counts)
                {
                    countsFile.WriteLine($"{kvp.Key.ToString("MM.dd")}\t{kvp.Value.KeptCount}\t{kvp.Value.DeletedCount}");
                }
            }
        }
        public bool IsPlaying() => !String.IsNullOrEmpty(this.CurrentFilePath);
        public void InitializeCounts()
        {
            string statsDirectory = Path.Combine(this.inDirectory, Settings.StatsFolder);
            if (!Directory.Exists(statsDirectory))
            {
                Directory.CreateDirectory(statsDirectory);
            }

            this.OriginalFilesCount = ReadOriginalCount(this.inDirectory, statsDirectory);
            this.inFolderFilesCount = Directory.EnumerateFiles(this.inDirectory, "*.*", SearchOption.TopDirectoryOnly).Count();
            this.SessionCount.Reset();
            this.skipCount = -1;  // -1 accounts for first increment performed regardless (see method Next()).

            this.countsFilePath = Path.Combine(statsDirectory, $"{this.Today.Year}.txt");
            if (File.Exists(countsFilePath))
            {
                this.Counts = ParseCounts(File.ReadAllLines(countsFilePath));
                if (!Counts.ContainsKey(Today))
                {
                    this.Counts.Add(Today, new MoveCount());
                }

                // From last Monday.
                int numberOfDaysFromMonday = CalculatePositiveModulo(Today.DayOfWeek - DayOfWeek.Monday, 7);
                DateTime lastMonday = Today.AddDays(-numberOfDaysFromMonday);
                this.WeekCount = this.Counts.Where(kvp => kvp.Key >= lastMonday).Sum();
                // Last 7 days.
                //this.weekCount = this.counts.Where(kvp => kvp.Key >= this.today.AddDays(-6)).Sum();
                this.MonthCount = this.Counts.Where(kvp => kvp.Key.Month == Today.Month).Sum();
                this.YearCount = this.Counts.Sum();
            }
            else
            {
                this.Counts = new Dictionary<DateTime, MoveCount> { { this.Today, new MoveCount { KeptCount = 0, DeletedCount = 0 } } };
                this.WeekCount.Reset();
                this.MonthCount.Reset();
                this.YearCount.Reset();
            }

            this.CountsUpdated?.Invoke();
        }
        private int ReadOriginalCount(string inDirectory, string statsDirectory)
        {
            string originalCountFilePath = Path.Combine(statsDirectory, Settings.OriginalCountFileName);
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

                DateTime date = DateTime.ParseExact($"{Today.Year}.{lineFrags[0]}", "yyyy.MM.dd", System.Globalization.CultureInfo.CurrentCulture);
                int keptCount = Int32.Parse(lineFrags[1]);
                int deletedCount = Int32.Parse(lineFrags[2]);
                countsByDay.Add(date, new MoveCount { KeptCount = keptCount, DeletedCount = deletedCount });
            }
            return countsByDay;
        }
        private int CalculatePositiveModulo(int i, int n) => (i % n + n) % n;

        public void Next(bool isSkipping = true)
        {
            if (isSkipping && this.skipCount >= Settings.MaxSkipCount)
            {
                return;
            }

            string? nextFilePath = GetNextRandomFilePath();
            if (String.IsNullOrEmpty(nextFilePath))
            {
                EndReached?.Invoke();
                return;
            }

            this.CurrentFilePath = nextFilePath;
            Run();

            if (isSkipping)
            {
                SetSkipCount(++this.skipCount);
            }
        }
        public void Move()
        {
            if (!String.IsNullOrEmpty(this.CurrentFilePath))
            {
                this.filesToMove.Add(new FileMove { Action = MoveAction.Keep, Path = this.CurrentFilePath });
                Next(false);
                SetSkipCount(--this.skipCount);   // A little treat for being decisive ;)
            }
        }

        public void Delete()
        {
            if (!String.IsNullOrEmpty(this.CurrentFilePath))
            {
                this.filesToMove.Add(new FileMove { Action = MoveAction.Delete, Path = this.CurrentFilePath });
                Next(false);
            }
        }
        public void Run()
        {
            Task.Run(() =>
            {
                // See https://stackoverflow.com/a/240610
                this.processStartInfo.Arguments = $"\"{this.CurrentFilePath}\"";
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
        private string? GetNextRandomFilePath()
        {
            IEnumerable<string> remainingsNotCurrentlyBeingMoved = Directory.EnumerateFiles(this.inDirectory, "*.*", SearchOption.TopDirectoryOnly).Where(remaining => this.filesToMove.All(move => remaining != move.Path));
            if (!remainingsNotCurrentlyBeingMoved.Any())
            {
                return null;
            }

            Random random = new Random();
            int chosenMp3Index = random.Next(1, remainingsNotCurrentlyBeingMoved.Count());
            return remainingsNotCurrentlyBeingMoved.ElementAt(chosenMp3Index - 1);
        }

        private void ConsumeFileToMove()
        {
            while (!this.filesToMove.IsCompleted)
            {
                FileMove fileToMove = this.filesToMove.Take();   // This is THE blocking call.
                Thread.Sleep(500);  // Let the opening of another file go first.

                bool wasTreated = false;
                for (int i = 0; i < Settings.MaxMoveRetriesCount && !wasTreated; i++)
                {
                    try
                    {
                        switch (fileToMove.Action)
                        {
                            case MoveAction.Keep: File.Move(fileToMove.Path, Path.Combine(this.outDirectory, Path.GetFileName(fileToMove.Path))); break;
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
                    WarningThrown?.Invoke(this,
                        new WarningArgs { Message = $"Couln't {fileToMove.Action} file '{Path.GetFileName(fileToMove.Path)}': unable to obtain exclusive access after {Settings.MaxMoveRetriesCount} attempts" });
                }
            }
        }
        private void IncrementCounts(MoveAction action)
        {
            this.SessionCount.Increment(action);
            this.Counts[this.Today].Increment(action);
            this.WeekCount.Increment(action);
            this.MonthCount.Increment(action);
            this.YearCount.Increment(action);

            this.CountsUpdated?.Invoke();
        }
        private void SetSkipCount(int skipCount)
        {
            this.SkipCountUpdated?.Invoke(this, new SkipArgs { Count = Settings.MaxSkipCount - skipCount });
        }
        #endregion
    }
}
