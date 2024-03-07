using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class Getallpaymentdetails
    {
        public int p_orgid { get; set; }
        public string p_fromdt { get; set; }
        public string p_todt { get; set; }
        public int p_status { get; set; }
    }
}
