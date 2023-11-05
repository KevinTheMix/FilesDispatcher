namespace Dispatch.Domain
{
    public static class Settings
    {
        public const string InDirectory = @"V:\@ Sort\# Musique";
        //public const string InDirectory = @"M:";
        public const string OutDirectory = @"V:\@ Sort\@ Very Good";
        public const string StatsFolder = "- Dispatch Stats";
        public const string GrowingTotalFileName = "original_count.txt";
        public const int MaxSkipCount = 50;
        public const int ButtonsReleaseThrottleMilliseconds = 800;
        public const int MaxMoveRetriesCount = 20;
        public const int MoveTryMilliseconds = 1000;
        public const int PastYearsLookedBehind = 5;
    }
}
