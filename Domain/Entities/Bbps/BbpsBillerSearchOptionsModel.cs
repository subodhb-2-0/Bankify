using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bbps
{
    public class BbpsBillerSearchOptionsModel
    {
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
        public string p_searchoptions { get; set; }
        public int p_billercategoryid { get; set; }
    }
}
