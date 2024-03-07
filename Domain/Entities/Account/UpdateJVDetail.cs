using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class UpdateJVDetail
    {
        public int jvdetailsid { get; set; }
        public int status { get; set; }
        public int ModifierBy { get; set; }
    }
}
