using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Acquisition
{
    public class ListProductsforChannel : BaseEntity
    {
        public long productId { get; set; }
        public string productname { get; set; }
        public string productdesc { get; set; }
        public int enrollmentfee { get; set; }
        public int lotsize { get; set; }
        public int status { get; set; }
    }
}
