using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class RetailerProductUpdate : BaseEntity 
    {

       public int productId { get; set; }
    }
}
