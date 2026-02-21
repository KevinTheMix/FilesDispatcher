using Dispatch.Domain.Extensions;
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

    public class Engine : IEngine
    {
        #region Properties
        public event EventHandler<SkipArgs>? SkipCountUpdated;
        public event EventHandler<WarningArgs>? WarningThrown;
        public event Action? CountsUpdated;
        public event Action? EndReached;

        public Dictionary<DateTime, MoveCount> Counts { get; private set; }
        public DateTime Today { get; set; }
        public MoveCount SessionCount { get; set; } = new MoveCount();
        public MoveCount WeekCount { get; set; } = new MoveCount();
        public MoveCount MonthCount { get; set; } = new MoveCount();
        public MoveCount YearCount { get; set; } = new MoveCount();
        public MoveCount DoneCount { get; set; } = new MoveCount();
        public string? CurrentFilePath { get; set; }
        /// <summary>
        /// Number of files currently in "In" folder itself (ie non-recursively).
        /// </summary>
        public int InFolderCount { get; set; }
        /// <summary>
        /// Number of files currently in the "In" directory (recursively), initialized once on the very first run and periodically refreshed after that.
        /// </summary>
        public int RemainingCount { get; set; }
        public bool IsReadOnly { get; set; }
        #endregion

        #region Variables
        private readonly BlockingCollection<FileMove> filesToMove = new(1);
        private readonly ProcessStartInfo processStartInfo;
        private readonly string inDirectory;
        private readonly string outDirectory;
        private readonly string statsDirectory;
        private readonly string remaininCountFilePath;
        private readonly Random random;
        private Process? process;
        private int skipsCount;
        #endregion

        #region Constructor
        public Engine(string inDirectory, string outDirectory)
        {
            this.skipsCount = -1;  // accounts for first increment performed regardless (see method Next()).
            this.random = new Random();
            this.Today = DateTime.Today;    // So the whole class agrees on what the current day is.
            this.inDirectory = inDirectory;
            this.outDirectory = outDirectory;
            this.statsDirectory = Path.Combine(this.inDirectory, Settings.StatsFolder);
            this.remaininCountFilePath = Path.Combine(this.statsDirectory, Settings.RemainingCountFileName);
            CreateOnceStatsFolder();

            this.Counts = ReadDatedCounts();
            InitializeCounts();

            this.processStartInfo = new ProcessStartInfo
            {
                //FileName = "explorer",    // Previously using explorer.exe to open files, but it couldn't handle Unicode chars in file names, so use Windows shell instead.
                WindowStyle = ProcessWindowStyle.Minimized,
                CreateNoWindow = true
            };
            Task.Run(() => ConsumeFileToMove());
        }
        #endregion

        #region Methods
        public async Task Next(bool isSkipping = true)
        {
            if (isSkipping && this.skipsCount >= Settings.MaxSkipCount)
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
            await RunCurrent();

            if (isSkipping)
            {
                SetSkipCount(++this.skipsCount);
            }
        }
        public async Task RunCurrent()
        {
            await Task.Run(() =>
            {
                // See https://stackoverflow.com/a/240610
                this.processStartInfo.FileName = this.CurrentFilePath;
                this.processStartInfo.UseShellExecute = true;   // Using this to open non-executable files (eg media). Thanks ChatGPT 3.5.
                if (this.process == null)
                {
                    this.process = Process.Start(this.processStartInfo);
                }
                else
                {
                    this.process.StartInfo = this.processStartInfo;
                    this.process.Start();
                }
                this.process?.Close();
                //this.process.WaitForExit();
            });
        }
        public async Task Move()
        {
            if (!this.IsReadOnly && !String.IsNullOrEmpty(this.CurrentFilePath))
            {
                this.filesToMove.Add(new FileMove { Action = MoveAction.Keep, Path = this.CurrentFilePath });
                await Next(false);
                SetSkipCount(--this.skipsCount);   // A little treat for being decisive ;)
            }
        }
        public async Task Delete()
        {
            if (!this.IsReadOnly && !String.IsNullOrEmpty(this.CurrentFilePath))
            {
                this.filesToMove.Add(new FileMove { Action = MoveAction.Delete, Path = this.CurrentFilePath });
                await Next(false);
            }
        }

        private void InitializeCounts()
        {
            if (!this.Counts.Any() || !this.Counts.ContainsKey(this.Today))
            {
                this.Counts.Add(this.Today, new MoveCount());
            }
            if (this.Counts.Any())
            {
                CalculateDoneCounts();
            }

            this.RemainingCount = File.Exists(this.remaininCountFilePath) ? ReadOrEnumerateStaleRemainingCount() : EnumerateRemainingCount();
            this.InFolderCount = Directory.EnumerateFiles(this.inDirectory, "*.*", SearchOption.TopDirectoryOnly).Count();
            this.CountsUpdated?.Invoke();
        }
        private Dictionary<DateTime, MoveCount> ReadDatedCounts()
        {
            Dictionary<DateTime, MoveCount> counts = new();
            string yearFilePath;
            int year = this.Today.Year;
            bool lastFileExists = false;
            do
            {
                yearFilePath = Path.Combine(statsDirectory, $"{year}.txt");
                lastFileExists = File.Exists(yearFilePath);
                if (lastFileExists)
                {
                    // Merging dictionaries (without duplicate values https://stackoverflow.com/q/294138/3559724).
                    counts = counts.Concat(File.ReadAllLines(yearFilePath).IntoDatedDictionary(year)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                }
            }
            while (this.Today.Year - --year <= Settings.PastYearsLookedBehind || lastFileExists);
            return counts;
        }
        private void CalculateDoneCounts()
        {
            this.WeekCount = this.Counts.Where(kvp => kvp.Key >= Today.GetLastMonday()).Sum();
            this.MonthCount = this.Counts.Where(kvp => kvp.Key.Year == Today.Year && kvp.Key.Month == Today.Month).Sum();
            this.YearCount = this.Counts.Where(kvp => kvp.Key.Year == Today.Year).Sum();
            this.DoneCount = this.Counts.Sum();
        }
        private int ReadOrEnumerateStaleRemainingCount()
        {
            bool isRemainingCountStale = new FileInfo(this.remaininCountFilePath).LastWriteTime < this.Today.AddDays(-Settings.DaysBeforeUpdate);
            return isRemainingCountStale ? EnumerateRemainingCount() : Int32.Parse(File.ReadAllText(this.remaininCountFilePath));
        }
        private int EnumerateRemainingCount()
        {
            int remainingCount = Directory.EnumerateFiles(this.inDirectory, "*.*", new EnumerationOptions() { AttributesToSkip = FileAttributes.System, RecurseSubdirectories = true }).Count();
            WriteRemainingCountFile(remainingCount);
            return remainingCount;
        }
        private void CreateOnceStatsFolder()
        {
            string statsDirectory = Path.Combine(this.inDirectory, Settings.StatsFolder);
            if (!Directory.Exists(statsDirectory))
            {
                Directory.CreateDirectory(this.statsDirectory);
            }
        }
        private void WriteRemainingCountFile(int remainingCount)
        {
            File.WriteAllText(this.remaininCountFilePath, remainingCount.ToString());
        }
        private void WriteCurrentYearFile()
        {
            string currentYearFilePath = Path.Combine(this.statsDirectory, $"{this.Today.Year}.txt");
            using StreamWriter countsFile = new(currentYearFilePath, false);
            foreach (var kvp in this.Counts.Where(kvp => kvp.Key.Year == Today.Year))
            {
                countsFile.WriteLine($"{kvp.Key:MM.dd}\t{kvp.Value.KeptCount}\t{kvp.Value.DeletedCount}");
            }
        }
        private string? GetNextRandomFilePath()
        {
            IEnumerable<string> remainingsNotCurrentlyBeingMoved = Directory.EnumerateFiles(this.inDirectory, "*.*", SearchOption.TopDirectoryOnly).Where(remaining => this.filesToMove.All(move => remaining != move.Path));
            if (!remainingsNotCurrentlyBeingMoved.Any())
            {
                return null;
            }

            int chosenFileIndex = this.random.Next(0, remainingsNotCurrentlyBeingMoved.Count());    // Upper bound is non-inclusive => no need to decrement by 1.
            return remainingsNotCurrentlyBeingMoved.ElementAt(chosenFileIndex);
        }
        private void ConsumeFileToMove()
        {
            while (!this.filesToMove.IsCompleted)   // While queue not empty.
            {
                FileMove fileToMove = this.filesToMove.Take();   // This is THE blocking call.
                string fileNameToMove = Path.GetFileName(fileToMove.Path);
                Thread.Sleep(Settings.MoveTryMilliseconds);  // Let the opening of another file go first.

                bool wasTreated = false;
                for (int i = 0; i < Settings.MaxMoveRetriesCount && !wasTreated; i++)
                {
                    try
                    {
                        File.SetAttributes(fileToMove.Path, FileAttributes.Normal); // Removes potential readonly attributes preventing deletion (https://stackoverflow.com/a/45603466/3559724).
                        switch (fileToMove.Action)
                        {
                            case MoveAction.Keep: File.Move(fileToMove.Path, Path.Combine(this.outDirectory, fileNameToMove)); break;
                            case MoveAction.Delete: File.Delete(fileToMove.Path); break;
                        }

                        // Log
                        string statsDirectory = Path.Combine(this.inDirectory, Settings.StatsFolder);
                        File.AppendAllText(Path.Combine(statsDirectory, "move.log"), $"{fileToMove.Action}\t{fileNameToMove}{Environment.NewLine}");

                        wasTreated = true;
                    }
                    catch   // If the file is still in use.
                    {
                        Thread.Sleep(Settings.MoveTryMilliseconds);
                    }
                }

                if (wasTreated)
                {
                    IncrementCounts(fileToMove.Action);
                    SaveStats();
                }
                else
                {
                    WarningThrown?.Invoke(this,
                        new WarningArgs { Message = $"Couldn't {fileToMove.Action} file '{Path.GetFileName(fileToMove.Path)}': unable to obtain exclusive access after {Settings.MaxMoveRetriesCount} attempts" });
                }
            }
        }
        private void SetSkipCount(int skipCount)
        {
            this.SkipCountUpdated?.Invoke(this, new SkipArgs { Count = Settings.MaxSkipCount - skipCount });
        }
        private void IncrementCounts(MoveAction action)
        {
            this.Counts[this.Today].Increment(action);
            this.SessionCount.Increment(action);
            this.WeekCount.Increment(action);
            this.MonthCount.Increment(action);
            this.YearCount.Increment(action);
            this.DoneCount.Increment(action);

            this.CountsUpdated?.Invoke();
        }
        private void SaveStats()
        {
            if (!this.IsReadOnly)
            {
                WriteCurrentYearFile();
            }
        }
        #endregion
    }
}
