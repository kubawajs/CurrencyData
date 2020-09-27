using System.Threading.Tasks;
using EcbSdmx.Core.Domain;
using EcbSdmx.Core.Domain.Response;

namespace EcbSdmx.Infrastructure.Services.Abstractions
{
    public interface IEcbSdmxService
    {
        Task<ApiResponseData> GetAsync(EcbSdmxQueryParameters queryParameters);
    }
}
