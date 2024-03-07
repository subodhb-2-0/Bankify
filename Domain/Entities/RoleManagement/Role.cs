using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RoleManagement
{
    public class Role : BaseEntity
    {
        public int  RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int Status { get; set; }

        public int Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public int? Modifier { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
