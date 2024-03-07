using Contracts.Bbps;
using Contracts.Common;
using Contracts.Constants;
using Contracts.Error;
using Contracts.Response;
using Contracts.ServiceManagement;
using Domain.Entities.Servicemanagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class ServiceManagementService : IServiceManagementRepositoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public ServiceManagementService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task<ResponseModelDto> GetAllServiceCatagory(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allServiceCats = await _repositoryManager.serviceManagementRepository.GetAllServiceCatagoryAsync(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var serviceDtos = allServiceCats.Item1.Adapt<IEnumerable<GetServiceManagementDto>>();

                ServiceManagementResponse serviceManagementResponse = new ServiceManagementResponse() { ServiceDetails = serviceDtos, TotalRecord = allServiceCats.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };


                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = serviceManagementResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching service ");
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

        public async Task<ResponseModelDto> GetAllService(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allServiceCats = await _repositoryManager.serviceManagementRepository.GetAllServiceAsync(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
                var serviceDtos = allServiceCats.Item1.Adapt<IEnumerable<GetMasterServiceDto>>();

                GetAllServiceResponse serviceManagementResponse = new GetAllServiceResponse()
                {
                    AllServiceDetails = serviceDtos, TotalRecord = allServiceCats.Item2,
                    PageNumber = pageNumber, PageSize = pageSize,
                    OrderBy = orderBy, orderByColumn = orderByColumn
                };

                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = serviceManagementResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching service ");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                             ErrorCode = CommonErrorCode.ContactToAdmin,
                                             Error = Resource.ContactToAdmin }
                                        };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetAllSupplier(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allServiceCats = await _repositoryManager.serviceManagementRepository.GetAllSupplierAsync(pageSize,pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
                var serviceDtos = allServiceCats.Item1.Adapt<IEnumerable<GetSupplierDto>>();
                GetAllSupplierResponse serviceManagementResponse = new GetAllSupplierResponse()
                {
                    SupplierDetails = serviceDtos, TotalRecord = allServiceCats.Item2,
                    PageNumber = pageNumber, PageSize = pageSize,
                    OrderBy = orderBy, orderByColumn = orderByColumn
                };

                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = serviceManagementResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching supplier ");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                             ErrorCode = CommonErrorCode.ContactToAdmin,
                                             Error = Resource.ContactToAdmin }
                                        };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetAllProvider(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy , CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allServiceCats = await _repositoryManager.serviceManagementRepository.GetAllProviderAsync(pageSize, pageNumber,orderByColumn,orderBy, searchBy, cancellationToken);
                var serviceDtos = allServiceCats.Item1.Adapt<IEnumerable<GetSupplierDto>>();

                GetAllSupplierResponse serviceManagementResponse = new GetAllSupplierResponse() 
                {
                    SupplierDetails = serviceDtos, TotalRecord = allServiceCats.Item2,
                    PageNumber = pageNumber, PageSize = pageSize,
                    OrderBy = orderBy, orderByColumn = orderByColumn
                };

                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = serviceManagementResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching supplier ");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                            ErrorCode = CommonErrorCode.ContactToAdmin,
                                            Error = Resource.ContactToAdmin }
                                        };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetAllServiceBySearch(int serviceid, string? servicename, int servicecategoryid, int status, int creator, DateTime creationdate, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allService = await _repositoryManager.serviceManagementRepository.GetAllServiceBySearchAsync(serviceid, servicename, servicecategoryid, status, creator, creationdate, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var serviceDtos = allService.Adapt<IEnumerable<GetMasterServiceDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = serviceDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching service {serviceid}");
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
        public async Task<ResponseModelDto> CreateMasterServiceAsync(MasterServiceCreateDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<MasterServiceCreate>();
                await _repositoryManager.serviceManagementRepository.AddMasterServiceAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while create service");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public async Task<ResponseModelDto> CreateMasterSupplierAsync(CreateMasterSupplierDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<CreateMasterSupplier>();
                var response = await _repositoryManager.serviceManagementRepository.AddMasterSupplierAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = response;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while create Supplier");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public async Task<ResponseModelDto> CreatePstoSupplierAsync(CreatePstoSuppliersDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<CreatePstoSuppliers>();
                await _repositoryManager.serviceManagementRepository.AddPstoSupplierAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while create Psto Supplier");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        public ResponseModelDto ValidateEditMasterService(MasterServiceUpdateDto entity)
        {
            ResponseModelDto responseModel = null;


            var context = new ValidationContext(entity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                responseModel = new ResponseModelDto() { Errors = new List<ErrorModelDto>() };
                foreach (var result in results)
                {

                    var _error = result.ToString().Split('|');

                    responseModel.ResponseCode = _error[0];
                    responseModel.Response = _error[1];
                    responseModel.Errors.Add(new ErrorModelDto() { Error = _error[1], ErrorCode = _error[1] });
                }

            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditMasterServiceAsync(MasterServiceUpdateDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<MasterServiceUpdate>();
                await _repositoryManager.serviceManagementRepository.UpdateMasterServiceAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while update master service");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> GetViewBySupplierAsync(int supplierid, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allComission = await _repositoryManager.serviceManagementRepository.GetViewBySupplier(supplierid, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = allComission.Adapt<IEnumerable<GetViewBySupplierDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching service Type");
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
        public async Task<ResponseModelDto> GetServiceProviderAsync(int ServiceId, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allComission = await _repositoryManager.serviceManagementRepository.GetServiceProvider(ServiceId, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = allComission.Adapt<IEnumerable<GetServiceProviderByService>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching service Provider");
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
        public async Task<ResponseModelDto> EditServiceStatusAsync(UpdateServiceStatusDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateServiceStatus>();
                await _repositoryManager.serviceManagementRepository.UpdateServiceStatusAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while update service status");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditServiceProviderAsync(UpdateServiceProviderDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateServiceProvider>();
                await _repositoryManager.serviceManagementRepository.UpdateServiceProviderAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while update service provider");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditServiceProviderStatusAsync(UpdateServiceProviderStatusDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateServiceProviderStatus>();
                await _repositoryManager.serviceManagementRepository.UpdateServiceProviderStatusAsync(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while update service provider status");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public Task<ResponseModelDto> AddAsync(GetServiceManagementDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> UpdateAsync(GetServiceManagementDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ResponseModelDto Validate(GetServiceManagementDto entity)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseModelDto> UpdateServiceSupplierData(UpdateServiceSupplierDataDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<UpdateServiceSupplierData>();
                await _repositoryManager.serviceManagementRepository.UpdateServiceSupplierData(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while update service supplier");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetMobileDefaulterByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();   

            try
            {
                var _getMobileDefaulter = await _repositoryManager.serviceManagementRepository.GetMobileDefaulterById(id, cancellationToken);
               
                if (_getMobileDefaulter == null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                          ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                          Error = Resource.UserNotFound
                        }};
                }
                else
                {
                    var mobDto = _getMobileDefaulter.Adapt<MobileDefaultDto>();
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                    responseModel.Data = mobDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Mobile Defaulter {id}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
             ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
             Error = Resource.ContactToAdmin } };
            }

            return (responseModel);
        }

        public async Task<ResponseModelDto> GetPanDefaulterByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();

            try
            {
                var _getPanDefaulter = await _repositoryManager.serviceManagementRepository.GetPanDefaulterById(id, cancellationToken);
                
                if (_getPanDefaulter == null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                          ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                          Error = Resource.UserNotFound
                        }};
                }
                else
                {
                    var panDto = _getPanDefaulter.Adapt<PanDefaultDto>();
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                    responseModel.Data = panDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the Pan Defaulter {id}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
             ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
             Error = Resource.ContactToAdmin } };
            }

            return (responseModel);
        }

        public async Task<ResponseModelDto> DeactivateAssignProviderBySuppId(int suppspmapid, CancellationToken cancellationToken)
        {
            var responseModelDto = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.serviceManagementRepository.DeactivateAssignProviderBySuppId(suppspmapid, cancellationToken);
                if (result > 0)
                {
                    responseModelDto.ResponseCode = "0";
                    //responseModelDto.Response = status == 3 ? "Product has been deactivated" : "Product has been activated";
                    responseModelDto.Response = "Success";
                }
                else
                {
                    responseModelDto.Response = "Error";
                    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
             ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
             Error = Resource.UserNotFound } };
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
        public async Task<ResponseModelDto> GetDynamicSearchService(DynamicSearchRequestDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var serviceEntity = entity.Adapt<DynamicSearchRequest>();
                var response = await _repositoryManager.serviceManagementRepository.GetDynamicSearchService(serviceEntity, cancellationToken);

                var commDtos = response.Adapt<IEnumerable<DynamicManageServiceDto>>();
                GetMasterServiceResponse serviceResponse = new GetMasterServiceResponse() { AllServiceDetails = commDtos };
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = serviceResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetDynamicSearchService");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
             ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
             Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetDynamicSearchServiceProvider(DynamicSearchRequestDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var serviceEntity = entity.Adapt<DynamicSearchRequest>();
                var response = await _repositoryManager.serviceManagementRepository.GetDynamicSearchServiceProvider(serviceEntity, cancellationToken);

                var commDtos = response.Adapt<IEnumerable<DynamicManageServiceProviderDto>>();

                DynamicManageServiceProviderResponse providerResponse = new DynamicManageServiceProviderResponse()
                { SupplierDetails = commDtos };

                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetDynamicSearchServiceProvider");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
             ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
             Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetDynamicSearchServiceSupplier(DynamicSearchRequestDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var serviceEntity = entity.Adapt<DynamicSearchRequest>();
                var response = await _repositoryManager.serviceManagementRepository.GetDynamicSearchServiceSupplier(serviceEntity, cancellationToken);
                var commDtos = response.Adapt<IEnumerable<DynamicManageServiceProviderDto>>();

                DynamicManageServiceProviderResponse providerResponse = new DynamicManageServiceProviderResponse()
                { SupplierDetails = commDtos };
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetDynamicSearchServiceSupplier ");
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
