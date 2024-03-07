using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class EditCpOnBoardByMailDto
    {
        public int orgid { get; set; }
        public string? ref_param1 { get; set; }
        public string? otp { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mothersname { get; set; }
        public string fathersname { get; set; }
        public int userid { get; set; }
        public string Email { get; set; }
    }
}
