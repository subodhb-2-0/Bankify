using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IMasterDataRepository : IGenericRepository<MasterDataModel>
    {
        Task<IEnumerable<MasterDataModel>> GetDesignationListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<MasterDataModel>> GetDepartmentListAsync(CancellationToken cancellationToken = default);
    }
}
