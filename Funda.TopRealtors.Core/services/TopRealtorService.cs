using Funda.TopRealtors.Core.clients;

namespace Funda.TopRealtors.Core.services
{
    public class TopRealtorService : ITopRealtorService
    {
        private IFundaApiClient apiClient;

        public TopRealtorService(IFundaApiClient apiClient)
        {
            ArgumentNullException.ThrowIfNull(apiClient, nameof(apiClient));

            this.apiClient = apiClient;
        }

        public string Test()
        {
            return "Working";
        }
    }
}
