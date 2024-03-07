using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class ListofAccType : BaseEntity
    {
        public int orgtypeid { get; set; }
        public int accid { get; set; }
        public string accdescription { get; set; }
    }
}
