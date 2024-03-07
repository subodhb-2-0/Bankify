namespace Contracts.PageManagement
{
    public class GetPageDetailDto
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string PageDesc { get; set; }
        public string PagePath { get; set; }
        public int PageSource { get; set; }
        public int PageModeId { get; set; }
        public int ParentId { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
    }
}
