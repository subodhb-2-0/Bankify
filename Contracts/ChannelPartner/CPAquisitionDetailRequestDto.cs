namespace Contracts.Onboarding
{
    public class CPAquisitionDetailRequestDto
    {
        public int orgId { get; set; }
       // public int status { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string productName { get; set; }
        public string searchById { get; set; }
        public string searchByName { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
