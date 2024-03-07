using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class ListProductsforChannelDto 
    {
        public long productId { get; set; }
        public string productname { get; set; }
        public string productdesc { get; set; }
        public int enrollmentfee { get; set; }
        public int lotsize { get; set; }
        public int status { get; set; }
    }
}
