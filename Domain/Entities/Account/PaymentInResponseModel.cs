using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class PaymentInResponseModel
    {
        public int p_operationstatus { get; set; } // integer,
        public string p_operationmessage { get; set; } //character varying,
        public double p_operationlogid { get; set; } //integer, p
        public long _paymentid { get; set; } //bigint
    }
}
