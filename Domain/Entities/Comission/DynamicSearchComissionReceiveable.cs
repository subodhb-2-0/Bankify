using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Comission
{
    public class DynamicSearchComissionReceiveable
    {
        public string p_searchoption { get; set; }
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
    }
}
