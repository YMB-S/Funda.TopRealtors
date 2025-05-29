
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

            var response = await this.httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<FundaApiResponse>(json);

            if (deserialized == null) { throw new JsonException("Deserialization resulted in null. The JSON input might be invalid or empty."); }
            return deserialized;
        }
    }
}
