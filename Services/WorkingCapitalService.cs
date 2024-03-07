using Contracts.Common;
using Contracts.Error;
using Contracts.WorkingCapital;
using Domain.Entities.Account;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System.Security.Cryptography;

namespace Services
{
    public class WorkingCapitalService : IWorkingCapialService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public WorkingCapitalService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task<ResponseModelDto> CheckWCPayments(WCPaymentRequestDTO request, CancellationToken cancellationToken = default)
        {
            //return new ResponseModelDto
            //{
            //    Data = new List<WCPaymentResponseDTO> { 
            //        new WCPaymentResponseDTO
            //        {
            //            amount =2.20M,
            //            bankPayInSlip="bankPayInSlip",
            //            depositDate=DateTime.Now.ToString("yyyy-MM-dd"),
            //            depositTime=DateTime.Now.ToString("HH:mm:ss"),
            //            instrumentNumber="123",
            //            orgId=1,
            //            orgName="orgName",
            //            payeeName="payeeName",
            //            paymentDate=DateTime.Now.ToString("yyyy-MM-dd"),
            //            paymentMode=1,
            //            pgCharge="pgCharge",
            //            remarks="remarks",
            //            seviceCharge="serviceCharge",
            //            transferByOrgId=1,
            //            vpa="dXXXXXX@ybl",
            //            wiseIFSC="HDFCXXXX1" 
            //        } 
            //    }
            //};
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = request.Adapt<WCPaymentRequestDTO>();
                var _payment = await _repositoryManager.accountRepository.CheckWCPayments(request, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Check WCPayment");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetListOfPaymentMode(int orgId, int orgType, CancellationToken cancellationToken)
        {
            return new ResponseModelDto
            {
                Data = new List<PaymentResponseDTO>
                {
                    new PaymentResponseDTO
                    {
                        paymentMode = "deposit",
                        paymentStatus = PaymentStatus.successful 
                    }
                }
            };
        }

        public async Task<ResponseModelDto> InititatePayment(WCInitiatePaymentDto request, CancellationToken cancellationToken = default)
        {

            //return new ResponseModelDto
            //{
            //    Data = new { paymentId = "dummyPaymentId" }
            //};
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                if (!string.IsNullOrEmpty(request.p_bankpayinslip))
                {
                    var _filename = Guid.NewGuid().ToString() + "." + request.p_fileFormat;
                    File.WriteAllBytes(@"wwwroot/Uploads/UPIPayments/" + _filename, Convert.FromBase64String(request.p_bankpayinslip));
                    request.p_bankpayinslip = _filename;
                }
                var serviceEntity = request.Adapt<WCInitiatePaymentDto>();
                var _payment = await _repositoryManager.accountRepository.InititatePayment(request, cancellationToken);
                //if (_payment == null)
                //{
                //    responseModel.ResponseCode = ((int)ErrorCodeEnum.PaymentErrorEnum.SomethingWentWrongWhileUpdatingVPA).ToString();
                //    responseModel.Response = Resource.SomethingWentWrongWhileUpdatingVPA;
                //    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                //    ErrorCode  = ((int)ErrorCodeEnum.PaymentErrorEnum.SomethingWentWrongWhileUpdatingVPA).ToString(),
                //    Error = Resource.SomethingWentWrongWhileUpdatingVPA } };
                //    return responseModel;
                //}
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _payment;

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Inititate Payment");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetOrgDetail(CancellationToken cancellationToken)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var _payment = await _repositoryManager.accountRepository.GetOrgDetail(cancellationToken);
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
    }
}