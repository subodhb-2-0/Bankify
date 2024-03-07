using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bbps
{
    public class GetBillerDetailsModel
    {
        public long totalcount { get; set; }
        public string billerid { get; set; }
        public string serviceprovider { get; set; }
        public string billercategory { get; set; }
        public int billercategoryid { get; set; }
        public string billername { get; set; }
        public bool adhocpayment { get; set; }
        public string coverage { get; set; }
        public bool paymentexactness { get; set; }
        public string billerdesc { get; set; }
        public string billerstatus { get; set; }
        public int supplierid { get;set; }
        public string suppliername { get; set; }
        public int serviceproviderid { get; set; }
    }
}
