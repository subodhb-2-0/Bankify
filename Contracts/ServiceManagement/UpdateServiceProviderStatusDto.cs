using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class UpdateServiceProviderStatusDto
    {
        public int SupplierId { get; set; }
        public int Status { get; set; }
    }
}
