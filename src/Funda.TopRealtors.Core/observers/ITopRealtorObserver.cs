namespace Funda.TopRealtors.Core
{
    public interface ITopRealtorObserver
    {
        public void NotifyOfNewListingsPerRealtor(Dictionary<int, Realtor> updatedRealtors);
    }
}
