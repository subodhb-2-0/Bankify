using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class RetailerInfoByDistributorId : BaseEntity
    {
        public int totalcount { get; set; }
        public string retailercode { get; set; }
        public string orgname { get; set; }
        public double balancetransferamount { get; set; }
        public double retailerbalance { get; set; }
        public double transferpercentage { get; set; }

    }
}
