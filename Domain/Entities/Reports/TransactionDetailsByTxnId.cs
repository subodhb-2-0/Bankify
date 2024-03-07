using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class TransactionDetailsByTxnId
    {
        public long p_transactionid { get; set; }
        public DateTime p_transactiondate { get; set; }
        public string p_service { get; set; }
        public string p_supplier { get; set; }
        public string p_serviceprovider { get; set; }
        public string p_retailercode { get; set; }
        public string p_retailername { get; set; }
        public string p_distributorcode { get; set; }
        public string p_distributorname { get; set; }
        public string p_superdistributorcode { get; set; }
        public string p_superdistributor { get; set; }
        public double p_txnamount { get; set; }
        public double p_servicecharge { get; set; }
        public string p_suppliertxnreference { get; set; }
        public string p_txntype { get; set; }
        public string p_txnmode { get; set; }
        public string p_customermobile { get; set; }
        public string p_customername { get; set; }
        public string p_operatororbiller { get; set; }
        public long p_crn { get; set; }
        public string p_receivername { get; set; }
        public string p_accountnumber { get; set; }
        public string p_bankname { get; set; }
        public string p_deviceserialnumber { get; set; }
        public string p_devicetype { get; set; }
        public string p_cardtype { get; set; }
        public DateTime duedate { get; set; }
        public double latepayment { get; set; }
        public string billperiod { get; set; }
        public string geotag { get; set; }
        public string rrn { get; set; }
        public string complaintid { get; set; }
        public string statu { get; set; }
        public DateTime p_creationdate { get; set; }
    }
}
