using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class UserByMobileAndType
    {
        public int userid { get; set; }
        public int orgid { get; set; }
        public string loginid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
    }
}
