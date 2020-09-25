using CurrencyData.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyData.Api.Controllers
{
    // TODO: swagger
    [ApiController]
    [Route("[controller]")]
    public class CurrencyDataController
    {
        private readonly ILogger<CurrencyDataController> _logger;
        private readonly ICurrencyDataService _currencyDataService;

        public CurrencyDataController(ILogger<CurrencyDataController> logger, ICurrencyDataService currencyDataService)
        {
            _logger = logger;
            _currencyDataService = currencyDataService;
        }

        // TODO: authorized with api key
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
        public async Task<ActionResult<string>> Get([FromQuery] Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate, string apiKey)
        {
            var now = DateTime.Now;
            if(now < startDate || now < endDate)
                return new NotFoundResult();

            var key = currencyCodes.First().Key;

            var result = await _currencyDataService.GetCurrencies(key, currencyCodes[key], startDate, endDate);

            if (result == null)
                return new NotFoundResult();

            return new JsonResult(result);
        }
    }
}
