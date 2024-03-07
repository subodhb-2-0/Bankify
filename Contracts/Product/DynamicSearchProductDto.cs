using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class DynamicSearchProductDto
    {
        public long totalcount { get; set; }
        public long productId { get; set; }
        public string productName { get; set; }
        public string productDesc { get; set; }
        public string channelName { get; set; }
        public DateTime creationDate { get; set; }
        public int status { get; set; }

    }
}
