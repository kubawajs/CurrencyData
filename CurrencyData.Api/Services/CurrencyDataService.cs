using CurrencyData.Api.Repositories.Abstractions;
using CurrencyData.Api.Services.Abstractions;
using System;
using System.Threading.Tasks;
using EcbSdmx.Core.Models.Response;
using EcbSdmx.Core.Services.Abstractions;

namespace CurrencyData.Api.Services
{
    public class CurrencyDataService : ICurrencyDataService
    {
        private readonly ICurrencyDataRepository _currencyDataRepository;
        private readonly IEcbSdmxService _ecbSdmxService;

        public CurrencyDataService(ICurrencyDataRepository currencyDataRepository, IEcbSdmxService ecbSdmxService)
        {
            // TODO: redis cache
            // TODO: 
            _currencyDataRepository = currencyDataRepository;
            _ecbSdmxService = ecbSdmxService;
        }

        // TODO: currencyCodes to dict Dictionary<string, string> currencyCodes,
        public async Task<ApiResponseData> GetCurrencies(string code1, string code2, DateTime startDate, DateTime endDate)
        {
            // if in redis cache, return
            // if in sql cache, return
            return await _ecbSdmxService.Get(code1, code2, startDate, endDate);
        }
    }
}
