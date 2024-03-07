using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Onboarding
{
    public class CpafDetailsDto
    {
        public int orgId { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public int status { get; set; }
    }
}
