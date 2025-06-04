namespace Funda.TopRealtors.Core
{
    public class FundaApiConfig
    {
        public string ApiKey { get; }
        public int AmountOfRealtorsToRank { get; }

        public FundaApiConfig(string apiKey, int amountOfRealtorsToRank)
        {
            ApiKey = apiKey;
            AmountOfRealtorsToRank = amountOfRealtorsToRank;
        }
    }
}
