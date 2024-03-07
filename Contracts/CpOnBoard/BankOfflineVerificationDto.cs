using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class BankOfflineVerificationDto
    {
        public string? IFSC { get; set; }
        public string? AccountNo { get; set; }
        //public string? cancelledchequeimage { get; set; }
        public int Orgid { get; set; }
        public string? fileData { get; set; }
        //public string? filePath { get; set; }
        public string? fileFormat { get; set; }
        public string? bankaccountname { get; set; }
        public int? bankid { get; set; }
    }
}
