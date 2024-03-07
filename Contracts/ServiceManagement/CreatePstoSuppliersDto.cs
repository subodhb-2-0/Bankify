using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class CreatePstoSuppliersDto
    {
        public int serviceId { get; set; }
        public int supplierId { get; set; }
        public int serviceProviderId { get; set; }
        public int commReceivableId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
    }
}
