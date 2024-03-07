using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class Inventory: BaseEntity
    {
        public int inventoryId { get; set; }
        public int OrgId { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int totalInventory { get; set; }
        public decimal totalAmt { get; set; }
        public DateTime purchaseDate { get; set; }
    }

}