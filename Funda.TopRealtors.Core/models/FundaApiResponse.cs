namespace Funda.TopRealtors.Core
{
    public class FundaApiResponse
    {
        public List<FundaObject> Objects { get; set; }

        public class FundaObject
        {
            public int MakelaarID { get; set; }
            public string MakelaarNaam { get; set; }
        }
    }
}
