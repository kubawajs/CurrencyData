using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcbSdmx.Core.Models.Response;

namespace CurrencyData.Api.Services.Abstractions
{
    public interface ICurrencyDataService
    {
        // TODO: change into currency item
        Task<ApiResponseData> GetCurrencies(string code1, string code2, DateTime startDate, DateTime endDate);
    }
}
