using Contracts.Bbps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bbps
{
    public class BbpsBillerListModel
    {
        public string p_billerid { get; set; }
        public int p_serviceproviderid { get; set; }
        public int p_billercategoryid { get; set; }
        public string p_billername { get; set; }
        public int p_adhocpayment { get; set; }
        public string p_coverage { get; set; }
        public int p_fetchrequirement { get; set; }
        public int p_paymentexactness { get; set; }
        public int p_supportbillvalidation { get; set; }
        public int p_supportpendingstatus { get; set; }
        public int p_supportdeemed { get; set; }
        public int p_billertimeout { get; set; }
        public string p_billeramountoptions { get; set; }
        public string p_billerpaymentmode { get; set; }
        public string p_billerdesc { get; set; }
        public int p_status { get; set; }
        public int p_createdby { get; set; }
        public List<ParamInfo> p_billerinputparams { get; set; }
    }
}
