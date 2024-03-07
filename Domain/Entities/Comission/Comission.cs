using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Comission
{
    public class Comission : BaseEntity
    {
        public int param_id { get; set; }
        public string param_group { get; set; }
        public string param_key { get; set; }
        public string param_value1 { get; set; }
        public string param_value2 { get; set; }
    }
}
