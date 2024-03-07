using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class GetCommSharingModelDtlsDto
    {
        public int commSharingId { get; set; }
        public int commSharingDtlsId { get; set; }
        public int minimumValue { get; set; }
        public int maximumValue { get; set; }
        public int commType { get; set; }
        public int baseParam { get; set; }
        public int status { get; set; }
        public int UserId { get; set; }
        public string userName { get; set; }
        public float sevenPayShare { get; set; }
        public float DistShare { get; set; }
        public float retailerShare { get; set; }
        public DateTime CreatedDate { get; set; }
        public float SuperDistShare { get; set; }
    }
}
