using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class BaseEntity 
    {
        public int Id { get; set; }
        public string? IPAddress { get; set; }
        [Column("creationdate")]
        public DateTime CreatedOn { get; set; }
        [Column("modificationdate")]
        public DateTime UpdatedOn { get; set; }
        [Column("creator")]
        public int CreatedBy { get; set; }
        [Column("modifier")]
        public int UpdatedBy { get; set; }

    }
}
