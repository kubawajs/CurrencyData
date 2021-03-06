﻿using System;
using System.Collections.Generic;

namespace CurrencyData.Infrastructure.Domain
{
    [Serializable]
    public class ResponseData
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DailyExchangeRate> ExchangeRates { get; set; }
    }
}
