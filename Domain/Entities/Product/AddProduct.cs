using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class AddProduct : BaseEntity
    {
        public int productId { get; set; }
        public int channelId { get; set; }
        public string productName { get; set; }
        //public string channelName { get; set; }
       
        public string productDesc { get; set; }
        public double enrollmentfee { get; set; }
        

    }
}
