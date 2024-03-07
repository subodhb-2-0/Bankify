using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class InventoryIdDetailsResponse
    {
        public int inventorydetailsid {  get; set; }
        public string urn { get; set; }
        public string product {  get; set; }
        public int orderno { get; set; }
        public DateTime orderdate { get; set; }
        public decimal buyamount { get; set; }
        public decimal sellamt { get; set; }
        public string purchaseby { get; set; }
        public string orgname { get; set; }
    }
}
