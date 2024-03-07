using Contracts.Bbps;
using Domain.Entities.Servicemanagement;

namespace Domain.RepositoryInterfaces
{
    public interface IServiceManagementRepository : IGenericRepository<GetServiceManagement>
    {
        Task<Tuple<IEnumerable<GetServiceManagement>, int>> GetAllServiceCatagoryAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<GetMasterService>, int>> GetAllServiceAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<GetSupplier>, int>> GetAllSupplierAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<GetSupplier>, int>> GetAllProviderAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<MasterServiceCreate> AddMasterServiceAsync(MasterServiceCreate entity, CancellationToken cancellationToken = default);
        Task<MasterSupplierResponse> AddMasterSupplierAsync(CreateMasterSupplier entity, CancellationToken cancellationToken = default);
        Task<CreatePstoSuppliers> AddPstoSupplierAsync(CreatePstoSuppliers entity, CancellationToken cancellationToken = default);
        Task<MasterServiceUpdate> UpdateMasterServiceAsync(MasterServiceUpdate entity, CancellationToken cancellationToken = default);
        Task<UpdateServiceStatus> UpdateServiceStatusAsync(UpdateServiceStatus entity, CancellationToken cancellationToken = default);
        Task<UpdateServiceProvider> UpdateServiceProviderAsync(UpdateServiceProvider entity, CancellationToken cancellationToken = default);
        Task<UpdateServiceProviderStatus> UpdateServiceProviderStatusAsync(UpdateServiceProviderStatus entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetMasterService>> GetAllServiceBySearchAsync(int serviceid, string? servicename, int servicecategoryid, int status, int creator, DateTime creationdate, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetViewBySupplier>> GetViewBySupplier(int supplierid, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetServiceProviderByService>> GetServiceProvider(int ServiceId, CancellationToken cancellationToken = default);

        Task<UpdateServiceSupplierData> UpdateServiceSupplierData(UpdateServiceSupplierData entity, CancellationToken cancellationToken = default);

        Task<PanDefault> GetPanDefaulterById(int id, CancellationToken cancellationToken = default);
        Task<MobileDefault> GetMobileDefaulterById(int id, CancellationToken cancellationToken = default);

        Task<int> DeactivateAssignProviderBySuppId(int suppspmapid, CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicManageService>> GetDynamicSearchService(DynamicSearchRequest entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicManageServiceProvider>> GetDynamicSearchServiceProvider(DynamicSearchRequest entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<DynamicManageServiceProvider>> GetDynamicSearchServiceSupplier(DynamicSearchRequest entity, CancellationToken cancellationToken = default);

    }
}
