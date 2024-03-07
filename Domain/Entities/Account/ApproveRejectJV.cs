using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class ApproveRejectJV : BaseEntity
    {
        public int jvno { get; set; }
        public int status { get; set; }
        public int modifier { get; set; }
        public int currentstatus { get; set; }
    }
}
