namespace Funda.TopRealtors.Core
{
    public interface IFundaApiClient
    {
        public Task<FundaApiResponse> GetListingsAsync(int page, int pageSize);
        public Task<FundaApiResponse> GetListingsWithGardenAsync(int page, int pageSize);
    }
}
