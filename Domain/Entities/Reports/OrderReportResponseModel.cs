using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class OrderReportResponseModel
    {
        public int orderid { get; set; }
        public DateTime orgdate { get; set; }
        public string productname { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public int totalamt { get; set; }
        public int totalqty { get; set; }
        public int status { get; set; }
        public string contactno { get; set; }
        public string deliveryaddress { get; set; }
        public string busi_district { get; set; }
        public string busi_sub_district { get; set; }
        public string tracknumber { get; set; }
        public string deliverypartner { get; set; }
    }
}
