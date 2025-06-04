namespace Funda.TopRealtors.Core
{
    public class TopRealtorService : ITopRealtorService
    {
        private IFundaApiClient apiClient;
        private List<ITopRealtorObserver> observers;

        public TopRealtorService(IFundaApiClient apiClient)
        {
            ArgumentNullException.ThrowIfNull(apiClient, nameof(apiClient));
            this.apiClient = apiClient;

            this.observers = new();
        }

        public void AddObserver(ITopRealtorObserver observer)
        {
            this.observers.Add(observer);
        }
        public void RemoveObserver(ITopRealtorObserver observer)
        {
            this.observers.Remove(observer);
        }

        public async Task<List<Realtor>> CalculateTopRealtorsAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1)
        {
            Dictionary<int, Realtor> listingsPerRealtor = await CalculateListingsPerRealtor(amountOfPagesToTraverse, apiClient.GetListingsAsync);
            return GetTopRealtorsFromListings(listingsPerRealtor, amountOfRealtorsToCalculate);
        }

        public async Task<List<Realtor>> CalculateTopRealtorsWithGardensAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1)
        {
            Dictionary<int, Realtor> listingsPerRealtor = await CalculateListingsPerRealtor(amountOfPagesToTraverse, apiClient.GetListingsWithGardenAsync);
            return GetTopRealtorsFromListings(listingsPerRealtor, amountOfRealtorsToCalculate);
        }

        private async Task<Dictionary<int, Realtor>> CalculateListingsPerRealtor(int amountOfPagesToTraverse, Func<int, int, Task<FundaApiResponse>> listingsFetcher, int startPage = 1)
        {
            Dictionary<int, Realtor> listingsPerRealtor = new();

            int pageSize = 25;
            for (int currentPage = startPage; currentPage < (startPage + amountOfPagesToTraverse); currentPage++)
            {
                var response = await listingsFetcher(currentPage, pageSize);
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

                this.NotifyObserversOfNewlyCalculatedListingsPerRealtor(listingsPerRealtor);
                await Task.Delay(1000); // Make sure we don't overload the API
            }

            return listingsPerRealtor;
        }

        private List<Realtor> GetTopRealtorsFromListings(Dictionary<int, Realtor> listingsPerRealtor, int amountOfRealtorsToCalculate)
        {
            return listingsPerRealtor
                .Values
                .OrderByDescending(r => r.AmountOfListings)
                .Take(amountOfRealtorsToCalculate)
                .ToList();
        }

        private void NotifyObserversOfNewlyCalculatedListingsPerRealtor(Dictionary<int, Realtor> updatedRealtors)
        {
            foreach(var observer in this.observers)
            {
                observer.NotifyOfNewListingsPerRealtor(updatedRealtors);
            }
        }
    }
}
