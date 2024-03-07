using Contracts;
using Contracts.Bbps;
using Contracts.Common;

namespace Services.Abstractions
{
    public interface IComissionService : IGenericService<ComissionDto>
    {
        Task<ResponseModelDto> GetAllCommisionTypeAsync( CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCommisionTypeAsync(string commType, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetbaseParamByCommTypeAsync(string Value2, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCommReceivablesDtlsAsync(int commReceivableId, int Userid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCommSharingModelDtlsAsync(int commSharingId, int Userid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCommReceivablesDtlByMinAndMaxsAsync(int minimumValue, int maximumValue,int UserId, int commReceivableId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCommSharingDtlByMinAndMaxsAsync(int minimumValue, int maximumValue,int UserId, int commSharingId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetRecentIdOfComissionReciveAsync(string crname, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetRecentIdOfComissionSharingAsync(string csmname, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCommisionSharingAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetComissionReceivableAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateComissionReceivableAsync(CreateComissionReceivableDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateComissionReceivableDetlsAsync(CreateCommReceivablesDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateComissionSharingModelAsync(CreateCommSharingModelDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateComissionSharingModelDtlsAsync(CreateCommSharingModelDtlsDto entity, CancellationToken cancellationToken = default);
        ResponseModelDto ValidateCommReceivablesDtls(CreateCommReceivablesDto entity);
        ResponseModelDto ValidateCommSharingModelDtls(CreateCommSharingModelDtlsDto entity);
        Task<ResponseModelDto> GetCommReceivablesStatus( int CRID, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditReceivablesStatusAsync(ComissionReciveableStatusDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditReceivablesDtlsAsync(UpdateCommReceivablesDtlsDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditCommSharingModelAsync(UpdateCommSharingModelDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditCommSharingModelDtlsAsync(UpdateCommSharingModelDtlsDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetDynamicSearchComissionReceiveable(DynamicSearchComissionReceiveableDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetDynamicSearchSharingModels(DynamicSearchComissionReceiveableDto entity, CancellationToken cancellationToken = default);
    }
}
