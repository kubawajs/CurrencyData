using System;
using System.Threading.Tasks;
using AutoMapper;
using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using EcbSdmx.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace CurrencyData.Infrastructure.Repositories
{
    public class CurrencyDataRepository : ICurrencyDataRepository
    {
        private readonly IEcbSdmxService _ecbSdmxService;
        private readonly IMapper _mapper;

        public CurrencyDataRepository(IEcbSdmxService ecbSdmxService, IMapper mapper)
        {
            _ecbSdmxService = ecbSdmxService;
            _mapper = mapper;
        }

        public async Task<ApiResponseDataDTO> GetAsync(string inCurrency, string outCurrency, DateTime startDate,
            DateTime endDate)
        {
            var response = await _ecbSdmxService.GetAsync(inCurrency, outCurrency, startDate, endDate);
            return _mapper.Map<ApiResponseDataDTO>(response);
        }
    }
}