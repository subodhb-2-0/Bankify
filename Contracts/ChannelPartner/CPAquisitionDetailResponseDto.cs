namespace Contracts.Onboarding
{
    public class CPAquisitionDetailResponseDto
    {
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public string orderByColumn { get; set; }
        public IEnumerable<CPAquisitionDetailDto>  cPAquisitionDetailDtos  { get; set; }
    }
}
