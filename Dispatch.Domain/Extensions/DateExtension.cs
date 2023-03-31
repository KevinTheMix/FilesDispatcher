using System;

namespace Dispatch.Domain.Extensions
{
    internal static class DateExtension
    {
        private static int CalculatePositiveModulo(int i, int n) => (i % n + n) % n;
        private static int GetNumberOfDaysFromPreviousMonday(this DateTime date) => CalculatePositiveModulo(date.DayOfWeek - DayOfWeek.Monday, 7);
        public static DateTime GetLastMonday(this DateTime date) => date.AddDays(-date.GetNumberOfDaysFromPreviousMonday());
    }
}
