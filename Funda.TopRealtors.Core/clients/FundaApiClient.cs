
using System;
using System.Net.Http;
using System.Text.Json;

namespace Funda.TopRealtors.Core
{
    public class FundaApiClient : IFundaApiClient
    {
        HttpClient httpClient;
        string apiKey;

        public FundaApiClient(HttpClient httpClient, FundaApiConfig config)
        {
            this.httpClient = httpClient;
            this.apiKey = config.ApiKey;
        }

        public async Task<FundaApiResponse> GetListingsAsync(int page, int pageSize)
        {
            var url = $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{this.apiKey}/?type=koop&zo=/amsterdam/&page={page}&pagesize={pageSize}";
            return await this.GetListingsFromEndpointAsync(url);
        }

        public async Task<FundaApiResponse> GetListingsWithGardenAsync(int page, int pageSize)
        {
            var url = $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{this.apiKey}/?type=koop&zo=/amsterdam/tuin/&page={page}&pagesize={pageSize}";
            return await this.GetListingsFromEndpointAsync(url);
        }

        private async Task<FundaApiResponse> GetListingsFromEndpointAsync(string endpointUrl)
        {
            var response = await this.httpClient.GetAsync(endpointUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<FundaApiResponse>(json);

            if (deserialized == null) { throw new JsonException("Deserialization resulted in null. The JSON input might be invalid or empty."); }
            return deserialized;
        }
    }
}
