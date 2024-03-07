using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class MasterServiceCreateDto
    {
        public string serviceName { get; set; }
        public string serviceCode { get; set; }
        public int servicecategoryId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public string remarks { get; set; }
        public string servicedescription { get; set; }


    }
}
