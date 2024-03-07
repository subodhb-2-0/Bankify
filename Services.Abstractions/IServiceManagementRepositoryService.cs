using Contracts.Bbps;
using Contracts.Common;
using Contracts.ServiceManagement;

namespace Services.Abstractions
{
    public interface IServiceManagementRepositoryService : IGenericService<GetServiceManagementDto>
    {
        Task<ResponseModelDto> GetAllServiceCatagory(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllService(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllSupplier(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllProvider(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllServiceBySearch(int serviceid, string? servicename, int servicecategoryid, int status, int creator, DateTime creationdate, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateMasterServiceAsync(MasterServiceCreateDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateMasterSupplierAsync(CreateMasterSupplierDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreatePstoSupplierAsync(CreatePstoSuppliersDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditMasterServiceAsync(MasterServiceUpdateDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditServiceStatusAsync(UpdateServiceStatusDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditServiceProviderAsync(UpdateServiceProviderDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditServiceProviderStatusAsync(UpdateServiceProviderStatusDto entity, CancellationToken cancellationToken = default);
        ResponseModelDto ValidateEditMasterService(MasterServiceUpdateDto entity);
        Task<ResponseModelDto> GetViewBySupplierAsync(int supplierid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetServiceProviderAsync(int ServiceId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateServiceSupplierData(UpdateServiceSupplierDataDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetMobileDefaulterByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetPanDefaulterByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> DeactivateAssignProviderBySuppId(int suppspmapid, CancellationToken cancellationToken);
        Task<ResponseModelDto> GetDynamicSearchService(DynamicSearchRequestDto entity, CancellationToken cancellationToken);
        Task<ResponseModelDto> GetDynamicSearchServiceProvider(DynamicSearchRequestDto entity, CancellationToken cancellationToken);
        Task<ResponseModelDto> GetDynamicSearchServiceSupplier(DynamicSearchRequestDto entity, CancellationToken cancellationToken);

    }
}
