using AutoMapper;
using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using EcbSdmx.Infrastructure.Services.Abstractions;
using System;
using System.Threading.Tasks;

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

        public async Task<ApiResponseDataDTO> GetAsync(string inCurrency, string outCurrency, DateTime startDate, DateTime endDate)
        {
            // TODO: caching to external service
            var response = await _ecbSdmxService.GetAsync(inCurrency, outCurrency, startDate, endDate);
            var responseDTO = _mapper.Map<ApiResponseDataDTO>(response);

            return responseDTO;
        }
    }
}
