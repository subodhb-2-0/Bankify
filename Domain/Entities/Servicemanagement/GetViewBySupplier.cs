using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class GetViewBySupplier
    {
        public int serviceid { get; set; }
        public int supplierid { get; set; }
        public int serviceproviderpid { get; set; }
        public int suppspmapid { get; set; }
        public int crid { get; set; }
        public string servicename { get; set; }
        public string suppliername { get; set; }
        public string crname { get; set; }
        public int status { get; set; }
    }
}
