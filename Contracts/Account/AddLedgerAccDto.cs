using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class AddLedgerAccDto
    {
        public int orgtypeid { get; set; }
        public string accdescription { get; set; }
        public string accname { get; set; }
    }
}
