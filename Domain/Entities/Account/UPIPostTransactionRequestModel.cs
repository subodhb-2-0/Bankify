using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class UPIPostTransactionRequestModel
    {
        public Int64 p_paymentid { get; set; } //bigint,
        public int p_depostat { get; set; } //integer, 
        public int p_userid { get; set; } //integer,
        public string p_remarks { get; set; } //character varying,
        public string p_instrumentid { get; set; } //character varying

        //PayOut
        public string p_txntype { get; set; }
        public int p_status { get; set; }
        public string p_utrnumber { get; set; }
        public int p_createdby { get; set; }
        public string p_creatoripaddress { get; set; }

        public string MethodType { get; set; }
    }
}
