using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class BillerInfoResponseMainModel
    {
        public Xml xml { get; set; }
        public BillerInfoResponse billerInfoResponse { get; set; }
        public string requestId { get; set; }
    }
}
