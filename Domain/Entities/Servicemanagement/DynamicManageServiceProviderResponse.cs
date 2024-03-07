using Contracts.ServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicemanagement
{
    public class DynamicManageServiceProviderResponse
    {
        public IEnumerable<DynamicManageServiceProviderDto> SupplierDetails { get; set; }
       
    }
}
