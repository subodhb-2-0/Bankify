using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Account
{
    public class SevenPayBankList : BaseEntity
    {
        public int bankId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("accountNumber")]
        public string bankaccnumber { get; set; }
        public string remarks { get; set; }
        public int status { get; set; }
    }
}
