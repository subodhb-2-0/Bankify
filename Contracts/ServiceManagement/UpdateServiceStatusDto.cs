using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class UpdateServiceStatusDto
    {
        public int ServiceId { get; set; }
        public int Status { get; set; }
    }
}
