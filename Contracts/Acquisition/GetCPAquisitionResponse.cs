namespace Contracts.Acquisition
{
    public class GetCPAquisitionResponse
    {
        public int? PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int? PageNumber { get; set; }
        public string OrderBy { get; set; }
        public string orderByColumn { get; set; }
        public IEnumerable<GetCPAquisitionDto> getCPAquisitionDtos { get; set; }
    }
}
