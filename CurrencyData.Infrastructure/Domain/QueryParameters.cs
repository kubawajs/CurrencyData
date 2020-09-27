using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyData.Infrastructure.Domain
{
    public class QueryParameters
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public string CacheKey => $"{FromCurrency};{ToCurrency};{StartPeriod:yyyy-MM-dd};{EndPeriod:yyyy-MM-dd}";

        private QueryParameters(string fromCurrency, string toCurrency, DateTime startPeriod, DateTime endPeriod)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
        }

        public static QueryParameters Create(Dictionary<string, string> currencyCodes, DateTime startDate,
            DateTime endDate)
        {
            if (!currencyCodes.Any())
            {
                return new QueryParameters(null, null, startDate, endDate);
            }
            var currencyKey = currencyCodes.Keys.First();
            return new QueryParameters(currencyKey, currencyCodes[currencyKey], startDate, endDate);
        }
    }
}
