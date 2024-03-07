using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class UpdateUsersTokenDto
    {
        public string ipaddress { get; set; }
        public long userid { get; set; }
    }
}
