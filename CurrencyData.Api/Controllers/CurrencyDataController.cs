using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyData.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace CurrencyData.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyDataController
    {
        private readonly ILogger<CurrencyDataController> _logger;
        private readonly ICurrencyDataService _currencyDataService;

        public CurrencyDataController(ILogger<CurrencyDataController> logger, ICurrencyDataService currencyDataService, IDistributedCache distributedCache)
        {
            _logger = logger;
            _currencyDataService = currencyDataService;
        }

        // TODO: authorized with api key
        // TODO: handle error on get
        // [Authorize]
        /// <summary>
        /// Returns collection of currency data for specific currencies and date interval
        /// </summary>
        /// <param name="currencyCodes"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "currencyCodes", "startDate", "endDate" })]
        public async Task<ActionResult<string>> Get([FromQuery] Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate, string apiKey)
        {
            var now = DateTime.Now;
            if (now < startDate || now < endDate)
            {
                _logger.LogWarning("Future start or end date.");
                return new NotFoundResult();
            }

            var result = await _currencyDataService.GetCurrencies(currencyCodes, startDate, endDate);
            if (result == null)
            {
                _logger.LogWarning($"No results found for given parameters: {currencyCodes}, {startDate}, {endDate}");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Successfully retrieved date for given parameters: {currencyCodes}, {startDate}, {endDate}");
            return new JsonResult(result);
        }
    }
}
