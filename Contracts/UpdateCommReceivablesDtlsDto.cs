using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class UpdateCommReceivablesDtlsDto
    {
        public int commReceivableId { get; set; }
        public int commReceivableDtlsId { get; set; }
        public int statusId { get; set; }
        
    }
}
