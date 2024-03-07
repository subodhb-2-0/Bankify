namespace Contracts.Report
{
    public class SupplierDto
    {
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public int status { get; set; }
    }
    public class PaymentModeDto
    {
        public int paymentModeId { get; set; }
        public string paymentMode { get; set; }
        public int status { get; set; }
    }
}