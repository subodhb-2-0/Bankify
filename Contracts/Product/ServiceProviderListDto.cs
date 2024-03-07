using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class ServiceProviderListDto
    {
        public long ServiceProvideId { get; set; }
        public string ServiceProvideName { get; set; }
        public int status { get; set; }

    }
}
