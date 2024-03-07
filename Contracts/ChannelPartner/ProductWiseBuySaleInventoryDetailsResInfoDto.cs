using Contracts.Common;

namespace Contracts.Onboarding
{
    public class ProductWiseBuySaleInventoryDetailsResInfoDto 
    {
        public string productname { get; set; }
        public string channelname { get; set; }
        public int buycount { get; set; }
        public int salecount { get; set; }
    }
}
