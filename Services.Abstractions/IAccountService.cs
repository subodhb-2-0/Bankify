using Contracts.Account;
using Contracts.Common;

namespace Services.Abstractions
{
    public interface IAccountService : IGenericService<ResponseModelDto>
    {
        Task<ResponseModelDto> ViewLedgerAccount(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofOrgType(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreateLedgerAccount(CreateLedgerAccountDto createLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateLedgerAccount(UpdateLedgerAccountDto updateLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetLedgerAccount(int? Id, string? Value, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofAccType(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> AddJV(AddJVDto addJVDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> AddJVDetails(JVDetailsDto addJVDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateJVDetailsId(UpdateJVDetailDto updateLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> RemoveJV(UpdateJVDetailDto updateLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ApproveRejectJV(ApproveRejectJVDto updateLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ApproveAndRejectJV(ApproveRejectJVDto addJVDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofWCRequest(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ViewBankClaimDeposits(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ViewBankClaimDepositsWithFiltterasync(int paymentmode, int status, string? orgcode, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ApproveRejectWC(ApproveRejectWCDto approveRejectWC, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> AddLedgerAccount(AddLedgerAccDto addLedgerAccDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAddLedgerAccount(int?pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        
        Task<ResponseModelDto> GetListofJVDetailsByJVNo(int? jvno, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofJVDetails(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListAccByOrgeTypeId(int OrgTypeId, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> UpdateJVByJVDetailID(UpdateJVDetailDto updateLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofJV(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetAccountDetailsByOrgID(int Offsetrows, int Fetchrows, int OrgtypeId, string? Accdescription, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> AddAccount(AddAccountDto addaccDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllLedgerDetailsById(int transactionid, string code, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetSevenPayBankList(long orgid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetBanksInfoIFSCCode(string ifscCode, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetDynamicSearchJV(DynamicSearchJVRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetPayoutReportAsync(string? orgcode, int status, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default);

    }
}
