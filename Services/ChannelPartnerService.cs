using Contracts.Common;
using Contracts.Error;
using Contracts.Onboarding;
using Contracts.Product;
using Contracts.Response;
using Contracts.ServiceManagement;
using Domain.Entities.Onboarding;
using Domain.Entities.Product;
using Domain.Entities.Servicemanagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;

namespace Services
{
    public class ChannelPartnerService : IChannelPartnerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public ChannelPartnerService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task<ResponseModelDto> BuyRetailerInventory(BuyRetailerInventoryRequestDto request, CancellationToken token)
        {
            //ResponseModelDto responseModel = new ResponseModelDto();
            //try
            //{
            //    var inventoryId = new Random().Next();
            //    TypeAdapterConfig<BuyRetailerInventoryRequestDto, Inventory>.NewConfig()
            //        .Map(dest => dest, (source) => source)
            //        .Map(dest => dest.totalInventory, source => source.totalLot)
            //        .Map(dest => dest.totalAmt, source => source.totalAmount).NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            //    var newInventory = request.Adapt<Inventory>();
            //    var response = await _repositoryManager.inventoryRepository.AddAsync(newInventory, token);
            //    if(response != null)
            //    {
            //        for(var index=0; index<request.totalLot; index++)
            //        {
            //            TypeAdapterConfig<(BuyRetailerInventoryRequestDto, Inventory), InventoryDlts>.NewConfig()
            //        .Map(dest => dest, (source) => source.Item1)
            //        .Map(dest => dest, (source) => source.Item2)
            //        .Map(dest => dest.inventoryId, (source) => source.Item2.inventoryId)
            //        .Map(dest => dest.status, (source) => 1)
            //        .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            //            var inventoryDlts = (request, response).Adapt<InventoryDlts>();
            //           await _repositoryManager.inventoryDetailsRepository.AddAsync(inventoryDlts, token);
            //        }
            //    }
            //    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
            //    responseModel.Response = "Success";
            //}
            //catch (Exception ex)
            //{
            //    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
            //    responseModel.Response = "Error";
            //    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
            //                              {
            //                                    ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
            //                                    Error = Resource.ContactToAdmin }
            //                              };
            //    _logger.LogException(ex);
            //}
            //return responseModel;

            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = request.Adapt<BuyRetailerInventoryRequestDto>();
                var _inventory = await _repositoryManager.inventoryRepository.BuyRetailerInventory(request, token);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _inventory;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Buy Retailer Inventory");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetChannelTypes(CancellationToken token)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var channels = await _repositoryManager.channelRepository.GetAllByStatusAsync(2,token);
                var channelTypeDtos = channels.Adapt<IEnumerable<ChannelTypeDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = channelTypeDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                          {
                                                ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                Error = Resource.ContactToAdmin }
                                          };
                _logger.LogException(ex);
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetProductDetails(CancellationToken token)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var products = await _repositoryManager.productRepository.GetProductDetails(token); 
                var productDetailsDto = products.Adapt<List<ProductDetailDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = productDetailsDto;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                          {
                                                ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                Error = Resource.ContactToAdmin }
                                          };
                _logger.LogException(ex);
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetRetailerBalance(int parantorgid, CancellationToken token)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var products = await _repositoryManager.inventoryRepository.GetRetailerBalance(parantorgid, token);
                var productDetailsDto = products.Adapt<List<RetailerBalanceDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = productDetailsDto;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                          {
                                                ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                Error = Resource.ContactToAdmin }
                                          };
                _logger.LogException(ex);
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetProductWiseBuySaleInventoryDetails(string fromdate, string todate, int productid, int channelid, int distributororgid, CancellationToken token)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var _inventory = await _repositoryManager.inventoryRepository.GetProductWiseBuySaleInventoryDetails(fromdate, todate, productid, channelid, distributororgid, token);
                var userDtos = _inventory.Adapt<IEnumerable<ProductWiseBuySaleInventoryDetailsResInfoDto>>();
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Product Wise Buy Sale Inventory Details");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetRetailerInfoByDistributorId(string fromdate, string todate, int offsetrows, int fetchrows, int distributororgid, CancellationToken token)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var _inventory = await _repositoryManager.inventoryRepository.GetRetailerInfoByDistributorId(fromdate, todate, offsetrows, fetchrows, distributororgid, token);
                var userDtos = _inventory.Adapt<IEnumerable<RetailerInfoByDistributorIdDto>>();
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Retailer Info By DistributorId");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetDistributortxnDetails(int distributororgid, string fromdate, string todate, CancellationToken token)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var txnDetail = await _repositoryManager.inventoryRepository.GetDistributortxnDetails(distributororgid,  fromdate,  todate, token);
                var txnDetailDto = txnDetail.Adapt<List<DistributortxnDetailsDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = txnDetailDto;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                          {
                                                ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                Error = Resource.ContactToAdmin }
                                          };
                _logger.LogException(ex);
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetDistributorDetails(int distributororgid, CancellationToken token)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var txnDetail = await _repositoryManager.inventoryRepository.GetDistributorDetails(distributororgid, token);
                var txnDetailDto = txnDetail.Adapt<List<DistributorDetailsDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = txnDetailDto;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                          {
                                                ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                Error = Resource.ContactToAdmin }
                                          };
                _logger.LogException(ex);
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> GetDynamicSearchProduct(DynamicSearchProductRequestDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var serviceEntity = entity.Adapt<DynamicSearchRequest>();
                var response = await _repositoryManager.inventoryRepository.GetDynamicSearchProduct(serviceEntity, cancellationToken);
                var commDtos = response.Adapt<IEnumerable<DynamicSearchProductDto>>();

                DynamicSearchProductResponse userDetailResponse = new DynamicSearchProductResponse() { ProductListDtos = commDtos };

                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetDynamicSearchProduct");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> UpdateCPDetails(UpdateCPDetailsDto updateCPDetailsDto, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                var isEmailDuplicate = await _repositoryManager.channelPartnerRepository.CheckDuplicateEmailId(updateCPDetailsDto.EmailAddress, updateCPDetailsDto.OrgId);
                var isMobileNumberDuplicate = await _repositoryManager.channelPartnerRepository.CheckDuplicateMobileNumber(updateCPDetailsDto.MobileNumber, updateCPDetailsDto.OrgId);

                if (!isEmailDuplicate && !isMobileNumberDuplicate)
                {
                    var updateCpDetails = updateCPDetailsDto.Adapt<UpdateCPDetails>();
                    var result = await _repositoryManager.channelPartnerRepository.UpdateCPDetails(updateCpDetails, cancellationToken);
                    if (result > 0)
                    {
                        responseModelDto.ResponseCode = "0";
                        responseModelDto.Response = "Email Id and Mobile Number Updated Successfully.";
                    }
                    else
                    {
                        responseModelDto.Response = "Error";
                        responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                    }
                }
                else
                {
                    responseModelDto.Response = "Error";
                    responseModelDto.Errors = new List<ErrorModelDto>();

                    if (isEmailDuplicate)
                    {
                        responseModelDto.Errors.Add(new ErrorModelDto()
                        {
                            ErrorCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserAlreadyExist).ToString(),
                            Error = "Email Address already exists."
                        });
                    }

                    if (isMobileNumberDuplicate)
                    {
                        responseModelDto.Errors.Add(new ErrorModelDto()
                        {
                            ErrorCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.MobileNumberExist).ToString(),
                            Error = "Mobile Number already exists."
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = "Error";
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModelDto;
        }

        public async Task<ResponseModelDto> GetCPDetailsByOrgCode(string orgCode, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var cpDetails = await _repositoryManager.channelPartnerRepository.GetCPDetailsByOrgCode(orgCode, cancellationToken);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = cpDetails.Adapt<GetCPDetailsByOrgCodeDto>();
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                          {
                                                ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                Error = Resource.ContactToAdmin }
                                          };
                _logger.LogException(ex);
            }

            return responseModel;
        }
    }
}