using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class InsertBbpsAgentDto
    {
        public int p_orgid { get; set; }
        //public string p_orgid { get; set; }
        //public string p_orgcode { get; set; }
        public string p_agentid { get; set; }
        public string p_agentname { get; set; }
        public string p_geocode { get; set; }
        public string p_mobilenumber { get; set; }
        public string p_pincode { get; set; }
        public string p_city { get; set; }
        public string p_state { get; set; }
        public int p_createdby { get; set; }
        public int p_modifiedby { get; set; }
        public string p_agentcode { get; set; }
    }
}
