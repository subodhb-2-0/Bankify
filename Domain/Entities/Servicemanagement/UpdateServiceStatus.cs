using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class UpdateServiceStatus
    {
        public int ServiceId { get; set; }
        public int Status { get; set; }
    }
}
