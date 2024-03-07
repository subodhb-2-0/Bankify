using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class CreateMasterSupplierDto
    {
        public int serviceId { get; set; }
        public string supplierName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDesc { get; set; }
        public int supplierType { get; set; }
        public int remarks { get; set; }
        public int createdBy { get; set; }
    }
}
