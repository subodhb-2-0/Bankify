using Domain.Entities.Bbps;
using Domain.Entities.Reports;
using Domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IBbpsRepository
    {
        Task<OperationResult> insertbillerDetails(BbpsBillerListModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetBillerDetailsModel>> GetbillerDetails(BbpsBillerSearchOptionsModel entity, CancellationToken cancellationToken = default);
        Task<OperationResult> insertAgentDetails(InsertBbpsAgentModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetAgentDetailsModel>> GetAgentDetails(GetAgentRequestModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<BillerInputParams>> getBillerInputParams(int bbpsbillerid, CancellationToken cancellationToken = default);
        Task<UpdateBBPSBillerModel> UpdateBillerStatusAsync(UpdateBBPSBillerModel entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<BillerCategoryResult>> GetBillerCategoryList(CancellationToken cancellationToken = default);
        Task<IEnumerable<BbpsServiceResult>> GetBbpsServiceResult(int serviceId,CancellationToken cancellationToken = default);
    }
}
