namespace Funda.TopRealtors.Core
{
    public interface IFundaApiClient
    {
        public Task<FundaApiResponse> GetListingsAsync(int page, int pageSize);
    }
}
