namespace Contracts.WorkingCapital
{
    public class PaymentDto
    {
        public int orgId { get; set; }
        public string orgName { get; set; }
        public decimal amount { get; set; }
        public string paymentDate { get; set; }
        public string depositDate { get; set; }
        public string depositTime { get; set; }
        public string bankPayInSlip { get; set; }
        public string instrumentNumber { get; set; }
        public string seviceCharge { get; set; }
        public string pgCharge { get; set; }
        public string wiseIFSC { get; set; }
        public int transferByOrgId { get; set; }
        public string payeeName { get; set; }
        public int status { get; set; }
        public string vpa { get; set; }
        public string remarks { get; set; }
    }
}