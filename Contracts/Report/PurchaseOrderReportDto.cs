
namespace Contracts.Report
{
    public class PurchaseOrderReportDto 
    {
        public int? OrderStatus { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
