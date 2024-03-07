using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Role
{
    public class RoleAccessDto
    {
        public int RolePageId { get; set; }
        public int RoleId { get; set; }
        public int PageId { get; set; }
        public int Mode { get; set; }
        //public int Status { get; set; }
        public int PageStatus { get; set; }
        public int Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Modifier { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string PageName { get; set; }
        public string PageDesc { get; set; }
        public string PagePath { get; set; }
        public int CorpId { get; set; }
        public int? ParentId { get; set; }
        public int? SortOrder { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
    }
}
