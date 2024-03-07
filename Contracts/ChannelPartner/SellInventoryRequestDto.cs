using Contracts.Common;
namespace Contracts.Onboarding
{
    public class SellInventoryRequestDto: BaseEntityDto
    {
        public string orgId { get; set; }
        public int channelId { get; set; }
        public int productId { get; set; }
        public int cpafNumber { get; set; }
        public string orgName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public decimal packAmt { get; set; }
        public int paymentMode { get; set; }
    }
}
