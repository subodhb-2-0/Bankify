using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class ResponseModel
    {
        public string? ResponseCode { get; set; }
        public string? Response { get; set; }
        public IList<ErrorModel>? Errors { get; set; }
    }
}
