using Contracts.Common;

namespace Contracts.PageManagement
{
    public class AddPageDetailDto
    {
        //public string PageType { get; set; }  
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string PagePath { get; set; }
        public int? ParentId { get; set; }
        public int Creator { get; set; }
    }
}
