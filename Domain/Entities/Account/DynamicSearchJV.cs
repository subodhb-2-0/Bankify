using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class DynamicSearchJV : BaseEntity
    {
        public long totalcount { get; set; }
        public int jvno { get; set; }
        public string jvname { get; set; }
        public string description { get; set; }
        public DateTime jvdate { get; set; }
        public int status { get; set; }
    }
}
