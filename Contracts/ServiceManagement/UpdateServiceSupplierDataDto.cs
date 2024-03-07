using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class UpdateServiceSupplierDataDto
    {
        public int Supplierid { get; set; }
        public string Suppliercode { get; set; }
        public string Suppliername { get; set; }
        public int Status { get; set; }
        
    }
}
