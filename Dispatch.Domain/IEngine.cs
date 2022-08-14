using Dispatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dispatch.Domain
{
    public interface IEngine
    {
        Dictionary<DateTime, MoveCount>? Counts { get; set; }
        DateTime Today{ get; set; }
        MoveCount SessionCount{ get; set; }
        MoveCount WeekCount{ get; set; }
        MoveCount MonthCount{ get; set; }
        MoveCount YearCount{ get; set; }
        string? CurrentFilePath{ get; set; }
        /// <summary>
        /// Total number of files to sort, initialized once by browser the "In" directory recursively on the very first run.
        /// </summary>
        int OriginalFilesCount { get; set; }
        /// <summary>
        /// Number of files currently in "In" folder itself (i.e. non-recursively).
        /// </summary>
        int inFolderFilesCount{ get; set; }

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
