using Dispatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch.Domain
{
    internal static class MoveCountExtensions
    {
        public static MoveCount Sum(this IEnumerable<KeyValuePair<DateTime, MoveCount>> kvps) => kvps.Select(kvp => kvp.Value).Sum();
        public static MoveCount Sum(this IEnumerable<MoveCount> moveCounts)
        {
            return new MoveCount { KeptCount = moveCounts.Sum(m => m.KeptCount), DeletedCount = moveCounts.Sum(m => m.DeletedCount) };
        }
    }
}
