using Contracts.Product;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class DynamicSearchProductResponse
    {

        public IEnumerable<DynamicSearchProductDto> ProductListDtos { get; set; }
     
    }
}
