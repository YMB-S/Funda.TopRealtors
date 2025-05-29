namespace Funda.TopRealtors.Core
{
    public class TopRealtorService : ITopRealtorService
    {
        private IFundaApiClient apiClient;

        public TopRealtorService(IFundaApiClient apiClient)
        {
            ArgumentNullException.ThrowIfNull(apiClient, nameof(apiClient));
            this.apiClient = apiClient;
        }

        public async Task CalculateTopRealtorsAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1)
        {
            int pageSize = 25;

            for (int currentPage = startPage; currentPage < (startPage + amountOfPagesToTraverse); currentPage++)
            {
                var response = await apiClient.GetListingsAsync(currentPage, pageSize);
                foreach (var fundaObject in response.Objects)
                {
                    Console.WriteLine(fundaObject.MakelaarNaam);
                }
            }
        }
    }
}
