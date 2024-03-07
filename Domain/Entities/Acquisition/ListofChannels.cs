using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Acquisition
{
    public class ListofChannels : BaseEntity
    {
        public long ChannelId { get; set; }
        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDesc { get; set; }
    }
}
