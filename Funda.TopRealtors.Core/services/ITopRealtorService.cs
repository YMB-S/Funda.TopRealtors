namespace Funda.TopRealtors.Core
{
    public interface ITopRealtorService
    {
        public Task<List<Realtor>> CalculateTopRealtorsAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1);
        public Task<List<Realtor>> CalculateTopRealtorsWithGardensAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1);
        public void AddObserver(ITopRealtorObserver observer);
        public void RemoveObserver(ITopRealtorObserver observer);
    }
}
