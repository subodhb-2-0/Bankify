using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class InventoryReportResponseModel
    {
        public int inventoryid { get; set; }
        public DateTime inventorydate { get; set; }
        public string batchnumber { get; set; }
        public string inventorytype { get; set; }
        public int inventoryorder {  get; set; }
        public int inventoryamount { get; set; }
        public int balanceinventory {  get; set; }
        public string productname { get; set; }
    }
}
