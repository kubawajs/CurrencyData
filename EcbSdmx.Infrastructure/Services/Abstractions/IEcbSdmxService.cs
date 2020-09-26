using System;
using System.Threading.Tasks;
using EcbSdmx.Core.Domain.Response;

namespace EcbSdmx.Infrastructure.Services.Abstractions
{
    public interface IEcbSdmxService
    {
        Task<ApiResponseData> GetAsync(string firstCode, string secondCode, DateTime startDate, DateTime endDate);
    }
}
