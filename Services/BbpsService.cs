using Contracts.Bbps;
using Contracts.Common;
using Contracts.Error;
using Contracts.Report;
using Domain.Entities.Bbps;
using Domain.Entities.Reports;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Mapster.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BbpsService : IBbpsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public BbpsService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public async Task<ResponseModelDto> insertbillerAsync(BbpsBillerListDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<BbpsBillerListModel>();
                var _checkResponse = await _repositoryManager.bbpsRepository.insertbillerDetails(requestparam, cancellationToken);
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
                _logger.LogInfo($"Error while inserting the biller details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetbillerAsync(BbpsBillerSearchOptionsDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<BbpsBillerSearchOptionsModel>();
                var _checkResponse = await _repositoryManager.bbpsRepository.GetbillerDetails(requestparam, cancellationToken);
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
                _logger.LogInfo($"Error while getting the biller details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetAgentAsync(GetAgentRequestDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<GetAgentRequestModel>();
                var _checkResponse = await _repositoryManager.bbpsRepository.GetAgentDetails(requestparam, cancellationToken);
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
                _logger.LogInfo($"Error while getting the agent details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> InsertAgentAsync(InsertBbpsAgentDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<InsertBbpsAgentModel>();
                var _checkResponse = await _repositoryManager.bbpsRepository.insertAgentDetails(requestparam, cancellationToken);
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
                _logger.LogInfo($"Error while insert the agent details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetBillerInputParams(int bbpsbillerid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _checkResponse = await _repositoryManager.bbpsRepository.getBillerInputParams(bbpsbillerid, cancellationToken);
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
                _logger.LogInfo($"Error while getting the biller details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetBillerInfoAsync(string billercode, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "0";
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:*/*");                   
                    var configurationBuilder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                    // Get values from the config, given their key and their target type.
                    string apiUrl = configurationBuilder.GetSection("ApiUrls")["GetBillerInfo"];
                    string _result = client.UploadString($"{apiUrl}?billercode={billercode}", "");
                    var result = JsonConvert.DeserializeObject<ResponseModelDto>(_result);
                    string responseJson = JsonConvert.SerializeObject(result.Data);
                    var result2 = System.Text.Json.JsonSerializer.Deserialize<BillerInfoResponseMainModel>(responseJson.ToString());
                    //var result2 = JsonConvert.DeserializeObject<BillerInfoResponseMainModel>(responseJson);
                    if (result2 is not null)
                    {
                        responseModel.Data = result2;
                    }
                    else
                    {
                        responseModel.Errors = result.Errors;
                    }
                }
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
        public async Task<ResponseModelDto> UpdateBBPSBillerStatus(UpdateBBPSBillerModelDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<UpdateBBPSBillerModel>();
                var _checkResponse = await _repositoryManager.bbpsRepository.UpdateBillerStatusAsync(requestparam, cancellationToken);
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
                _logger.LogInfo($"Error while update the agent details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        public async Task<ResponseModelDto> GetBillerCategoryList( CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _checkResponse = await _repositoryManager.bbpsRepository.GetBillerCategoryList(cancellationToken);
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
                _logger.LogInfo($"Error while getting the biller details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        public async Task<ResponseModelDto> GetBbpsServiceResult(int serviceId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _checkResponse = await _repositoryManager.bbpsRepository.GetBbpsServiceResult(serviceId,cancellationToken);
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
                _logger.LogInfo($"Error while getting the biller details report");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        /*
        public async  Task<BbpsBillerListDto> GenerateBillerInfoInsertUpdateObject(int serviceProviderId,BillerInfoResponseMainModel holdUpdatedBillerInfo)
        {
            //Construct param info
            ParamInfo paramInfo;
            List<ParamInfo> paramInfos = new List<ParamInfo>();
            
            foreach (var v in holdUpdatedBillerInfo.billerInfoResponse.biller.billerInputParams)
            {
                paramInfo = new ParamInfo()
                {
                    dataType = v.DataType,
                    isOptional = v.IsOptional,
                    maxLength = v.MaxLength,
                    minLength = v.MinLength,
                    paramName = v.ParamName
                };
                paramInfos.Add(paramInfo);
            }

            //Prepare the request and response object for update process.
            BbpsBillerListDto bbpsBillerListDto = new()
            {
                p_billerid = holdUpdatedBillerInfo.billerInfoResponse.biller.billerId,
                p_billername = holdUpdatedBillerInfo.billerInfoResponse.biller.billerName,
                p_serviceproviderid = serviceProviderId,
                p_billercategoryid = 0,
                p_adhocpayment = Convert.ToInt16(Convert.ToBoolean(holdUpdatedBillerInfo.billerInfoResponse.biller.billerAdhoc)),
                p_coverage = holdUpdatedBillerInfo.billerInfoResponse.biller.billerCoverage,
                p_fetchrequirement = (holdUpdatedBillerInfo.billerInfoResponse.biller.billerFetchRequiremet.ToUpper() == "MANDATORY") ? 1 : 0,
                p_paymentexactness = (holdUpdatedBillerInfo.billerInfoResponse.biller.billerPaymentExactness.ToUpper() == "EXACT") ? 1 : 0,
                p_supportbillvalidation = (holdUpdatedBillerInfo.billerInfoResponse.biller.billerSupportBillValidation.ToUpper() == "MANDATORY") ? 1 : 0,
                p_supportpendingstatus = (holdUpdatedBillerInfo.billerInfoResponse.biller.supportPendingStatus.ToUpper() == "YES") ? 1 : 0,
                p_supportdeemed = (holdUpdatedBillerInfo.billerInfoResponse.biller.supportDeemed.ToUpper() == "YES") ? 1 : 0,
                p_billertimeout = (!string.IsNullOrEmpty(holdUpdatedBillerInfo.billerInfoResponse.biller.billerTimeout)) ? Convert.ToInt32(holdUpdatedBillerInfo.billerInfoResponse.biller.billerTimeout) : 0,
                p_billeramountoptions = holdUpdatedBillerInfo.billerInfoResponse.biller.billerAmountOptions,
                p_billerpaymentmode = holdUpdatedBillerInfo.billerInfoResponse.biller.billerPaymentModes,
                p_billerdesc = holdUpdatedBillerInfo.billerInfoResponse.biller.billerDescription,
                p_status = 2,
                p_createdby = 0,
                p_billerinputparams = paramInfos
            };

            return await Task.FromResult(bbpsBillerListDto);


        }

        */
    }
}
