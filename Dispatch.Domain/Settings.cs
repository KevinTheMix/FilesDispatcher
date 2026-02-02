namespace Dispatch.Domain
{
    public static class Settings
    {
        public const string InDirectory = @"M:\@ Sort\# Musique";
        public const string OutDirectory = @"M:\@ Sort\@ Very Good";
        public const string StatsFolder = "- Dispatch Stats";
        public const string RemainingCountFileName = "remaining.txt";
        public const int MaxSkipCount = 50;
        public const int ButtonsReleaseThrottleMilliseconds = 800;
        public const int MaxMoveRetriesCount = 20;
        public const int MoveTryMilliseconds = 1000;
        public const int PastYearsLookedBehind = 5;
        public const int DaysBeforeUpdate = 7;
    }
}
