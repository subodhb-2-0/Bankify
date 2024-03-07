using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RoleManagement
{
    public class ListofRole
    {
        public int roleid { get; set; }
        public string rolename { get; set; }
        public string roledescription { get; set; }
        public int status { get; set; }
        public int creator { get; set; }
        public DateTime creationdate { get; set; }
        public int modifier { get; set; }
        public DateTime modificationdate { get; set; }
    }
}
