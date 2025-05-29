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

        public async Task<List<Realtor>> CalculateTopRealtorsAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1)
        {
            Dictionary<int, Realtor> listingsPerRealtor = await CalculateListingsPerRealtor(amountOfPagesToTraverse);

            var topRealtors = listingsPerRealtor
                .Values
                .OrderByDescending(r => r.AmountOfListings)
                .Take(amountOfRealtorsToCalculate)
                .ToList();

            return topRealtors;
        }

        private async Task<Dictionary<int, Realtor>> CalculateListingsPerRealtor(int amountOfPagesToTraverse, int startPage = 1)
        {
            Dictionary<int, Realtor> listingsPerRealtor = new();

            int pageSize = 25;
            for (int currentPage = startPage; currentPage < (startPage + amountOfPagesToTraverse); currentPage++)
            {
                var response = await apiClient.GetListingsAsync(currentPage, pageSize);
                foreach (var fundaObject in response.Objects)
                {
                    Realtor? realtor;
                    if (listingsPerRealtor.TryGetValue(fundaObject.MakelaarId, out realtor))
                    {
                        listingsPerRealtor[fundaObject.MakelaarId].AmountOfListings += 1;
                    }
                    else
                    {
                        listingsPerRealtor.Add(fundaObject.MakelaarId, new Realtor
                        {
                            RealtorID = fundaObject.MakelaarId,
                            RealtorName = fundaObject.MakelaarNaam,
                            AmountOfListings = 1
                        });
                    }
                }

                await Task.Delay(1000); // Make sure we don't overload the API
            }

            return listingsPerRealtor;
        }
    }
}
