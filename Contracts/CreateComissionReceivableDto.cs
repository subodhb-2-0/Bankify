using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class CreateComissionReceivableDto
    {
        
        public int Status { get; set; }
        public string ComissionReceivableName { get; set; }
        public DateTime creationdate { get; set; }
        public int UserId { get; set; }
    }
}
