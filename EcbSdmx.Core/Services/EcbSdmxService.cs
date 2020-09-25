using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EcbSdmx.Core.Models.Response;
using EcbSdmx.Core.Services.Abstractions;

namespace EcbSdmx.Core.Services
{
    public class EcbSdmxService : IEcbSdmxService
    {
        private readonly HttpClient _httpClient;

        public EcbSdmxService()
        {
            _httpClient = new HttpClient {BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/service/data/EXR/")};
        }

        public async Task<ApiResponseData> Get(string firstCode, string secondCode, DateTime startDate, DateTime endDate)
        {
            // TODO: to const/config
            var endpoint =
                $"D.{firstCode}.{secondCode}.SP00.A?startPeriod={startDate:yyyy-MM-dd}&endPeriod={endDate:yyyy-MM-dd}&detail=dataonly";
            var response = await _httpClient.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(ApiResponseData));

            return (ApiResponseData)serializer.Deserialize(responseStream);
        }
    }
}
