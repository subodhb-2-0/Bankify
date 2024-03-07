using Contracts;
using Contracts.Common;

namespace Services.Abstractions
{
    public interface IMasterDataService : IGenericService<MasterDataDto>
    {
        Task<ResponseModelDto> GetDesignationListAsync(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetDepartmentListAsync(CancellationToken cancellationToken = default);
    }
}