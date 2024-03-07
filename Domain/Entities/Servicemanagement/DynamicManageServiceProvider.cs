using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class DynamicManageServiceProvider
    {
        public long totalcount { get; set; }
        public int supplierid { get; set; }
        public string suppliercode { get; set; }
        public string suppliername { get; set; }
        public string supplierdesc { get; set; }
        public int status { get; set; }
        public DateTime creationdate { get; set; }
        public string servicename { get; set; }
        public int suppliertype { get; set; }

    }
}
