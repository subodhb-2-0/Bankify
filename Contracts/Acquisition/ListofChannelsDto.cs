using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class ListofChannelsDto 
    {
        public long ChannelId { get; set; }
        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDesc { get; set; }
    }
}
