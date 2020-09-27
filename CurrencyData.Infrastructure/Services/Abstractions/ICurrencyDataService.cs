using CurrencyData.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyData.Infrastructure.Services.Abstractions
{
    public interface ICurrencyDataService
    {
        Task<ResponseData> GetCurrencies(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate);
    }
}
