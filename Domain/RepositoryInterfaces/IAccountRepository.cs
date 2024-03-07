using Domain.Entities.Account;
using Contracts.WorkingCapital;
using Contracts.FundTransfer;
using Domain.Entities.Servicemanagement;

namespace Domain.RepositoryInterfaces
{
    public interface IAccountRepository : IGenericRepository<ViewLedger>
    {
        Task<IEnumerable<ViewLedger>> ViewLedgerAccount(CancellationToken cancellationToken = default);
        Task<IEnumerable<ListOfOrgType>> GetListofOrgType(CancellationToken cancellationToken = default);
        Task<CreateLedgerAccount> CreateLedgerAccount(CreateLedgerAccount addLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<UpdateLedgerAccount> UpdateLedgerAccount(UpdateLedgerAccount updateLedgerAccountDto, CancellationToken cancellationToken = default);
        Task<IEnumerable<ViewLedger>> GetLedgerAccount(int? Id, string? Value, CancellationToken cancellationToken = default);
        Task<IEnumerable<ListofAccType>> GetListofAccType(CancellationToken cancellationToken = default);
        Task<AccountResponseModel> AddJV(AddJV addJVDto, CancellationToken cancellationToken = default);
        //Add Ledger Account
        Task<AddLedgerAccount> AddLedgerAccount(AddLedgerAccount addLedgerAccount, CancellationToken cancellationToken = default);
        //Get Ledger Account
        Task<Tuple<IEnumerable<GetLedgerAccount>, int>> GetAddLedgerAccount(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);

        //check duplicate name in AddLedgerAccount
        Task<bool> checkDuplicateAddLedgerAccount(AddLedgerAccount addLedgerAccount);
        Task<AccountResponseModel> AddJVDetails(JVDetails addJVDto, CancellationToken cancellationToken = default);
        Task<UpdateJVDetail> UpdateJVDetailsId(UpdateJVDetail updateJVDetailDto, CancellationToken cancellationToken = default);
        Task<UpdateJVDetail> RemoveJV(UpdateJVDetail updateJVDetailDto, CancellationToken cancellationToken = default);
        Task<ApproveRejectJV> ApproveRejectJV(ApproveRejectJV updateJVDetailDto, CancellationToken cancellationToken = default);
        Task<AccountResponseModel> ApproveAndRejectJV(ApproveRejectJV jvDto, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<AccountPayment>, int>> ViewBankClaimDeposits(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountPayment>> ViewBankClaimDepositsWithFiltter(int paymentmode,int status, string? orgcode, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<AccountPayment>, int>> GetListofWCRequest(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ApproveRejectWC> ApproveRejectWC(ApproveRejectWC approveRejectWCDto, CancellationToken cancellationToken = default);
        Task<IEnumerable<JVDetailsList>> GetListofJVDetailsByJVNo(int? jvno, CancellationToken cancellationToken = default);

        Task<Tuple<IEnumerable<JVDetailsList>, int>> GetListofJVDetails(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default);
        Task<IEnumerable<ListofAccType>> GetListAccByOrgeTypeId(int OrgTypeId, CancellationToken cancellationToken = default);

        Task<WCInitiatePaymentResponseModel> InititatePayment(WCInitiatePaymentDto  wCInitiatePaymentDto, CancellationToken cancellationToken = default);
        Task<UpdateJVDetail> UpdateJVByJVDetailID(UpdateJVDetail updateJVDetailDto, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<JVInfoList>, int>> GetListofJV(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);

        Task<IEnumerable<WCPaymentResponseDTO>> CheckWCPayments(WCPaymentRequestDTO request, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrgDetailDto>> GetOrgDetails(int orgId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChannelPartnerDetailDto>> GetListOfChannelPartner(int distributororgid, int retailerorgid, CancellationToken cancellationToken = default);

        Task<IEnumerable<ChannelPartnerDto>> GetChannelPartnerList(int distributorid, CancellationToken cancellationToken = default);

        Task<IEnumerable<OrgDetailsDto>> GetOrgDetail(CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountDetailsByOrgID>> GetAccountDetailsByOrgID(int Offsetrows, int Fetchrows, int OrgtypeId, string? Accdescription, CancellationToken cancellationToken = default);
        Task<AccountResponseModel> AddAccount(AddAccount addaccDto, CancellationToken cancellationToken = default);

        Task<IEnumerable<LedgerDetailsById>> GetAllLedgerDetailsById(int transactionid, string code, CancellationToken cancellationToken = default);
        Task<IEnumerable<SevenPayBankList>> GetSevenPayBankList(long orgid, CancellationToken cancellationToken = default);
        Task<BankIFSCModel> GetBanksInfoIFSCCode(string ifscCode, CancellationToken cancellationToken = default);

        Task<RetailInventoryResponseModel> InitiateFundTransfer(FundTransferRequest fundTransferRequest, CancellationToken cancellationToken = default);
        Task<PaymentInResponseModel> UPIUpdatePayment(UPIPostTransactionRequestModel upiPostTransactionRequestModel, CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicSearchJV>> GetDynamicSearchJV(DynamicSearchRequest entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<PayoutReport>> GetPayoutReport(string? orgcode,int status, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default);
    }
}
