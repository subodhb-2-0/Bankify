using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class AllpaymentdetailsResponse
    {
        public long totalcount { get; set; }
        public long paymentid { get; set; }
        public DateTime creationdate { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public double amount { get; set; }
        public int paymentmode { get; set; }
        public string instrumentnumber { get; set; }
        public string bankaccount { get; set; }
        public string issuingifsccode { get; set; }
        public int status { get; set; }
        public DateTime modificationdate { get; set; }
        public string bankname { get; set; }
        public string bankrefno { get; set; }
        public int orgtype { get; set; }
        public int pgid { get; set; }
        public string remark { get; set; }
        public string bankpayinslip { get; set; }
    }
}
