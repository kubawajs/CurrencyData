namespace EcbSdmx.Infrastructure
{
    public static class Constants
    {
        public struct EcbSdmx
        {
            public struct Api
            {
                public const string Url = "https://sdw-wsrest.ecb.europa.eu/service/data/EXR/";
                public static readonly string QueryParameters = "D.{0}.{1}.SP00.A?startPeriod={2:yyyy-MM-dd}&endPeriod={3:yyyy-MM-dd}&detail=dataonly";
            }

            public const string CurrencyId = "CURRENCY";
            public const string CurrencyDenomId = "CURRENCY_DENOM";
        }
    }
}
