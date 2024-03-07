namespace Contracts.Onboarding
{
    public class InventoryDetailsDto
    {
        public int orgId { get; set; }
        public int channelId { get; set; }
        public string channelName { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public int inventorySold { get; set; }
        public int inventoryBought { get; set; }
        public int cpafNumber { get; set; }
        public int inventoryId { get; set; }
    }
}
