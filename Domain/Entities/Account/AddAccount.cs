using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class AddAccount : BaseEntity
    {
        public int OrgtypeId { get; set; }
        public string Accdescription { get; set; }
        public int status { get; set; }
        public int creator { get; set; }
    }
}
