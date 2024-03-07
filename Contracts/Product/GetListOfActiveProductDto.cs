using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class GetListOfActiveProductDto
    {
        public int? productid { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public string productname { get; set; }
        public int status { get; set; }
      
    }
}
