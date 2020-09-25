using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcbSdmx.Core.Models.Response;

namespace CurrencyData.Infrastructure.Services.Abstractions
{
    public interface ICurrencyDataService
    {
        Task<ApiResponseData> GetCurrencies(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate);
    }
}
