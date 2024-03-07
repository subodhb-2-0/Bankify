using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class ListOfOrgType 
    {
        public int org_type_id { get; set; }
        public string org_type { get; set; }
        public string org_type_desc { get; set; }
    }
}
