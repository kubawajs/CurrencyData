using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Extensions;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using CurrencyData.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyData.Infrastructure.Services
{
    public class CurrencyDataService : ICurrencyDataService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CurrencyDataService> _logger;
        private readonly ICurrencyDataRepository _currencyDataRepository;
        private readonly IDistributedCache _distributedCache;

        public CurrencyDataService(ILogger<CurrencyDataService> logger, IConfiguration configuration,
            ICurrencyDataRepository currencyDataRepository, IDistributedCache distributedCache)
        {
            _config = configuration;
            _logger = logger;
            _currencyDataRepository = currencyDataRepository;
            _distributedCache = distributedCache;
        }

        public async Task<ResponseData> GetCurrencies(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            if (!currencyCodes.Any())
            {
                return null;
            }

            // Set date as recent working day
            startDate = startDate.GetRecentWorkingDayDate();
            endDate = endDate.GetRecentWorkingDayDate();

            // Get from cache
            var currencyKey = currencyCodes.First().Key;
            var cacheKey = GenerateCacheKey(currencyKey, currencyCodes[currencyKey], startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            
            var cachedResponse = await _distributedCache.GetAsync(cacheKey);
            if (cachedResponse != null)
            {
                _logger.LogInformation($"Returning value from distributed cache for key: {cacheKey}");
                return cachedResponse.FromByteArray<ResponseData>();
            }

            // Get from external API
            try
            {
                var response = await _currencyDataRepository.GetAsync(currencyKey, currencyCodes[currencyKey], startDate, endDate);
                response.ExchangeRates = response.ExchangeRates.Where(x => x.Rate.HasValue).ToList();

                await _distributedCache.SetAsync(cacheKey, response.ToByteArray(),
                        new DistributedCacheEntryOptions().SetSlidingExpiration(
                            TimeSpan.FromDays(double.Parse(_config[Constants.Configuration.DistributedCache.SlidingExpirationTimeDays]))
                        ));
                _logger.LogInformation($"Setting value in distributed cache for key: {cacheKey}");

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured during retrieving data from EcbService. Error details: {e.Message}");
                _logger.LogError(e, e.Message);
                return null;
            }
        }

        public static string GenerateCacheKey(string inCode, string outCode, string startDate, string endDate) =>
            $"{inCode};{outCode};{startDate};{endDate}";
    }
}
