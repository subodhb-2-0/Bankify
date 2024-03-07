using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class GetretailerledgerResponse
    {
        public long totalCount { get; set; }
        public double openingbalance { get; set; }
        public double runningtotal { get; set; }
        public long orgid { get; set; }
        public DateTime retdate { get; set; }
        public long accid { get; set; }
        public long targetorgid { get; set; }
        public DateTime createdate { get; set; }
        public double debit { get; set; }
        public double credit { get; set; }
        public string narration1 { get; set; }
        public string accdescription { get; set; }
        public string respectiveid { get; set; }
    }
}
