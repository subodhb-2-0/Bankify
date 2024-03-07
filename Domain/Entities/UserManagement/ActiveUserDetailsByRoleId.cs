using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class ActiveUserDetailsByRoleId
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobilenumber { get; set; }
        public DateTime lastlogindate { get; set; }
        public int userid { get; set; }
    }
}
