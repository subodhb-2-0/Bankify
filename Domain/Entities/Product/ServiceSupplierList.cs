using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class ServiceSupplierList : BaseEntity
    {
        public long ServiceSupplierId { get; set; }
        public string ServiceSupplierName { get; set; }
        public int status { get; set; }

    }
}
