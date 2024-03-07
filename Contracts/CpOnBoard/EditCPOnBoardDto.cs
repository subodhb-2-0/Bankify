using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class EditCPOnBoardDto
    {
        public int orgid { get; set; }
        public string? mobilenumber { get; set; }
        public string? otp { get; set; }
        public int? userId { get; set; }
        public int? payoutBankId { get; set; }
    }
}
