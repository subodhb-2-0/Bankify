using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class AccountResponseModel
    {
        public int p_operationstatus { get; set; } // integer, 
        public string p_operationmessage { get; set; } //character varying, 
        public int p_operationlogid { get; set; } // integer, 
        public string p_transactiontime { get; set; } // character varying
    }
}
