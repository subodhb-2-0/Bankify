using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class EmailOtpVerificationDto
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mothersname { get; set; }
        public string fathersname { get; set; }
        public int userid { get; set; }
        public string Email { get; set; }
    }
}
