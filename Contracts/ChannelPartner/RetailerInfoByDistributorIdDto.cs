using Contracts.Common;

namespace Contracts.Onboarding
{
    public class RetailerInfoByDistributorIdDto
    {
        public int totalcount { get; set; }
        public string retailercode { get; set; }
        public string orgname { get; set; }
        public double balancetransferamount { get; set; }
        public double retailerbalance { get; set; }
        public double transferpercentage { get; set; }
    }
}
