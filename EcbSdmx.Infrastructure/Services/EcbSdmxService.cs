using EcbSdmx.Core.Domain;
using EcbSdmx.Core.Domain.Response;
using EcbSdmx.Infrastructure.Services.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcbSdmx.Infrastructure.Services
{
    public class EcbSdmxService : IEcbSdmxService
    {
        private readonly HttpClient _httpClient;

        public EcbSdmxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Constants.EcbSdmx.Api.Url);
        }

        public async Task<ApiResponseData> GetAsync(EcbSdmxQueryParameters queryParameters)
        {
            var endpoint = string.Format(Constants.EcbSdmx.Api.QueryParameters, queryParameters.FromCurrency,
                queryParameters.ToCurrency, queryParameters.StartPeriod, queryParameters.EndPeriod);
            var response = await _httpClient.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(ApiResponseData));

            return (ApiResponseData)serializer.Deserialize(responseStream);
        }
    }
}
