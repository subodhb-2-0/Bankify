using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class LedgerDetailsById : BaseEntity
    {
        public DateTime vretdate { get; set; }
        public string vorgname { get; set; }
        public string vorgcode { get; set; }
        public string vaccounttype { get; set; }
        public int vdebit { get; set; }
        public int vcredit { get; set; }
        public string vnarration { get; set; }
        public DateTime vcreationdate { get; set; }
        public int vsr_no { get; set; }
    }
}
