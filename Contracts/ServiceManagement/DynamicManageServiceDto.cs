using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceManagement
{
    public class DynamicManageServiceDto
    {
        public long totalcount { get; set; }
        public int servicecategoryid { get; set; }
        public string serviceName { get; set; }
        public string servicedescription { get; set; }
        public string remarks { get; set; }
        public int serviceid { get; set; }
       
        public int Status { get; set; }
        public string serviceCode { get; set; }

        public DateTime creationdate { get; set; }
        
    }
}

