using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class InsertTokenDataModel
    {
        public long p_userid { get; set; }
        public string p_gettoken { get; set; }
        public string p_ipaddress { get; set; }
    }
}
