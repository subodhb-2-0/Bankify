using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class ComissionDto : BaseEntityDto
    {
        public int param_id { get; set; }
        public string param_group { get; set; }
        public string param_key { get; set; }
        public string param_value1 { get; set; }
        public string param_value2 { get; set; }
    }
}
