using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class PaymentChannelInfo
    {
        public string paymentChannelName { get; set; }
        public string minAmount { get; set; }
        public string maxAmount { get; set; }
    }
}
