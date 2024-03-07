using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.OTP
{
    public class OTPDetailsDto
    {
        public string LoginId { get; set; }

        public string OTP { get; set; }

        public int ModuleId { get; set; }

        public int ExpiredTimeInSecond { get; set; }

        public int Creator { get; set; }
        public int CreationDate { get; set; }

        public string ImeiNo { get; set; }
    }
}
