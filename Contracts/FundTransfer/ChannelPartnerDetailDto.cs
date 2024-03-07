namespace Contracts.FundTransfer
{
    public class ChannelPartnerDetailDto
    {
        //public int orgId { get; set; }
        public int distributorid { get; set; }
        public string orgName { get; set; }
        //public int orgCode { get; set; }
        public string orgCode { get; set; }
        //public int status { get; set; }
        public string status { get; set; }
        public string bussinessAddress { get; set; }
        public string pinCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string latlog { get; set; }
        public double retailerbalance { get; set; }
    }
}