using Contracts.Common;
using Contracts.Error;
using Contracts.Response;
using Contracts.Account;
using Domain.Entities.Servicemanagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using Domain.Entities.Account;
using Contracts.Constants;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;
        public AccountService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
     
        public async Task<ResponseModelDto> ViewLedgerAccount(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.ViewLedgerAccount(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ViewLedgerDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while View Ledger");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetListofOrgType(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetListofOrgType(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListOfOrgTypeDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of OrgType");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> CreateLedgerAccount(CreateLedgerAccountDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<CreateLedgerAccount>();
                await _repositoryManager.accountRepository.CreateLedgerAccount(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create Ledger Account");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> UpdateLedgerAccount(UpdateLedgerAccountDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateLedgerAccount>();
                await _repositoryManager.accountRepository.UpdateLedgerAccount(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update Ledger Account");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> GetLedgerAccount(int? Id, string? Value, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetLedgerAccount(Id, Value, cancellationToken); 
                var userDtos = allUsers.Adapt<IEnumerable<ViewLedgerDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Ledger Account {Id}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetListofAccType(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetListofAccType(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListofAccTypeDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of AccType");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> AddJV(AddJVDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = entity.Adapt<AddJV>();
                var _transactionresponse = await _repositoryManager.accountRepository.AddJV(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _transactionresponse;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Add JV");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public async Task<ResponseModelDto> AddJVDetails(JVDetailsDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = entity.Adapt<JVDetails>();
                var _transactionresponse = await _repositoryManager.accountRepository.AddJVDetails(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _transactionresponse;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create JV Details");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public async Task<ResponseModelDto> UpdateJVDetailsId(UpdateJVDetailDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateJVDetail>();
                await _repositoryManager.accountRepository.UpdateJVDetailsId(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update Ledger Account");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public async Task<ResponseModelDto> RemoveJV(UpdateJVDetailDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateJVDetail>();
                await _repositoryManager.accountRepository.RemoveJV(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update Ledger Account");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> ApproveRejectJV(ApproveRejectJVDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<ApproveRejectJV>();
                await _repositoryManager.accountRepository.ApproveRejectJV(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Approve Reject JV");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> ApproveAndRejectJV(ApproveRejectJVDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = entity.Adapt<ApproveRejectJV>();
                var _transactionresponse = await _repositoryManager.accountRepository.ApproveAndRejectJV(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _transactionresponse;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Approve and Reject JV");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> GetListofWCRequest(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                
                var allUsers = await _repositoryManager.accountRepository.GetListofWCRequest(pageSize, pageNumber,orderByColumn, orderBy, cancellationToken);
                //var userDtos = allUsers.Adapt<IEnumerable<AccountPaymentDto>>();
                //responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                //responseModel.Response = "Success";
                //responseModel.Data = userDtos;

                var userDtos = allUsers.Item1.Adapt<IEnumerable<AccountPaymentDto>>();
                AccountPaymentResponse userDetailResponse = new AccountPaymentResponse() { AccountPayments = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogInfo($"Error while Get List of WCRequest");
                //_logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> ViewBankClaimDeposits(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                //var allUsers = await _repositoryManager.accountRepository.ViewBankClaimDeposits(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                //var userDtos = allUsers.Adapt<IEnumerable<AccountPaymentDto>>();
                //responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                //responseModel.Response = "Success";
                //responseModel.Data = userDtos;

                var allUsers = await _repositoryManager.accountRepository.ViewBankClaimDeposits(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<AccountPaymentDto>>();
                AccountPaymentResponse userDetailResponse = new AccountPaymentResponse() { AccountPayments = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogInfo($"Error while View Bank Claim Deposits");
                //_logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> ViewBankClaimDepositsWithFiltterasync(int paymentmode, int status, string? orgcode, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var _gettxnResponse = await _repositoryManager.accountRepository.ViewBankClaimDepositsWithFiltter(paymentmode, status, orgcode, fromDate, toDate, p_offsetrows, p_fetchrows, cancellationToken);
                if (_gettxnResponse == null)
                {
                    responseModel.ResponseCode = WorkingCapitalError.Paymentnotfound;
                    responseModel.Response = Resource.UnableToFetcpaymentHistory;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = WorkingCapitalError.Paymentnotfound,
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = _gettxnResponse;
                responseModel.TotalCount = _gettxnResponse.Count();
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the txn history {paymentmode}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                {
                     ErrorCode  = CommonErrorCode.ContactToAdmin,
                     Error = Resource.ContactToAdmin 
                } };
            }
            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        
        public async Task<ResponseModelDto> ApproveRejectWC(ApproveRejectWCDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<ApproveRejectWC>();
                await _repositoryManager.accountRepository.ApproveRejectWC(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Approve Reject WC");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public async Task<ResponseModelDto> GetListofJVDetailsByJVNo(int? jvno, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetListofJVDetailsByJVNo(jvno, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<JVDetailsListDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while ListofJVDetails {jvno}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetListofJVDetails(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetListofJVDetails(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<JVDetailsListDto>>();
                JVDetailsListResponse userDetailResponse = new JVDetailsListResponse() { jVDetailsListDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogInfo($"Error while View Bank Claim Deposits");
                //_logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetListAccByOrgeTypeId(int OrgTypeId,CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetListAccByOrgeTypeId(OrgTypeId,cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListofAccTypeDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of AccType");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        Task<ResponseModelDto> IGenericService<ResponseModelDto>.AddAsync(ResponseModelDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        Task<ResponseModelDto> IGenericService<ResponseModelDto>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.UpdateAsync(ResponseModelDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ResponseModelDto IGenericService<ResponseModelDto>.Validate(ResponseModelDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> UpdateJVByJVDetailID(UpdateJVDetailDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateJVDetail>();
                await _repositoryManager.accountRepository.UpdateJVByJVDetailID(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update JVDetail ID ");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> GetListofJV(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetListofJV(pageSize, pageNumber, orderByColumn, orderBy,searchBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<JVInfoListDto>>();
                JVInfoListResponse userDetailResponse = new JVInfoListResponse() { jVDetailsListDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogInfo($"Error while View Bank Claim Deposits");
                //_logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetAccountDetailsByOrgID(int Offsetrows, int Fetchrows, int OrgtypeId, string? Accdescription, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetAccountDetailsByOrgID(Offsetrows,Fetchrows,OrgtypeId,Accdescription, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<AccountDetailsByOrgIDDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Account Details By OrgTypeID {OrgtypeId}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> AddAccount(AddAccountDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = entity.Adapt<AddAccount>();
                var _transactionresponse = await _repositoryManager.accountRepository.AddAccount(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _transactionresponse;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Add Account");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> GetAllLedgerDetailsById(int transactionid, string code, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetAllLedgerDetailsById(transactionid, code, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<LedgerDetailsByIdDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get All Ledger Details By Id {transactionid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetSevenPayBankList(long orgid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetSevenPayBankList(orgid, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<SevenPayBankListDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Seven Pay Bank List {orgid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> GetBanksInfoIFSCCode(string ifscCode, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetBanksInfoIFSCCode(ifscCode, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<BankIFSCModelDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Banks Info IFSCCode {ifscCode}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> AddLedgerAccount(AddLedgerAccDto addLedgerAccDto, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var _addLedgerAccount = addLedgerAccDto.Adapt<AddLedgerAccount>();

                bool isDuplicate = await _repositoryManager.accountRepository.checkDuplicateAddLedgerAccount(_addLedgerAccount);
                if (isDuplicate == true)
                {
                    responseModel.ResponseCode = "1";
                    responseModel.Response = $"Duplicate name found: {addLedgerAccDto.accname}";
                }
                else
                {
                    var serviceEntity = addLedgerAccDto.Adapt<AddLedgerAccount>();
                    await _repositoryManager.accountRepository.AddLedgerAccount(serviceEntity, cancellationToken);
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while adding Ledger Account");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetAddLedgerAccount(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.accountRepository.GetAddLedgerAccount(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var getUserDtos = allUsers.Item1.Adapt<IEnumerable<GetLedgerAccountDetailsDto>>();
                GetLedgerAccountDetailsResponse getLedgerAccountDetailsResponse = new GetLedgerAccountDetailsResponse() { getLedgerAccountDetails = getUserDtos, TotalRecord = allUsers.Item2, PageSize = pageSize, PageNumber = pageNumber, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = getLedgerAccountDetailsResponse;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Getting Ledger Account Details.");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>()
         {
             new ErrorModelDto()
             {
             ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
             Error = Resource.ContactToAdmin
             }
         };
            }

            return responseModel;
        }


        public async Task<ResponseModelDto> GetDynamicSearchJV(DynamicSearchJVRequestDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var serviceEntity = entity.Adapt<DynamicSearchRequest>();
                var response = await _repositoryManager.accountRepository.GetDynamicSearchJV(serviceEntity, cancellationToken);

                var commDtos = response.Adapt<IEnumerable<DynamicSearchJVDto>>();

                DynamicSearchJVResponse userDetailResponse = new DynamicSearchJVResponse() { jVDetailsListDtos = commDtos };

                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetDynamicSearchJV");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetPayoutReportAsync(string? orgcode, int status, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var _gettxnResponse = await _repositoryManager.accountRepository.GetPayoutReport(orgcode, status, fromDate, toDate, p_offsetrows, p_fetchrows, cancellationToken);
                if (_gettxnResponse == null)
                {
                    responseModel.ResponseCode = WorkingCapitalError.Paymentnotfound;
                    responseModel.Response = Resource.UnableToFetcpaymentHistory;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = WorkingCapitalError.Paymentnotfound,
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = _gettxnResponse;
                responseModel.TotalCount = _gettxnResponse.Any() ? _gettxnResponse.Count() : 0;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Payout Report");
                _logger.LogException(ex);
                responseModel.ResponseCode = CommonErrorCode.ContactToAdmin;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                {
                    ErrorCode  = CommonErrorCode.ContactToAdmin,
                    Error = Resource.ContactToAdmin
                } };
            }
            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
    }
}
