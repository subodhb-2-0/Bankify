using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class DynamicSearchUserModel
    {
        public string p_loginid { get; set; }
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
    }
}
