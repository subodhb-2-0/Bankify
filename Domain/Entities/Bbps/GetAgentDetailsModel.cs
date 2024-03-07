using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bbps
{
    public class GetAgentDetailsModel
    {
        public long agentcount { get; set; }
        public int orgid { get; set; }
        public string agentid { get; set; }
        public string agentname { get; set; }
        public string geocode { get; set; }
        public string mobilenumber { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int createdby { get; set; }
        public DateTime createddate { get; set; }
        public string agentcode { get; set; }
    }
}
