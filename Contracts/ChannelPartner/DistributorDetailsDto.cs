using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Onboarding
{
    public class DistributorDetailsDto
    {
        public int orgid { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public double runningbalance { get; set; }
    }
}
