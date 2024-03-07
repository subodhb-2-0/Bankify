using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Role
{
    public class RolePageDto : RoleDto
    {
        [Required]
        public List<RolePages> RolePages
        {
            get;
            set;

        }
    }

    public class RolePages
    {
        public int PageId { get; set; }
        public int Mode { get; set; }
        //public int Status { get; set; }
        public int PageStatus { get; set; }
    }
}
