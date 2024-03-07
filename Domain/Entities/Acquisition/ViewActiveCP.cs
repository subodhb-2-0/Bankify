using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Acquisition
{
    public class ViewActiveCP : BaseEntity
    {
        public long orgid { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public int org_type { get; set; }
        public string orgtypename { get; set; }
        public int parentorgid { get; set; }
        public DateTime firsttrxdate { get; set; }
        public DateTime lasttrxdate { get; set; }
        public int status { get; set; }
        public int creator { get; set; }
        public DateTime creationdate { get; set; }
        public int modifier { get; set; }
        public DateTime modificationdate { get; set; }
        public int productid { get; set; }
        public int enrollmentfeecharged { get; set; }
        public int secdeposit { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int pgsstatus { get; set; }
        public int preferredlanguage { get; set; }
        public int classoftrade { get; set; }
        public DateTime activationdate { get; set; }
        public int fieldstaffid { get; set; }
        public string secdepositchequeno { get; set; }
        public int secdepositbankid { get; set; }
        public string secdepositaccount { get; set; }
        public string paidby { get; set; }
        public string payment_option { get; set; }
        public int onboardstage { get; set; }
        public int kyc_status { get; set; }

    }
}
