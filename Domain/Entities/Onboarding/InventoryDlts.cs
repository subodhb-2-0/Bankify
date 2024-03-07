using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Onboarding
{
    public class InventoryDlts: BaseEntity
    {
        public int inventoryId { get; set; }
        public int orgId { get; set; }
        public DateTime? soldDate { get; set; }
        public int status { get; set; }
        [Column("inventory_dtls_id")]
        public int inventory_dtls_id { get; set; }
        [Column("cpafnnumber")]
        public int cpafnnumber { get; set; }
    }
}