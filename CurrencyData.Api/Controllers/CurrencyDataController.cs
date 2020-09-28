using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyData.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyDataController
    {
        private readonly ILogger<CurrencyDataController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly ICurrencyDataService _currencyDataService;

        public CurrencyDataController(ILogger<CurrencyDataController> logger, IMemoryCache memoryCache,
            ICurrencyDataService currencyDataService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _currencyDataService = currencyDataService;
        }

        /// <summary>
        /// Returns collection of currency data for specified currencies and date interval
        /// </summary>
        /// <param name="currencyCodes">Currency codes dictionary. Key stands for "in" currency, value is a result currency, e.g. currencyCodes[USD]=EUR will return exchange rates for USD to EUR.</param>
        /// <param name="startDate">Start date for currency rate.</param>
        /// <param name="endDate">End date for currency rate.</param>
        /// <returns>Currency exchange rates for given parameters.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData), 200)]
        [ProducesResponseType(404)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "currencyCodes", "startDate", "endDate" })]
        public async Task<ActionResult<ResponseData>> Get([FromQuery] Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            var now = DateTime.Now;
            if (now < startDate || now < endDate)
            {
                _logger.LogWarning("Future start or end date.");
                return new NotFoundResult();
            }

            var queryParams = QueryParameters.Create(currencyCodes, startDate, endDate);
            if (queryParams.FromCurrency == null || queryParams.ToCurrency == null)
            {
                _logger.LogWarning("No currency specified.");
                return new NotFoundResult();
            }

            if (_memoryCache.TryGetValue(queryParams.CacheKey, out var cacheEntry))
            {
                _logger.LogInformation($"[MEMORY CACHE] Successfully retrieved data for given parameters: {currencyCodes}, {startDate}, {endDate}");
                return new JsonResult(cacheEntry);
            }

            var result = await _currencyDataService.GetCurrencies(queryParams);
            if (result == null)
            {
                _logger.LogWarning($"No results found for given parameters: {currencyCodes}, {startDate}, {endDate}");
                return new NotFoundResult();
            }

            _memoryCache.Set(queryParams.CacheKey, result, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(30)));
            _logger.LogInformation($"Added entry to in-memory cache: {queryParams.CacheKey}");
            _logger.LogInformation($"Successfully retrieved data for given parameters: {currencyCodes}, {startDate}, {endDate}");

            return new JsonResult(result);
        }
    }
}
