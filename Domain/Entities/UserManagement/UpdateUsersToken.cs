using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class UpdateUsersToken
    {
        public string ipaddress { get; set; }
        public long userid { get; set; }
    }
}
