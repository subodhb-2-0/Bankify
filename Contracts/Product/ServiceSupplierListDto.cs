using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class ServiceSupplierListDto
    {
        public long ServiceSupplierId { get; set; }
        public string ServiceSupplierName { get; set; }
        public int status { get; set; }

    }
}
