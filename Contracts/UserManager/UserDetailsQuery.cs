using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class UserDetailsQuery
    {
        public long userid { get; set; }
        public string loginid { get; set; }

        public string firstname { get; set; }
        public string middlename { get; set; }

        public string lastname { get; set; }

        public int orgid { get; set; }
        public string emailid { get; set; }
        public string mobilenumber { get; set; }
        public string RoleName { get; set; }
        public int roleid { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string ReportsTo { get; set; }

        public int status { get; set; }

        public int creator { get; set; }
        public DateTime creationdate { get; set; }

        public DateTime modificationdate { get; set; }
        public int totalrecord { get; set; }
    }
}
