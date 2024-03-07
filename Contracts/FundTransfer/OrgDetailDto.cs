namespace Contracts.FundTransfer
{
    public class OrgDetailDto
    {
        public int orgId { get; set; }
        public string orgName { get; set; }
        public string orgCode { get; set; }
        public decimal orgLimit { get; set; }
        public decimal walletBalance { get; set; }
    }
}