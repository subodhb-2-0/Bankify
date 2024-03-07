using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class GetAgentRequestDto
    {
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
        public string p_agentcode { get; set; }
    }
}
