using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class ProductDetailsbyIdDto
    {
        public long svcoffid { get; set; }
        public long productid { get; set; }
        public string productname { get; set; }
        public int serviceid { get; set; }
        public string servicename { get; set; }
        public int supplierid { get; set; }
        public string suppliername { get; set; }
        public int serviceproviderid { get; set; }
        public string ServiceProvideName { get; set; }
        public int csmid { get; set; }
        public string csmname { get; set; }
        public int channelid { get; set; }
        public string channelname { get; set; }
        public int status { get; set; }

    }
}
