using Dispatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dispatch.Domain
{
    public interface IEngine
    {
        Dictionary<DateTime, MoveCount> Counts { get; }
        DateTime Today{ get; set; }
        MoveCount SessionCount{ get; set; }
        MoveCount WeekCount{ get; set; }
        MoveCount MonthCount{ get; set; }
        MoveCount YearCount{ get; set; }
        MoveCount DoneCount{ get; set; }
        string? CurrentFilePath{ get; set; }
        /// <summary>
        /// Number of files currently in "In" folder itself (ie non-recursively).
        /// </summary>
        int InFolderCount { get; set; }
        /// <summary>
        /// Number of files currently in the "In" directory (recursively), initialized once on the very first run and periodically refreshed after that.
        /// </summary>
        int RemainingCount { get; set; }

        event EventHandler<SkipArgs>? SkipCountUpdated;
        event EventHandler<WarningArgs>? WarningThrown;
        event Action? CountsUpdated;
        event Action? EndReached;

        Task Next(bool isSkipping = true);
        Task RunCurrent();
        Task Move();
        Task Delete();
    }
}
