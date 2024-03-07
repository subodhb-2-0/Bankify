using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class AddServiceOffering : BaseEntity
    {
        public long productId { get; set; }
        public int serviceId { get; set; }
        public int supplierId { get; set; }
        public int serviceproviderId { get; set; }
        public int CSMId { get; set; }

    }
}
