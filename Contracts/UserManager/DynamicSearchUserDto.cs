using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class DynamicSearchUserDto
    {
        public string p_loginid { get; set; }
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
    }
}
