namespace Funda.TopRealtors.Core
{
    public class FundaApiConfig
    {
        public string ApiKey { get; }

        public FundaApiConfig(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
