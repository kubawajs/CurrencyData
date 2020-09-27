namespace CurrencyData.Infrastructure
{
    public static class Constants
    {
        public struct Configuration
        {
            private const string DistCache = "DistCache";
            public struct DistributedCache
            {
                public static readonly string ConnectionString = $"{DistCache}:ConnectionString";
                public static readonly string TableName = $"{DistCache}:TableName";
                public static readonly string SlidingExpirationTimeDays = $"{DistCache}:SlidingExpirationTimeDays";
            }
        }
    }
}
