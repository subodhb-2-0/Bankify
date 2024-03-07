using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class BillerInfoResponse
    {
        public string responseCode { get; set; }
        public Biller biller { get; set; }
    }
}
