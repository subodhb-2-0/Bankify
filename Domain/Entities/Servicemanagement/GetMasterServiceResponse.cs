using Contracts.ServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class GetMasterServiceResponse
    {
        public IEnumerable<DynamicManageServiceDto> AllServiceDetails { get; set; }
    }
}
