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

        public async Task<ResponseData> GetCurrencies(QueryParameters queryParameters)
        {
            // Set date as recent working day
            queryParameters.StartPeriod = queryParameters.StartPeriod.GetRecentWorkingDayDate();
            queryParameters.EndPeriod = queryParameters.EndPeriod.GetRecentWorkingDayDate();

            // Get from cache
           
            var cachedResponse = await _distributedCache.GetAsync(queryParameters.CacheKey);
            if (cachedResponse != null)
            {
                _logger.LogInformation($"Returning value from distributed cache for key: {queryParameters.CacheKey}");
                return cachedResponse.FromByteArray<ResponseData>();
            }

            // Get from external API
            try
            {
                var response = await _currencyDataRepository.GetAsync(queryParameters);
                response.ExchangeRates = response.ExchangeRates.Where(x => x.Rate.HasValue).ToList();

                await _distributedCache.SetAsync(queryParameters.CacheKey, response.ToByteArray(),
                        new DistributedCacheEntryOptions().SetSlidingExpiration(
                            TimeSpan.FromDays(double.Parse(_config[Constants.Configuration.DistributedCache.SlidingExpirationTimeDays]))
                        ));
                _logger.LogInformation($"Setting value in distributed cache for key: {queryParameters.CacheKey}");

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured during retrieving data from EcbService. Error details: {e.Message}");
                _logger.LogError(e, e.Message);
                return null;
            }
        }
    }
}
