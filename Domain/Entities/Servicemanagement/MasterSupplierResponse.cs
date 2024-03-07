using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class MasterSupplierResponse
    {
        public int p_operationstatus { get; set; } // integer, 
        public string p_operationmessage { get; set; } //character varying, 
        public int p_operationlogid { get; set; } // integer, 
    }
}
