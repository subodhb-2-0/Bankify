using Contracts.Bbps;
using Domain.Entities.Comission;

namespace Domain.RepositoryInterfaces
{
    public interface IComissionRepository : IGenericRepository<Comission>
    {
        Task<IEnumerable<Comission>> GetAllComissionType(CancellationToken cancellationToken = default);
        Task<IEnumerable<Comission>> GetComissionType(string commType, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetbaseParamById>> GetbaseParamByCommType(string Value2, CancellationToken cancellationToken = default);
        Task<IEnumerable<CommReceivablesDtls>> GetCommReceivablesDtls(int commReceivableId,int Userid, CancellationToken cancellationToken = default);
        Task<IEnumerable<CompairCommReceivablesDtls>> GetCompairCommReceivablesDtlsValue(int crid, int minimumValue, int maximumValue, CancellationToken cancellationToken = default);
        Task<CommReceivablesDtls> CompairCommReceivablesDtls(int commReceivableId, int crdid, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetCommSharingModelDtls>> GetCommSharingModelDtls(int commSharingId, int Userid, CancellationToken cancellationToken = default);
        Task<CreateCommReceivablesDtls> GetCommReceivablesDtlsByminAndMaxValue(int minimumValue, int maximumValue,int UserId,int commReceivableId, CancellationToken cancellationToken = default);
        Task<CreateCommSharingModelDtls> GetCommSharingDtlsByminAndMaxValue(int minimumValue, int maximumValue,int UserId, int commSharingId, CancellationToken cancellationToken = default);
        Task<ComissionReciveIdReturn> GetRecentIdOfComissionRecive(string crname, CancellationToken cancellationToken = default);
        Task<ComissionReciveIdReturn> GetRecentIdOfComissionSharing(string csmname, CancellationToken cancellationToken = default);
        
        Task<Tuple<IEnumerable<ComissionSharing>, int>> GetComissionSharingModel(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<ComissionReceivable>, int>> GetComissionReceivableModel(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        
        Task<CreateComissionReceivable> AddComissionReceivableAsync(CreateComissionReceivable entity, CancellationToken cancellationToken = default);
        Task<bool> CheckDuplicateName(CreateComissionReceivable entity);
        Task<CreateCommReceivablesDtls> AddComissionReceivableDtlsAsync(CreateCommReceivablesDtls entity, CancellationToken cancellationToken = default);
        Task<CreateCommSharingModel> AddComissionSharingModelAsync(CreateCommSharingModel entity, CancellationToken cancellationToken = default);
        Task<CreateCommSharingModelDtls> AddComissionSharingModelDtlsAsync(CreateCommSharingModelDtls entity, CancellationToken cancellationToken = default);
        Task<ComissionReciveableStatus> GetCommReceivablesStatusAsync( int CRID, CancellationToken cancellationToken = default);
        Task<ComissionReciveableStatus> CheckCommisionShairingAsync( int CommisionSharingId, CancellationToken cancellationToken = default);
        Task<ComissionReciveableStatus> UpdateCommReceivablesStatusAsync(ComissionReciveableStatus entity, CancellationToken cancellationToken = default);
        Task<UpdateCommReceivablesDtls> UpdateCommReceivablesDtlsAsync(UpdateCommReceivablesDtls entity, CancellationToken cancellationToken = default);
        Task<UpdateCommSharingModel> UpdateCommSharingModel(UpdateCommSharingModel entity, CancellationToken cancellationToken = default);
        Task<UpdateCommSharingModelDtls> UpdateCommSharingModelDtls(UpdateCommSharingModelDtls entity, CancellationToken cancellationToken = default);
        Task<CreateCommSharingModel> GetComissionSharingModelAsync(string CommSharingModelName, CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicSearchComissionReceiveableModel>> GetDynamicSearchComissionReceiveable(DynamicSearchComissionReceiveable entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicSearchSharingModels>> GetDynamicSearchSharingModels(DynamicSearchComissionReceiveable entity, CancellationToken cancellationToken = default);
    }
}
