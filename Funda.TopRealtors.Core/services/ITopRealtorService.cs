namespace Funda.TopRealtors.Core
{
    public interface ITopRealtorService
    {
        public Task CalculateTopRealtorsAsync(int amountOfRealtorsToCalculate, int amountOfPagesToTraverse, int startPage = 1);
    }
}
