using Funda.TopRealtors.Core;

namespace Funda.TopRealtors.RealtorConsole
{
    public class TopRealtorObserverConsole : ITopRealtorObserver
    {
        int amountOfRealtorsToRank;

        public TopRealtorObserverConsole(FundaApiConfig config)
        {
            amountOfRealtorsToRank = config.AmountOfRealtorsToRank;
        }

        public void NotifyOfNewListingsPerRealtor(Dictionary<int, Realtor> updatedRealtors)
        {
            Console.Clear();
            foreach (var realtor in updatedRealtors
                 .OrderByDescending(kv => kv.Value.AmountOfListings)
                 .Take(this.amountOfRealtorsToRank))
            {
                Console.WriteLine($"{realtor.Value.RealtorName}, Id: {realtor.Key}, amount of listings: {realtor.Value.AmountOfListings}");
            }
        }
    }
}
