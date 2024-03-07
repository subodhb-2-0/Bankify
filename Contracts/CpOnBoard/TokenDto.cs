using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class CpOnBoardTokenData
    {
        public string token { get; set; }
    }

    public class CpOnBoardToken
    {
        public string responseCode { get; set; }
        public CpOnBoardTokenData data { get; set; }
        public string response { get; set; }
        public object errors { get; set; }
    }

}
