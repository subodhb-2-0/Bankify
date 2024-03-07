using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class SaleChannelGetProductDetailsDto
    {
        public string mobilenumber { get; set; }
        public string orgcode { get; set; }
        public int orgtype { get; set; }
        public string productname { get; set; }
        public int productid { get; set; }
        public string EmployeeAddress { get; set; }
        public string orgName { get; set; }
    }
}
