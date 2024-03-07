using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class AddServiceOfferingDto
    {
        public long productId { get; set; }
        public int serviceId { get; set; }
        public int supplierId { get; set; }
        public int serviceproviderId { get; set; }
        public int CSMId { get; set; }

    }
}
