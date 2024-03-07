using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Location
{
    public class CityAreaModel
    {
        public int CityAreaId { get; set; }
        public string CityAreaName { get; set; }
        public int Pincode { get; set; }
        public string CityName { get; set; }
        public int PinCodeId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
