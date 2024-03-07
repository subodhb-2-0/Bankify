namespace Contracts.PageManagement
{
    public class GetPageDetailResponse
    {
        public string? SerachValue { get; set; }
        public int? PageSize { get; set; }
        public int? TotalRecord { get; set; }
        public int? PageNumber { get; set; }
        public string? OrderBy { get; set; }
        public string? orderByColumn { get; set; }
        public int? RoleId { get; set; }
        public IEnumerable<GetPageDetailDto> pageDetailsDtos { get; set; }
    }
}
