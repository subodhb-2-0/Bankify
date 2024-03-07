using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bbps
{
    public class BillerInputParams
    {
        public string paramname { get; set; }
        public string datatype { get; set; }
        public int isoptional { get; set; }
        public int minlength { get; set; }
        public int maxlength { get; set; }
        public int billerparaminfoid { get; set; }
    }
}
