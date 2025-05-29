namespace Funda.TopRealtors.Core
{
    public interface ITopRealtorService
    {
        public Task<List<Realtor>> CalculateTopRealtorsAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1);
    }
}
