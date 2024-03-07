using Contracts.Account;
using Contracts.Acquisition;
using Contracts.Common;
using Contracts.Constants;
using Contracts.Error;
using Contracts.FundTransfer;
using Contracts.WorkingCapital;
using Domain.Entities.Account;
using Domain.Entities.Onboarding;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System.Security.Cryptography;

namespace Services
{
    public class FundTransferService : IFundTransferService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public FundTransferService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task<ResponseModelDto> GetChannelPartnerList(int distributorid, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var _payment = await _repositoryManager.accountRepository.GetChannelPartnerList(distributorid, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Getting ChannelPartner List");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetListOfChannelPartner(int distributororgid, int retailerorgid, CancellationToken cancellationToken = default)
        {
            //return new ResponseModelDto
            //{
            //    Data = new List<ChannelPartnerDetailDto> {
            //        new ChannelPartnerDetailDto
            //        {
            //        city = "Noida",
            //        orgId = 1,
            //        orgName = "orgName",
            //        bussinessAddress = "businessAddress",
            //        state="UP",
            //        status=1,
            //        latlong="latLong",
            //        orgCode=1,
            //        pinCode="20XXXX1"
            //        }
            //    }
            //};
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var _payment = await _repositoryManager.accountRepository.GetListOfChannelPartner(distributororgid,retailerorgid, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Org Details");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetOrgDetails(int orgId, CancellationToken cancellationToken = default)
        {
            //return new ResponseModelDto
            //{
            //    Data = new OrgDetailDto
            //    {
            //        orgCode = 1,
            //        orgId = 1,
            //        orgLimit =1,
            //        orgName ="orgName",
            //        walletBalance= 333.33M
            //    }
            //};
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var _payment = await _repositoryManager.accountRepository.GetOrgDetails(orgId, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Org Details");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> InitiateFundTransfer(FundTransferRequestDto request, CancellationToken cancellationToken = default)
        {
            //return new ResponseModelDto
            //{
            //    Data = new { paymentId = "dummyPaymentId" },
            //    Response = "InitiateFundTransfer",
            //    ResponseCode = "success"
            //};
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var _reqModel = request.Adapt<FundTransferRequest>();
                var _payment = await _repositoryManager.accountRepository.InitiateFundTransfer(_reqModel, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Initiate FundTransfer");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> UPIUpdatePayment(PaymentUpdateInModelDto paymentUpdateInModel, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var _payment = await _repositoryManager.accountRepository.UPIUpdatePayment(paymentUpdateInModel.Adapt<UPIPostTransactionRequestModel>(), cancellationToken);

                if (_payment == null)
                {
                    responseModel.ResponseCode = PaymentError.SomethingWentWrongWhileUpdatingVPA;
                    responseModel.Response = Resource.SomethingWentWrongWhileUpdatingVPA;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = PaymentError.SomethingWentWrongWhileUpdatingVPA,
                    Error = Resource.SomethingWentWrongWhileUpdatingVPA } };
                    return responseModel;
                }

                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;

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
    }
}