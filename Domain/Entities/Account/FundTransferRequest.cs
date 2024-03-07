using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class FundTransferRequest : BaseEntity
    {
        public int orgid { get; set; }
        public int paymentmode { get; set; }
        public decimal amount { get; set; }
        public string depositdate { get; set; }
        public int status { get; set; }
        public string remark { get; set; }
        public int creator { get; set; }
        public int transferbyorgid { get; set; }
    }
}
