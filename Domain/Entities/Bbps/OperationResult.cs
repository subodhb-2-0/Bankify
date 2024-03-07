using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bbps
{
    public class OperationResult
    {
        public int p_operationstatus { get; set; }
        public string p_operationmessage { get; set; }
        public int p_operationlogid { get; set; }
    }
}
