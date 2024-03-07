using Contracts.AEPS;
using Contracts.Common;
using Contracts.Constants;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.HttpRequestHandler;

namespace Services
{
    public class AEPSService : IAEPSService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;
        private readonly IHttpRequestHandlerService _httpRequestHandlerService;
        

        public AEPSService(IRepositoryManager repositoryManager, ICustomLogger logger, IHttpRequestHandlerService httpRequestHandlerService)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _httpRequestHandlerService = httpRequestHandlerService;
        }

        public async Task<ResponseModelDto> GetAEPSOnBoardingDetails(int? pageSize, int? pageNumber, CancellationToken cancellationToken)
        {
            ResponseModelDto response = new ResponseModelDto();
            try
            {
                var cPBCOnBoardingDetails = await _repositoryManager.cpBcOnboardingRepository.GetAEPSOnBoardingDetails(pageSize, pageNumber, cancellationToken);
                List<AEPSOnBoardingDetailsDto> aEPSOnBoardingDetailsDtos = cPBCOnBoardingDetails.Adapt<List<AEPSOnBoardingDetailsDto>>();

                AEPSOnBoardingDetailsResponse aEPSOnBoardingDetailResponse = new AEPSOnBoardingDetailsResponse()
                {
                    aEPSOnBoardingDetailsDtos = aEPSOnBoardingDetailsDtos,
                    TotalRecord = cPBCOnBoardingDetails.Count(),
                    PageNumber = pageNumber,
                    PageSize = pageSize 
                };

                response.ResponseCode = ResponseCode.Success;
                response.Response = nameof(ResponseCode.Success);
                response.Data = aEPSOnBoardingDetailResponse;
            }
            catch (Exception)
            {
                response.ResponseCode = ResponseCode.Error;
                response.Response = nameof(ResponseCode.Error);
                response.Errors = new List<ErrorModelDto>() {
                                         new ErrorModelDto()
                                         {
                                                 ErrorCode = CommonErrorCode.ContactToAdmin,
                                                 Error = Resource.ContactToAdmin 
                                         }};
            }
            return response;
        }

        public async Task<ResponseModelDto> OnboardingAsync(AgentOnboardingRequestDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new();
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string url = configurationBuilder.GetSection(Enviroment.APIURL)["AEPSOnBoarding"];
                string requestStr = JsonConvert.SerializeObject(entity);
                string response = await _httpRequestHandlerService.MakeRequest(url, nameof(HttpMethod.Post), requestStr, "");
                _logger.LogInfo("" + response);
                if (response == null || response.Contains("wrong"))
                {
                    responseModel.ResponseCode = "-2";
                    responseModel.Response = "Onboarding status cannot be verified , Please try again";
                    return responseModel;
                }
                var transactionModel = JsonConvert.DeserializeObject<OnboardingResponse>(response);
                if (transactionModel != null)
                {
                    responseModel.ResponseCode = ResponseCode.Success;
                    responseModel.Response = nameof(ResponseCode.Success);
                    responseModel.Data = transactionModel;
                    return responseModel;
                }
                else
                {
                    responseModel.ResponseCode = "-1";
                    responseModel.Response = "Onboarding status is empty";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while processing dmt transaction transafer request . ");
                _logger.LogException(ex);
                responseModel.ResponseCode = CommonErrorCode.ContactToAdmin;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = CommonErrorCode.ContactToAdmin,
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> OnboardingPaytmAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken = default)
        {
            ResponseModelDto response = new ResponseModelDto();
            try
            {
                PaySprintOnboardingDetailsDto paySprintOnboardingDetailsDto = request.GetPaySprintOnboardingInput();
                var paySprintOnboardingList = await _repositoryManager.cpBcOnboardingRepository.GetPaySprintOnboardingDetailsAsync(paySprintOnboardingDetailsDto, cancellationToken);

                if(paySprintOnboardingList.Any())
                {
                    response.ResponseCode = ResponseCode.Success;
                    response.Response = PaySprintOnboardingResponse.OnboardingProcessComplete;
                    response.Data = paySprintOnboardingList;
                }
                else
                {
                    request.RefParam1 = string.IsNullOrEmpty(request.RefParam1) ? string.Empty : BankStringBuilder(request.RefParam1, request.OnboardingStatus);
                    request.RefParam2 = string.IsNullOrEmpty(request.RefParam2) ? string.Empty : BankStringBuilder(request.RefParam2, request.OnboardingStatus);
                    request.RefParam3 = string.IsNullOrEmpty(request.RefParam3) ? string.Empty : BankStringBuilder(request.RefParam3, request.OnboardingStatus);
                    int addResult = await _repositoryManager.cpBcOnboardingRepository.AddPaytmOnboardingAsync(request, cancellationToken);
                    if (addResult == 0)
                    {
                        string result = await PerformOnBoarding(request);
                        _logger.LogInfo(result);
                        response.ResponseCode = ResponseCode.Success;
                        response.Response = nameof(ResponseCode.Success);
                        response.Data = result;
                    }
                    else
                    {
                        response.ResponseCode = ResponseCode.Error;
                        response.Response = nameof(ResponseCode.Error);
                        response.Errors = new List<ErrorModelDto>()
                        {
                            new ErrorModelDto()
                            {
                                ErrorCode  = CommonErrorCode.ContactToAdmin,
                                Error = Resource.ContactToAdmin
                            }
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogInfo(PaySprintOnboardingResponse.OnboardingError);
                _logger.LogException(ex);
                response.ResponseCode = CommonErrorCode.ContactToAdmin;
                response.Response = Resource.ContactToAdmin;
                response.Errors = new List<ErrorModelDto>() 
                { 
                    new ErrorModelDto() 
                    {
                        ErrorCode  = CommonErrorCode.ContactToAdmin,
                        Error = Resource.ContactToAdmin
                    }
                };
            }
            return response;
        }

     
        public async Task<ResponseModelDto> GetPaySprintOnboardingDetailsAsync(PaySprintOnboardingDetailsDto request, CancellationToken cancellationToken = default)
        {
            ResponseModelDto response = new();
            try 
            {
                // apply bank 5 filters.
                List<PaySprintOnboardingDetails> pendingPaytmOnboarding = (await _repositoryManager.cpBcOnboardingRepository.GetPaySprintOnboardingDetailsAsync(request, cancellationToken))?.ToList();
                foreach(PaySprintOnboardingDetails pendingRecord in pendingPaytmOnboarding)
                {      
                    int status = await GetUpdatedStatus(pendingRecord);
                    if(status != pendingRecord.onboardstatus)
                    {
                        // remove hardcode
                        pendingRecord.ref_param1 = status != 1 ? BankStringBuilder("Bank5", status) : string.Empty; 
                        pendingRecord.ref_param3 = BankStringBuilder("Bank5", status);
                        await _repositoryManager.cpBcOnboardingRepository.UpdatePaytmOnboardingAsync(status, pendingRecord);
                    }       
                }
                var updatedOnboardingDetails = await _repositoryManager.cpBcOnboardingRepository.GetPaySprintOnboardingDetailsAsync(request, cancellationToken);

                response.ResponseCode = ResponseCode.Success;
                response.Response = nameof(ResponseCode.Success);
                response.Data = updatedOnboardingDetails;
            }
            catch(Exception ex)
            {
                _logger.LogInfo(PaySprintOnboardingResponse.PaytmStatusCheckError);
                _logger.LogException(ex);
                response.ResponseCode = CommonErrorCode.ContactToAdmin;
                response.Response = Resource.ContactToAdmin;
                response.Errors = new List<ErrorModelDto>()
                {
                    new ErrorModelDto()
                    {
                        ErrorCode  = CommonErrorCode.ContactToAdmin,
                        Error = Resource.ContactToAdmin
                    }
                };
            }
            return response;
        }

        private async Task<string> PerformOnBoarding(PaytmOnboardingRequestDto request)
        {
            string onBoardingresponse = string.Empty;
            try
            {
                string Bank = request.RefParam3.Contains(PaySprintBanks.Bank5) ? PaySprintBanks.PayTmBank
                            : request.RefParam2.Contains(PaySprintBanks.Bank2) || request.RefParam2.Contains(PaySprintBanks.Bank3) ? PaySprintBanks.NSDL_FINO
                            : PaySprintBanks.UnKnownBank;
                switch (Bank)
                {
                    case PaySprintBanks.PayTmBank:
                        onBoardingresponse = await CallPayTmOnboardingMsgApi(await UpdatePaytmOnboardingPacket(request));
                        onBoardingresponse = JsonConvert.DeserializeObject<PaytmOnboardingResponseDto>(onBoardingresponse).message;
                        break;
                    case PaySprintBanks.NSDL_FINO:
                        onBoardingresponse = await CallAEPSOnboardingMsgApi(request);
                        onBoardingresponse = JsonConvert.DeserializeObject<OnboardingResponse>(onBoardingresponse).message;
                        break;
                    default:
                        onBoardingresponse = PaySprintBanks.UnKnownBank;
                        break;
                }
            }
            catch(Exception)
            {
                throw;
            }
            return onBoardingresponse;
        }

        private async Task<int> GetUpdatedStatus(PaySprintOnboardingDetails pendingRecord)
        {
            int status = pendingRecord.onboardstatus;
            try
            {
                string msgResponse = await CallPayTmOnboardingStatusCheckMsgApi(await pendingRecord.PreparePayTmOnboardingStatusCheckPacket());

                PaytmOnboardingResponseDto paytmOnboardingResponseDto = string.IsNullOrEmpty(msgResponse)
                      ? throw new Exception(" Error while calling Messaging API to check status !")
                      : JsonConvert.DeserializeObject<PaytmOnboardingResponseDto>(msgResponse);
                status = paytmOnboardingResponseDto.status == true && paytmOnboardingResponseDto.response_code == 1 ? 2 : 1;
            }
            catch(Exception)
            {
                throw;
            }
            return status;
        }
        
        private async Task<string> CallPayTmOnboardingStatusCheckMsgApi(PayTmOnboardingStatusCheckPacket request)
        {
            string response = string.Empty;
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string baseURL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.MsgAPIURL];
                string requestStr = JsonConvert.SerializeObject(request);
                string url = string.Format($"{baseURL}{Enviroment.PayTmOnboardingStatusCheckURL}");
                response = await _httpRequestHandlerService.MakeRequest(url, nameof(HttpMethod.Post), requestStr, "");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        private async Task<string> CallPayTmOnboardingMsgApi(PaytmOnboardingRequestDto request)
        {
            string response = string.Empty;
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string baseURL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.MsgAPIURL];
                string requestStr = JsonConvert.SerializeObject(request.PaytmOnboardingPacket);
                string url = string.Format($"{baseURL}{Enviroment.PayTmOnboardingURL}");
                response = await _httpRequestHandlerService.MakeRequest(url, nameof(HttpMethod.Post), requestStr, "");
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }

        private async Task<string> CallAEPSOnboardingMsgApi(PaytmOnboardingRequestDto request)
        {
            string response = string.Empty;
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string URL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.AEPSOnBoarding];
                string requestStr = JsonConvert.SerializeObject(request.AgentOnboardingRequestDto);
                response = await _httpRequestHandlerService.MakeRequest(URL, nameof(HttpMethod.Post), requestStr, "");
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }

        private async Task<PaytmOnboardingRequestDto> UpdatePaytmOnboardingPacket(PaytmOnboardingRequestDto request) =>
        await Task.Run(async () =>
        {
            request.PaytmOnboardingPacket.statecode = await _repositoryManager.cpBcOnboardingRepository.GetStateCodeByPincode(request.PaytmOnboardingPacket.pincode);
            return request;
        });


        // Move this Method to soem where like Utility Service.
        private string BankStringBuilder(string bankName, int status)
        {
            string bankStringFormat = "|Bank|status||";
            string updatedBankName = bankStringFormat.Replace("Bank", bankName);
            updatedBankName = updatedBankName.Replace(nameof(status), Convert.ToString(status == 1 ? string.Empty : status));
            return updatedBankName;
        }
    }
}
