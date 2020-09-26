using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Extensions;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using CurrencyData.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyData.Infrastructure.Services
{
    public class CurrencyDataService : ICurrencyDataService
    {
        private readonly ILogger<CurrencyDataService> _logger;
        private readonly ICurrencyDataRepository _currencyDataRepository;
        private readonly IDistributedCache _distributedCache;

        public CurrencyDataService(ILogger<CurrencyDataService> logger, ICurrencyDataRepository currencyDataRepository,
            IDistributedCache distributedCache)
        {
            // TODO: sql cache
            _logger = logger;
            _currencyDataRepository = currencyDataRepository;
            _distributedCache = distributedCache;
        }

        public async Task<ApiResponseDataDTO> GetCurrencies(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            var currencyKey = currencyCodes.First().Key;
            
            var cacheKey = GenerateCacheKey(currencyKey, currencyCodes[currencyKey], startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            var cachedResponse = await _distributedCache.GetAsync(cacheKey);

            if (cachedResponse != null)
            {
                _logger.LogInformation($"Returning value from distributed cache for key: {cacheKey}");
                return cachedResponse.FromByteArray<ApiResponseDataDTO>();
            }

            var response = await _currencyDataRepository.GetAsync(currencyKey, currencyCodes[currencyKey], startDate, endDate);

            await _distributedCache.SetAsync(cacheKey, response.ToByteArray(),
                new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30)));
            _logger.LogInformation($"Setting value in distributed cache for key: {cacheKey}");

            return response;
        }

        public static string GenerateCacheKey(string inCode, string outCode, string startDate, string endDate) =>
            $"{inCode};{outCode};{startDate};{endDate}";
    }
}
