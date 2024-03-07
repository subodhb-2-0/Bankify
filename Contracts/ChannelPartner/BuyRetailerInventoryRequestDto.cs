namespace Contracts.Onboarding
{
    public class BuyRetailerInventoryRequestDto
    {
        public int OrgId { get; set; }
        public int channelId { get; set; }
        //public int cpafNumber { get; set; }
        public int productId { get; set; }
        public int totalLot { get; set; }
        public decimal totalAmount { get; set; }
        public int userid { get; set; }
        public string remark { get; set; }
    }
   
}
