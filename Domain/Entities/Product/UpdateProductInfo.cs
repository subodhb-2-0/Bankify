using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class UpdateProductInfo : BaseEntity
    {
        public int serviceid { get; set; }
        public int supplierid { get; set; }
        public int serviceproviderid { get; set; }
        public int csmid { get; set; }
    }
}
