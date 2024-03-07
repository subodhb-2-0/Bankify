using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.FundTransfer
{
    public class ChannelPartnerDto
    {
        public int orgid { get; set; }
        public string retailorcode { get; set; }
        public int status { get; set; }
    }
}
