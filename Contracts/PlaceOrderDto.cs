namespace Contracts
{
    public class PlaceOrderDto
    {
        public int OrgId {  get; set; }
        public int UserId {  get; set; }
        public string DeviceName {  get; set; }
        public long OrderId {  get; set; }
        public string OrderDate {  get; set; }
        public string Tracking_Number {  get; set; }
        public double Price {  get; set; }
        public int TotalQty {  get; set; }
        public double TotalAmt {  get; set; }
        public string DeliveryAddress {  get; set; }
        public string Delivery_Partner {  get; set; }
        public string Order_Status {  get; set; }

    }
}
