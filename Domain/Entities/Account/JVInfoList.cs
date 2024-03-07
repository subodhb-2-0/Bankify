using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class JVInfoList : BaseEntity
    {
        public int jvno { get; set; }
        public string jvname { get; set; }
        public string description { get; set; }
        public DateTime jvdate { get; set; }
        public int status { get; set; }
    }
}
