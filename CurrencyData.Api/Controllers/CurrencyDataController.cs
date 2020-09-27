using CurrencyData.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Caching.Memory;

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
        /// Returns collection of currency data for specific currencies and date interval
        /// </summary>
        /// <param name="currencyCodes"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "currencyCodes", "startDate", "endDate" })]
        public async Task<ActionResult<string>> Get([FromQuery] Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            var now = DateTime.Now;
            if (now < startDate || now < endDate)
            {
                _logger.LogWarning("Future start or end date.");
                return new NotFoundResult();
            }

            if (currencyCodes.Count == 0)
            {
                _logger.LogWarning("No currency specified.");
                return new NotFoundResult();
            }

            if (!currencyCodes.Any())
            {
                return null;
            }
            var currencyKey = currencyCodes.First().Key;
            var cacheKey = GenerateCacheKey(currencyKey, currencyCodes[currencyKey], startDate.ToString("yyyy-MM-dd"),
                endDate.ToString("yyyy-MM-dd"));
            
            if (!_memoryCache.TryGetValue(cacheKey, out var cacheEntry))
            {
                var result = await _currencyDataService.GetCurrencies(currencyKey, currencyCodes[currencyKey], startDate, endDate);
                _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30)));
                _logger.LogInformation($"Added entry to in-memory cache: {cacheKey}");
                cacheEntry = result;
            }

            if (cacheEntry == null)
            {
                _logger.LogWarning($"No results found for given parameters: {currencyCodes}, {startDate}, {endDate}");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Successfully retrieved date for given parameters: {currencyCodes}, {startDate}, {endDate}");
            return new JsonResult(cacheEntry);
        }

        public static string GenerateCacheKey(string inCode, string outCode, string startDate, string endDate) =>
            $"{inCode};{outCode};{startDate};{endDate}";
    }
}
