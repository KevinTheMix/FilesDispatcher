using Dispatch.Domain.Extensions;
using Dispatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch.Domain.Extensions
{
    internal static class MoveCountExtensions
    {
        public static MoveCount Sum(this IEnumerable<KeyValuePair<DateTime, MoveCount>> kvps) => kvps.Select(kvp => kvp.Value).Sum();
        public static MoveCount Sum(this IEnumerable<MoveCount> moveCounts)
        {
            return new MoveCount { KeptCount = moveCounts.Sum(m => m.KeptCount), DeletedCount = moveCounts.Sum(m => m.DeletedCount) };
        }

        public static Dictionary<DateTime, MoveCount> IntoDatedDictionary(this string[] countLines, int year)
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

                DateTime date = DateTime.ParseExact($"{year}.{lineFrags[0]}", "yyyy.MM.dd", System.Globalization.CultureInfo.CurrentCulture);
                int keptCount = Int32.Parse(lineFrags[1]);
                int deletedCount = Int32.Parse(lineFrags[2]);
                countsByDay.Add(date, new MoveCount { KeptCount = keptCount, DeletedCount = deletedCount });
            }
            return countsByDay;
        }
    }
}
