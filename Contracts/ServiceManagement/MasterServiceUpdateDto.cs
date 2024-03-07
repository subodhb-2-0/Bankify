using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class MasterServiceUpdateDto
    {
        public string serviceName { get; set; }
        public string serviceCode { get; set; }
        [Required]
        public int servicecategoryId { get; set; }
        public int serviceId { get; set; }
        public int Status { get; set; }
        public string remarks { get; set; }
        public string servicedescription { get; set; }

    }
}
