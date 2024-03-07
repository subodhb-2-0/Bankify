using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OTPManager
{
    public class OTPDetails : BaseEntity
    {
        public string LoginId { get; set; }

        public string OTP { get; set; }

        public int ModuleId { get; set; }

        public int ExpiredTimeInSecond { get; set; }

        public long Creator { get; set; }
        public DateTime CreationDate { get; set; }

        public string ImeiNo { get; set; }
    }
}
