using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class GetMasterService
    {
        
        public int servicecategoryid { get; set; }
        public string servicename { get; set; }
        public string servicedescription { get; set; }
        public string remarks { get; set; }
        public int serviceid { get; set; }
        public int status { get; set; }
        public string servicecode { get; set; }
        public DateTime creationdate { get; set; }
    }
}
