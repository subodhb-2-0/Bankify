using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class ProductWiseBuySaleInventoryDetailsResInfo : BaseEntity
    {
        public string productname { get; set; }
        public string channelname { get; set; }
        public int buycount { get; set; }
        public int salecount { get; set; }
    }
}
