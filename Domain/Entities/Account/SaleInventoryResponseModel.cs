using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class SaleInventoryResponseModel
    {
        public int p_OperationStatus { get; set; } // integer, 
        public string p_OperationMessage { get; set; } //character varying, 
        public int p_OperationLogId { get; set; } // integer, 
    }
}
