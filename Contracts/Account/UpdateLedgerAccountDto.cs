using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class UpdateLedgerAccountDto
    {
        public int orgtypeaccid { get; set; }
        public int orgtypeid { get; set; }
        public int accid { get; set; }
        public string accdescription { get; set; }
        public int modifier { get; set; }
    }
}
