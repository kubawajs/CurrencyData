using System;

namespace CurrencyData.Infrastructure.Domain
{
    [Serializable]
    public class DailyExchangeRate
    {
        public DateTime Date { get; set; }
        public double? Rate { get; set; }
    }
}
