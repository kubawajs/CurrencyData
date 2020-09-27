using AutoMapper;
using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using EcbSdmx.Infrastructure.Services.Abstractions;
using System.Threading.Tasks;
using EcbSdmx.Core.Domain;

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

        public async Task<ResponseData> GetAsync(QueryParameters queryParameters)
        {
            var ecbSdmxQueryParams = _mapper.Map<EcbSdmxQueryParameters>(queryParameters);
            var response = await _ecbSdmxService.GetAsync(ecbSdmxQueryParams);
            return _mapper.Map<ResponseData>(response);
        }
    }
}