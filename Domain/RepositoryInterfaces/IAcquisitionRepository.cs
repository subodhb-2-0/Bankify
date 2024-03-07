using Domain.Entities.Acquisition;
using Contracts.Acquisition;
using Contracts.Onboarding;
using Domain.Entities.Location;

namespace Domain.RepositoryInterfaces
{
    public interface IAcquisitionRepository : IGenericRepository<Acquisition>
    {
        Task<IEnumerable<Acquisition>> GetAcquisitionDetails(int? Id, string? Value, CancellationToken cancellationToken = default);
        Task<AddAcquisitionResponse> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default);

        Task<Tuple<IEnumerable<VerifyCPApplication>, int>> VerifyCPApplication(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default);
        Task<IEnumerable<ListOrgTypeDto>> GetListOrgType(CancellationToken cancellationToken = default);
        Task<IEnumerable<ListofFeildStaffDto>> GetListofFeildStaff(CancellationToken cancellationToken = default);

        Task<IEnumerable<ParentOrgIdDto>> GetParentOrgId(int? OrgType, CancellationToken cancellationToken = default);
        Task<IEnumerable<VerifyChannelPartner>> VerifyChannelPartner(int? Id, string? Value, CancellationToken cancellationToken = default);
        Task<CPDetailsforApprovalDto> GetCPDetailsById(int? OrgId, CancellationToken cancellationToken = default);
        Task<CPDetailsforApprovalDto> GetCPDetailsforApproval(int? OrgId, CancellationToken cancellationToken = default);
        Task<int> ConfirmRejectCP(ConfirmRejectCPInfoDto crInfoDto, CancellationToken cancellationToken = default);
        Task<int> UpdateCPStatus(int orgid, int status, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<ViewActiveCP>, int>> ViewActiveCP(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<IEnumerable<ListofChannels>> GetListofChannels(CancellationToken cancellationToken = default);
        Task<IEnumerable<ListProductsforChannel>> GetProductsforChannel(int? channelId, CancellationToken cancellationToken = default);
        Task<IEnumerable<getCPDetailsByName>> GetAllCPDetailsByNameAsync(int? orgType, string? orgName, CancellationToken cancellationToken = default);
        Task<int> ApproveRejectCP(ApproveRejectCPInfoDto crInfoDto, CancellationToken cancellationToken = default);
        Task<IEnumerable<CPAquisitionDetail>> GetCPAcquistionDetails(CPAquisitionDetailRequestDto request, CancellationToken cancellationToken = default);
        Task<AddAcquisitionResponse> CreateCPByDistributor(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default);
        Task<IEnumerable<CityAreaModel>> GetCityAreaByPinCode(int PinCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<CpafNumber>> GetCpafByOrgid(long orgid, CancellationToken cancellationToken = default);

        Task<int> ApproveCPByActivateDate(int orgid, int status, string activationdate,  CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicSearchCpApplicationModel>> GetDynamicSearchCP(DynamicSearchModelCP entity, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<CPAquisitionReportDetails>, int>> GetCPAquisitionReportDetails(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken);
    }
}
