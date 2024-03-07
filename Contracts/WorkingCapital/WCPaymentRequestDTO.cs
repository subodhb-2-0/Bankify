namespace Contracts.WorkingCapital
{
    public class WCPaymentRequestDTO
    {
        public int orgId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int searchById { get; set; }
        public int searchByValue { get; set; }
    }
}