using Contracts.Acquisition;
using Contracts.Common;
using Contracts.Constants;
using Contracts.Error;
using Contracts.Onboarding;
using Contracts.Product;
using Contracts.Response;
using Domain.Entities.Onboarding;
using Domain.Entities.UserManagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;

namespace Services
{
    public class SalesChannelService : ISalesChannelService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public SalesChannelService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        //Todo integration with repositories
        public async Task<ResponseModelDto> GetCpafDetails(int cpafNumber, CancellationToken token)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var channelPartners = await _repositoryManager.channelPartnerRepository.GetCpafDetailsAsync(cpafNumber);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = channelPartners.Adapt<IEnumerable<CpafDetailsDto>>();
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

        public async Task<ResponseModelDto> GetCPAcquistionDetails(CPAquisitionDetailRequestDto request, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetCPAcquistionDetails(request, cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<CPAquisitionDetailDto>>();
                CPAquisitionDetailResponseDto userDetailResponse = new CPAquisitionDetailResponseDto() 
                {
                    cPAquisitionDetailDtos = userDtos,
                    TotalRecord = userDtos.Count(),
                    PageNumber = request.pageNumber,
                    PageSize = request.pageSize
                };
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = userDetailResponse;
                responseModel.TotalCount = userDetailResponse.TotalRecord;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while VerifyChannelPartner {request}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                {
                    ErrorCode = CommonErrorCode.ContactToAdmin,
                    Error = Resource.ContactToAdmin 
                }};
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetInventoryDetails(int orgId, CancellationToken token)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                List<InventoryDetailsDto> inventoryDetailsDto = await GetInventoryDetailsDto(orgId);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = inventoryDetailsDto;
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

        public async Task<ResponseModelDto> SellInventory(SaleInventoryRequestDto request, CancellationToken token)
        {
            //return new ResponseModelDto
            //{
            //    ResponseCode = "sellInventory",
            //    Response = "success",
            //};
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var serviceEntity = request.Adapt<SaleInventoryRequestDto>();
                var _inventory = await _repositoryManager.inventoryRepository.SellInventory(request, token);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _inventory;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Sell Inventory");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> GetCPAquisitionReportDetails(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                // need to change this
                var CPAcquisition = await _repositoryManager.acquisitionRepository.GetCPAquisitionReportDetails(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var userDtos = CPAcquisition.Item1.Adapt<IEnumerable<GetCPAquisitionDto>>();
                GetCPAquisitionResponse getCPAquisitionResponse = new GetCPAquisitionResponse() { getCPAquisitionDtos = userDtos, TotalRecord = CPAcquisition.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = getCPAquisitionResponse;
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
            }

            return responseModel;
        }


        #region private methods
        private async Task<List<InventoryDetailsDto>> GetInventoryDetailsDto(int orgId)
        {
            var inventories = await _repositoryManager.inventoryRepository.GetByOrgIdAsync(orgId);
            List<InventoryDetailsDto> inventoryDetailsDtos = new List<InventoryDetailsDto>();
            foreach (var inventory in inventories)
            {
                var inventoryDetails = await _repositoryManager.inventoryDetailsRepository.GetByOrgIdAndInventoryIdAsync(orgId, inventory.inventoryId);
                //var product = await _repositoryManager.productRepository.GetByIdAsync(inventory.ProductId);
                var product = await _repositoryManager.productRepository.GetProductByIdAsync(inventory.ProductId);
                var channel = await _repositoryManager.channelRepository.GetByIdAsync(inventory.ChannelId);
                var soldCount = inventoryDetails.Count(x => x.status == 2);
                TypeAdapterConfig<(Inventory, InventoryDlts, Product, Channel), InventoryDetailsDto>.NewConfig()
                .Map(dest => dest, (source) => source.Item1)
                .Map(dest => dest, (source) => source.Item2)
                .Map(dest => dest.inventoryBought, (source) => source.Item1.totalInventory)
                .Map(dest => dest.cpafNumber, source => source.Item2.cpafnnumber)
                .Map(dest => dest.productName, source => source.Item3.productName)
                .Map(dest => dest.productId, source => source.Item3.productId)
                .Map(dest => dest.channelId, source => source.Item3.channelId)
                .Map(dest => dest.channelName, source => source.Item4.channelName)
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

                foreach (var inventoryDtls in inventoryDetails)
                {
                    //var inventoryDetail = (inventory, inventoryDtls, product, channel).Adapt<InventoryDetailsDto>();
                    var inventoryDetail = (inventory, inventoryDtls, product, channel).Adapt<InventoryDetailsDto>();

                    inventoryDetail.orgId = inventoryDtls.orgId;
                    inventoryDetail.channelId = channel.channelId;
                    inventoryDetail.channelName = channel.channelName;
                    inventoryDetail.productId = product.FirstOrDefault().productId;
                    inventoryDetail.productName = product.FirstOrDefault().productName;
                    inventoryDetail.inventoryBought = inventory.totalInventory;
                    inventoryDetail.cpafNumber = inventoryDtls.cpafnnumber;
                    inventoryDetail.inventoryId = inventoryDtls.inventoryId;
                    inventoryDetail.inventorySold = soldCount;
                    inventoryDetailsDtos.Add(inventoryDetail);
                }
            }

            return inventoryDetailsDtos;
        }
        private void CreateNewChannelPartner(SellInventoryRequestDto request)
        {
            TypeAdapterConfig<SellInventoryRequestDto, ChannelPartner>.NewConfig()
                .Map(x => x, y => y)
                .Map(x => x.activationDate, () => DateTime.Now)
                .Map(x => x.cpaf, (y) => y.cpafNumber)
                .Map(x => x.enrolmentFeeCharged, (y) => y.packAmt)
                .Map(x => x.parentOrgId, (y) => y.orgId)
                .Map(x => x.paymentOption, (y) => y.paymentMode)
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

            var newChannelPartner = request.Adapt<ChannelPartner>();
            var channelPartner = _repositoryManager.channelPartnerRepository.AddAsync(newChannelPartner);
        }

        private async Task<Users> CreateRetailerSubUser(SellInventoryRequestDto request, Users user)
        {
            TypeAdapterConfig<SellInventoryRequestDto, Users>.NewConfig()
                                .Map((dest) => dest, (source) => source)
                                .Map((dest) => dest.ReportsTo, () => user.Id)
                                .Map((dest) => dest.CreditLimit, (source) => source.packAmt)
                                .Map((dest) => dest.Password, (source) => source.firstName + source.lastName)
                                .Map((dest) => dest.UserId, (source) => source.firstName + source.lastName + new Random().Next(10000))
                                .Map((dest) => dest.RoleId, () => 6)
                                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            var retailer = request.Adapt<Users>();
            return await _repositoryManager.usersRepository.AddAsync(retailer);
        }

        public async Task<ResponseModelDto> SalesChannelGetProductDetails(string? mobilenumber, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                if (mobilenumber?.Count() != 10) throw new Exception("Wrong Mobile Number!");
                SaleChannelGetProductDetailsDto saleChannelGetProductDetailsDto = new SaleChannelGetProductDetailsDto();
                saleChannelGetProductDetailsDto.mobilenumber = mobilenumber;
                var allUsers = await _repositoryManager.inventoryRepository.SalesChannelGetProductDetails(mobilenumber,cancellationToken);
                var getUserDtos = allUsers.Adapt<IEnumerable<SaleChannelGetProductDetailsDto>>();
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = getUserDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Getting Product Details.");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>()
                {
                     new ErrorModelDto()
                     {
                         ErrorCode = CommonErrorCode.ContactToAdmin,
                         Error = Resource.ContactToAdmin
                     }
                };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetListOfActiveProduct( CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.inventoryRepository.GetListOfActiveProduct(cancellationToken);
                var getUserDtos = allUsers.Adapt<IEnumerable<GetListOfActiveProductDto>>();
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = getUserDtos;
                

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while getting list of active product.");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>()
                { new ErrorModelDto()
                {
                    ErrorCode = CommonErrorCode.ContactToAdmin,
                    Error = Resource.ContactToAdmin
                }
                };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> RetailerProductUpdateByProductId(int? productId, string? mobileNumber, string? orgCode, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.inventoryRepository.RetailerProductUpdateByProductId(productId, mobileNumber, orgCode, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = ResponseCode.Success;
                    responseModel.Response = "ProductId has been Updated";
                }
                else
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                    {
                        ErrorCode  = UserManagementError.NoSubUserFound,
                        Error = Resource.UserNotFound }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetCPDetilsByMobileNumber(string mobileNumber, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var cpDetail = await _repositoryManager.channelPartnerRepository.GetCPDetilsByMobileNumber(mobileNumber, cancellationToken);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = cpDetail.Adapt<GetCPDetailsByMobileNumberDto>();
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

        public async Task<ResponseModelDto> GetEmployeeDetailsByEmployeeCodeAndMobileNumber(string employeeCode, string mobileNumber, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var empDetail = await _repositoryManager.channelPartnerRepository.GetEmployeeDetailsByEmployeeCodeAndMobileNumber(employeeCode,mobileNumber, cancellationToken);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = empDetail.Adapt<GetEmployeeDetailsByMobilenoAndCodeDto>();
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

        public async Task<ResponseModelDto> ReMapEmployeeToCP(EmployeeCPMapDto employeeCPMappingDto, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var mappedEmployeeCP = employeeCPMappingDto.Adapt<MapEmployeeToCP>();
                var result = await _repositoryManager.channelPartnerRepository.ReMapEmployeeToCP(mappedEmployeeCP, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = ResponseCode.Success;
                    responseModel.Response = "Mapped Successfully!";
                }
                else
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                    {
                        ErrorCode  = UserManagementError.NoSubUserFound,
                        Error = Resource.UserNotFound }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetLoginIds(string loginId, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var LoginIds = await _repositoryManager.channelPartnerRepository.GetLoginIds(loginId, cancellationToken);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = LoginIds.Adapt<IEnumerable<string>>();
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
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> SubstituteEmployee(SubstituteEmployeeDto substituteEmployeeDto, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var substituteEmployee = substituteEmployeeDto.Adapt<SubstituteEmployee>();
                var result = await _repositoryManager.channelPartnerRepository.SubstituteEmployee(substituteEmployee, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = ResponseCode.Success;
                    responseModel.Response = "Updated Successfully!";
                }
                else
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { 
                        new ErrorModelDto()
                        {
                            ErrorCode = ResponseCode.Error,
                            Error = Resource.UserNotFound
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }


        #endregion
    }
}