using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Comission
{
    public class DynamicSearchSharingModels
    {
        public long totalcount { get; set; }
        public int csmid { get; set; }
        public string csmname { get; set; }
        public string csmstatus { get; set; }
        public DateTime creationdate { get; set; }
    }
}
