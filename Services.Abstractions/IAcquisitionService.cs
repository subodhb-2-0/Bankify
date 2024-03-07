using Contracts.Acquisition;
using Contracts.Common;
using Contracts.Bbps;

namespace Services.Abstractions
{
    public interface IAcquisitionService : IGenericService<ResponseModelDto>
    {
        Task<ResponseModelDto> GetAcquisitionDetails(int? Id, string? Value, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> VerifyCPApplication(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListOrgType(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofFeildStaff(CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetParentOrgId(int? OrgType, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> VerifyChannelPartner(int? Id, string? Value, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCpDetailsById(int? orgId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCPDetailsforApproval(int? OrgId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ConfirmRejectCP(ConfirmRejectCPInfoDto addAcquisitionDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateCPStatus(int orgid, int status, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ViewActiveCP(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofChannels(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetProductsforChannel(int? channelId,CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCpdetailsByNameAsync(int? orgType, string? orgName, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ApproveRejectCP(ApproveRejectCPInfoDto addAcquisitionDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateCPByDistributor(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCityAreaByPinCode(int PinCode, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCpafByOrgid(long orgid, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> ApproveCPByActivateDate(int orgid, int status, string activationdate, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetDynamicSearchCPAsync(DynamicSearchModelCPDto entity, CancellationToken cancellationToken = default);
    }
    public interface IUserProvider
    {
        string GetUserId();
        string GetUserName();
    }
}
