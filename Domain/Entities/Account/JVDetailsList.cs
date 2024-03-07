using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class JVDetailsList : BaseEntity
    {
        public int jvdetailsid { get; set; }
        public int jvno { get; set; }
        public string jvname { get; set; }
        public string description { get; set; }
        public DateTime jvdate { get; set; }
        public int status { get; set; }
        public int orgTypeId { get; set; }

        public string org_type { get; set; }
        public int accountId { get; set; }
        public string accountType { get; set; }
        public int orgId { get; set; }
        public string orgName { get; set; }
        public int amount { get; set; }
        public string JVType { get; set; }
        public int jvdtlsStatus { get; set; }
        public string narration { get; set; }
        public DateTime creationdate { get; set; }

    }
}
