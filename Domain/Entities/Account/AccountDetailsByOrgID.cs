using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class AccountDetailsByOrgID : BaseEntity
    {
        public int p_count { get; set; }
        public string organisation { get; set; }
        public string p_acountdescription { get; set; }
        public string p_createdby { get; set; }
        public DateTime p_creationdate { get; set; }
        public string p_modifiedby { get; set; }
        public DateTime p_modificationdate { get; set; }
    }
}
