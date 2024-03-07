
namespace Contracts.Response
{
    public class PaginationResponseModel
    {
        public int? PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int? PageNumber { get; set; }
        public string OrderBy { get; set; }
        public string orderByColumn { get; set; }
    }
}
