using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class UploadSelfiModelDto
    {
        public string fileData { get; set; }
        public string? filePath { get; set; }
        public string? fileFormat { get; set; }
        public string? ref_param1 { get; set; }
        public string? ref_param2 { get; set; }
        public string? ref_param3 { get; set; }
        public string? ref_param4 { get; set; }
        public string? ref_param5 { get; set; }
        public int status { get; set; }
        public int orgId { get; set; }
        public int userid { get; set; }
    }
}
