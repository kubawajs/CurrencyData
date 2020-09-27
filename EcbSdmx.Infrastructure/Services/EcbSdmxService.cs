using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EcbSdmx.Core.Domain;
using EcbSdmx.Core.Domain.Response;
using EcbSdmx.Infrastructure.Services.Abstractions;

namespace EcbSdmx.Infrastructure.Services
{
    public class EcbSdmxService : IEcbSdmxService
    {
        private readonly HttpClient _httpClient;

        public EcbSdmxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/service/data/EXR/");
        }

        public async Task<ApiResponseData> GetAsync(EcbSdmxQueryParameters queryParameters)
        {
            // TODO: to const/config
            var endpoint =
                $"D.{queryParameters.FromCurrency}.{queryParameters.ToCurrency}.SP00.A?startPeriod={queryParameters.StartPeriod:yyyy-MM-dd}&endPeriod={queryParameters.EndPeriod:yyyy-MM-dd}&detail=dataonly";
            var response = await _httpClient.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(ApiResponseData));

            return (ApiResponseData)serializer.Deserialize(responseStream);
        }
    }
}
