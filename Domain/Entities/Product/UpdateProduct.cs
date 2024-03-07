using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class UpdateProduct : BaseEntity
    {
        public int productId { get; set; }
        //public string servicename { get; set; }
        //public string servicesupplier { get; set; }
        //public string serviceprovider { get; set; }
        //public string CSM { get; set; }
        public int serviceId { get; set; }
        public int supplierId { get; set; }
        public int serviceproviderId { get; set; }
        public int CSMId { get; set; }

        public int svcoffid { get; set; }

    }
}
