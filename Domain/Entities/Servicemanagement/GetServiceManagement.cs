using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class GetServiceManagement : BaseEntity
    {
        public int tbl_mst_sc_id { get; set; }
        public string servicecategoryname { get; set; }
        public string creator { get; set; }
        public string servicecategorydesc { get; set; }
        public int status { get; set; }
    }
}
