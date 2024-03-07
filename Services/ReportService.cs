using Contracts.Common;
using Contracts.Error;
using Domain.RepositoryInterfaces;
using Logger;
using Services.Abstractions;
using Mapster;
using Contracts.Report;
using Contracts.Onboarding;
using Domain.Entities.Reports;
using Contracts.Response;
using Contracts.Constants;
using Domain.Entities.Acquisition;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public ReportService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task<ResponseModelDto> getCPLedger(LedgerRequestDto request, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                responseModel.Data = new List<LedgerDetailResponseDto>
                {
                    new LedgerDetailResponseDto
                    {
                       closingBalance = 1M,
                       openingBalance = 1M,
                       totalCredits = 1,
                       totalDebits = 1,
                       fromDate = DateTime.Now,
                       toDate = DateTime.Now,
                       data = new LedgerDetail
                       {
                           accountId = "1",
                           accountName = "Deepak",
                           CreatedBy = 1,
                           CreatedOn = DateTime.Now,
                           credit = 1,
                           date = DateTime.Now,
                           debit = 1,
                           ledgerId = "1",
                           jvNo = "1",
                           srNo = "1",
                           status = 1,
                           narration1 = "",
                           narration2 = "",
                           paymentId = 1,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now,
                       }

                    }
                };

            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> getListofChannel(CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                responseModel.Data = new List<ChannelTypeDto>
                {
                    new ChannelTypeDto
                    {
                        channelId = 1,
                        channelCode = "",
                        channelName = "channelName"
                    }
                };

            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> getListofPaymentMode(int? orgId, int? orgType, CancellationToken cancellationToken)
        {
            //ResponseModelDto responseModel = new ResponseModelDto();
            //responseModel.ResponseCode = "-1";
            //try
            //{
            //    responseModel.Data = new List<PaymentModeDto>
            //    {
            //        new PaymentModeDto
            //        {
            //            paymentModeId = 1,
            //            status = 1,
            //            paymentMode = "SpName"
            //        }
            //    };
            //}
            //catch (Exception ex)
            //{
            //    responseModel.Response = "Error";
            //    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
            //        ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
            //        Error = Resource.ContactToAdmin } };
            //}
            //return responseModel;

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.reportsRepository.getListofPaymentMode(orgId, orgType, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<PaymentModeDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of List of PaymentMode");
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

        public async Task<ResponseModelDto> getListofServiceProviders(int serviceId, int supplierId, CancellationToken cancellationToken)
        {
            //ResponseModelDto responseModel = new ResponseModelDto();
            //responseModel.ResponseCode = "-1";
            //try
            //{
            //    responseModel.Data = new List<ServiceProviderDto>
            //    {
            //        new ServiceProviderDto
            //        {
            //            spId = 1,
            //            status = 1,
            //            spName = "SpName"
            //        }
            //    };

            //}
            //catch (Exception ex)
            //{
            //    responseModel.Response = "Error";
            //    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
            //        ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
            //        Error = Resource.ContactToAdmin } };
            //}
            //return responseModel;

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.reportsRepository.getListofServiceProviders(serviceId, supplierId, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ServiceProviderDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of Service Providers");
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

        public async Task<ResponseModelDto> getListofServices(CancellationToken cancellationToken)
        {
            //ResponseModelDto responseModel = new ResponseModelDto();
            //responseModel.ResponseCode = "-1";
            //try
            //{
            //    responseModel.Data = new List<ServiceDto>
            //    {
            //        new ServiceDto
            //        {
            //            status = 1,
            //            servicId = 1,
            //            serviceName = "serviceName"
            //        }
            //    };

            //}
            //catch (Exception ex)
            //{
            //    responseModel.Response = "Error";
            //    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
            //        ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
            //        Error = Resource.ContactToAdmin } };
            //}

            //return responseModel;

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.reportsRepository.getListofServices(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ServiceDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Lit of Service");
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

        public async Task<ResponseModelDto> getListofState(CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.reportsRepository.getListofState(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<State>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of State");
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

        public async Task<ResponseModelDto> getListofSuppliers(int serviceId, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.reportsRepository.getListofSuppliers(serviceId,cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<SupplierDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of Suppliers");
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

        public async Task<ResponseModelDto> getListOfTranfers(BalanceTransferRequestDto request, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                responseModel.Data = new List<TransferResponseDto>
                {
                    new TransferResponseDto
                    {
                        status = 1,
                        orgCode = "123",
                        orgId = 1,
                        orgName = "orgName",
                        paymentDate = DateTime.Now,
                        paymentId = 1,
                        paymentMode = 1,
                        paymentOrgId = 1,
                        transferAmount = 1000
                    }
                };

            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
                _logger.LogException(ex);
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> ViewTxnsOfCPs(TransactionRequestDto requestDto, CancellationToken cancellationToken)
        {
            //ResponseModelDto responseModel = new ResponseModelDto();
            //responseModel.ResponseCode = "-1";
            //try
            //{
            //    responseModel.Data = new List<TransactionDto>
            //    {
            //        new TransactionDto
            //        {
            //            transactionId = "XXXXXXXXX",
            //            transactionDate = DateTime.Now,
            //            transactionType = TransactionType.sale,
            //            city = "Noida",
            //            orgCode="10002",
            //            orgId = 1000,
            //            sericeId = 1,
            //            serviceName = "Service name",
            //            serviceProviderCode = "1",
            //            serviceProviderName = "Jio Services",
            //            supplierId = 1,
            //            supplierName = "Supplier name",
            //            spId = 1,
            //            spname = "spName",
            //            orgName="retailers service",
            //            status = Contracts.WorkingCapital.PaymentStatus.successful,
            //            txnAmount = 10m,
            //        }
            //    };
            //}
            //catch (Exception ex)
            //{
            //    responseModel.Response = "Error";
            //    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
            //        ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
            //        Error = Resource.ContactToAdmin } };
            //    _logger.LogException(ex);
            //}

            //return responseModel;
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var requestparam = requestDto.Adapt<TransactionRequest>();
                var _checkResponse = await _repositoryManager.reportsRepository.ViewTxnsOfCPs(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the txn details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        public async Task<ResponseModelDto> GettxnReports(gettxndetailsreportsDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<gettxndetailsreports>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetTxnreportDetails(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the txn details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> Gettxnledger(GetretailerledgerDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<Getretailerledger>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetledgerDetails(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the ledger details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> Getwithdrawalledger(cashwithdrawalledgerRequestDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<cashwithdrawalledgerRequest>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetwithdrawalledgerDetails(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the ledger details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetAllpaymentdetailsAsync(GetallpaymentdetailsDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<Getallpaymentdetails>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetAllpaymentdetails(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the payment details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetAllpaymentsHistoryAsync(GetPaymentHistoryDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<GetPaymentHistory>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetAllPaymentHistory(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the payment history report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetTxnCancelOrSsucsessDetailsAsync(GetcancelTxnDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<GetcancelTxn>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetTxnCancelOrSsucsessDetails(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the payment cancel or sucess report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetRetalierSalsAndCancelationAsync(RetalierSalsAndCancelationDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<RetalierSalsAndCancelation>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetRetalierSalsAndCancelation(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the retailer cancel or sucess report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetDistributerSalsAndCancelationAsync(DistributerSalsAndCancelationDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<DistributerSalsAndCancelationModel>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetDistributerSalsAndCancelation(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Distributer cancel or sucess report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetchannelserviceSalsAndCancelationAsync(ChannelParametersRequestDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<ChannelParametersRequestModel>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetchannelserviceSalsAndCancelation(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the channelservice cancel or sucess report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetTransactionDetailsByTxnIdAsync(long p_txnid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _checkResponse = await _repositoryManager.reportsRepository.GetTransactionDetailsByTxnId(p_txnid, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching data by txn id");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetRetailerTxnReportAsync(RetailerTxnReportDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var requestparam = entity.Adapt<RetailerTxnReport>();
                var _checkResponse = await _repositoryManager.reportsRepository.GetRetailerTxnReport(requestparam, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Retailer Transaction report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }


        public async Task<ResponseModelDto> GetPurchaseOrderDetailsAsync(PurchaseOrderReportDto purchaseOrderReportDto, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var response = await _repositoryManager.reportsRepository.GetPurchaseOrderDetailsAsync(purchaseOrderReportDto, cancellationToken);
                if (response == null)
                {
                    responseModel.ResponseCode = ReportsError.Reportnotfound;
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() 
                                            {
                                                ErrorCode  = ReportsError.Reportnotfound,
                                                Error = Resource.Reportnotfound } 
                                            };
                    return responseModel;
                }
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = response;

            }
            catch(Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Retailer Transaction report");
                _logger.LogException(ex);
                responseModel.ResponseCode = CommonErrorCode.ContactToAdmin;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() 
                                        {
                                            ErrorCode  = CommonErrorCode.ContactToAdmin,
                                            Error = Resource.ContactToAdmin } 
                                        };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetOrderReportCpAsync(OrderReportRequestDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var requestParam = entity.Adapt<OrderReportRequestModel>();
                var response = await _repositoryManager.reportsRepository.GetOrderReportCP(requestParam, cancellationToken);
                if (response == null)
                {
                    responseModel.ResponseCode = ReportsError.Reportnotfound;
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                            {
                                                ErrorCode  = ReportsError.Reportnotfound,
                                                Error = Resource.Reportnotfound }
                                            };
                    return responseModel;
                }
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = response;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the CP Order report.");
                _logger.LogException(ex);
                responseModel.ResponseCode = CommonErrorCode.ContactToAdmin;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                            ErrorCode  = CommonErrorCode.ContactToAdmin,
                                            Error = Resource.ContactToAdmin }
                                        };
                
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetInventoryReportAsync(InventoryReportRequestDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var requestParam = entity.Adapt<InventoryReportRequestModel>();
                var response = await _repositoryManager.reportsRepository.GetInventoryReport(requestParam, cancellationToken);
                if (response == null)
                {
                    responseModel.ResponseCode = ReportsError.Reportnotfound;
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                            {
                                                ErrorCode  = ReportsError.Reportnotfound,
                                                Error = Resource.Reportnotfound }
                                            };
                    return responseModel;
                }
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = response;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Inventory report.");
                _logger.LogException(ex);
                responseModel.ResponseCode = CommonErrorCode.ContactToAdmin;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                            ErrorCode  = CommonErrorCode.ContactToAdmin,
                                            Error = Resource.ContactToAdmin }
                                        };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetInventoryIdDetailsAsync(int p_inventoryid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _checkResponse = await _repositoryManager.reportsRepository.GetInventoryIdDetails(p_inventoryid, cancellationToken);
                if (_checkResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString();
                    responseModel.Response = Resource.Reportnotfound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.reportsErrorEnum.Reportnotfound).ToString(),
                    Error = Resource.Reportnotfound } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _checkResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching Inventory id details data.");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
    }
}