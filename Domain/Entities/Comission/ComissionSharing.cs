using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Comission
{
    public class ComissionSharing
    {
        public int csmid { get; set; }
        public string csmname { get; set; }
        public string csmstatus { get; set; }
        public DateTime creationdate { get; set; }
    }
}
