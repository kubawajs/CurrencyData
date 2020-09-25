using System;
using System.Threading.Tasks;
using EcbSdmx.Core.Models.Response;

namespace EcbSdmx.Infrastructure.Services.Abstractions
{
    public interface IEcbSdmxService
    {
        Task<ApiResponseData> Get(string firstCode, string secondCode, DateTime startDate, DateTime endDate);
    }
}
