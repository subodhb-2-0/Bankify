using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class ServiceProviderList : BaseEntity
    {
        public long ServiceProvideId { get; set; }
        public string ServiceProvideName { get; set; }
        public int status { get; set; }

    }
}
