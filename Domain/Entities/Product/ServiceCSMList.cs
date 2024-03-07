using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class ServiceCSMList : BaseEntity
    {
        public long CSMId { get; set; }
        public string CSMName { get; set; }
        public int status { get; set; }

    }
}
