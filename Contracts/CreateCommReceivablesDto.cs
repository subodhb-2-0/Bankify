using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class CreateCommReceivablesDto
    {
        public int commReceivableId { get; set; }
        public int minimumValue { get; set; }
        public int maximumValue { get; set; }
        public int commType { get; set; }
        public int baseParam { get; set; }
        public int statusId { get; set; }
        public int UserId { get; set; }
        public DateTime createdDate { get; set; }
        public float sevenPayShare { get; set; }
    }
}
