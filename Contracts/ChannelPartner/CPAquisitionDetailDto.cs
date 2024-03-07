namespace Contracts.Onboarding
{
    public class CPAquisitionDetailDto
    {
        public int orgId { get; set; }
        public string orgcode { get; set; }
        public string orgName { get; set; }
        public int channelType { get; set; }
        public string productName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public DateTime activationDate { get; set; }
        public string businessAddress { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int status { get; set; }
    }
}
