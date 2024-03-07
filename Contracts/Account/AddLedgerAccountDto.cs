using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public  class AddLedgerAccountDto 
    {
        public int orgtypeid { get; set; }
        public int accid { get; set; }
        public string accname { get; set; }
        public string accdescription { get; set; }
        public string? org_type { get; set; }


    }
   
}
