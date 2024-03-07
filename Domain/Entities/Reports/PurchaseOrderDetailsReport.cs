
namespace Domain.Entities.Reports
{
    public class PurchaseOrderDetailsReport
    {
        public int TotalCount { get; set; }
        public int OrderId { get; set; }
        public string DeviceName { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalQty { get; set; }
        public string Tracking_number    { get; set; }
        public string Delivery_Partner { get; set; }
        public int Totalamt { get; set; }
        public int Order_Status { get; set; }

    }
}
