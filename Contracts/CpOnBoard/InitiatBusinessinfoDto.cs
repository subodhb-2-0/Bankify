using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CpOnBoard
{
    public class InitiatBusinessinfoDto
    {
        public string? HouseNo { get; set; }
        public string? BusinessRoadNo { get; set; }
        public string? BusinessDist { get; set; }
        public string? BusinessSubDist { get; set; }
        public string? ref_param1 { get; set; }
        public string? ref_param2 { get; set; }
        public string? PinCode { get; set; }
        public string? ShopImage { get; set; }
        public int? LandMark { get; set; }
        public int? IsHandicap { get; set; }
        public int? BusinessType { get; set; }
        public int Orgid { get; set; }
        public string fileData { get; set; }
        public string? filePath { get; set; }
        public string? fileFormat { get; set; }
    }
}
