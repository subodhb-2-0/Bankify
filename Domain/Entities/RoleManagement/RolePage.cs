using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RoleManagement
{
    public class RolePage
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
    }
}
