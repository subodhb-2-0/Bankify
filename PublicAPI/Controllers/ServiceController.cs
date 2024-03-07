using Contracts.Bbps;
using Contracts.Security;
using Contracts.ServiceManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public ServiceController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }
        /// <summary>
        /// Get All Service Catagory
        /// </summary>
        /// <returns>Service Catagory</returns>
        [HttpGet(Name = "GetAllServiceCatagory")]
        public async Task<ActionResult> GetAllServiceCatagoryAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetAllServiceCatagory(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);

            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get All Service
        /// </summary>
        /// <returns>AllService</returns>
        [HttpGet(Name = "GetAllService")]
        public async Task<ActionResult> GetAllServiceAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetAllService(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get All Supplier
        /// </summary>
        /// <returns>AllSupplier</returns>
        [HttpGet(Name = "AllSupplier")]
        public async Task<ActionResult> AllSupplierAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetAllSupplier(pageSize,pageNumber,orderByColumn,orderBy,searchBy, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get All Provider
        /// </summary>
        /// <returns>AllProvider</returns>
        [HttpGet(Name = "AllProvider")]
        public async Task<ActionResult> AllProviderAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetAllProvider(pageSize,pageNumber,orderByColumn,orderBy,searchBy, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get All service
        /// </summary>
        /// <returns>Allservice</returns>
        [HttpGet(Name = "AllserviceSerch")]
        public async Task<ActionResult> AllserviceSerchAsync(int serviceid, string? servicename, int servicecategoryid, int status, int creator, DateTime creationdate, CancellationToken cancellationToken = default)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetAllServiceBySearch(serviceid, servicename, servicecategoryid, status, creator, creationdate, cancellationToken);

            return Ok(userResponseModel);
        }
        /// <summary>
        /// Add Master service Models
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddmasterserviceModels(MasterServiceCreateDto serviceCreateDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ServiceManagementService.CreateMasterServiceAsync(serviceCreateDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        /// <summary>
        /// Add Master supplier Models
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddmastersupplierModels(CreateMasterSupplierDto supplierCreateDto, CancellationToken cancellationToken)
        {
            var supplierCreateModel = await _serviceManager.ServiceManagementService.CreateMasterSupplierAsync(supplierCreateDto, cancellationToken);
            return Ok(supplierCreateModel);
        }
        /// <summary>
        /// Add Psto supplier Models
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddPstosupplierModels(CreatePstoSuppliersDto supplierCreateDto, CancellationToken cancellationToken)
        {
            var supplierCreateModel = await _serviceManager.ServiceManagementService.CreatePstoSupplierAsync(supplierCreateDto, cancellationToken);
            return Ok(supplierCreateModel);
        }
        /// <summary>
        /// Update master service Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditServiceModels(MasterServiceUpdateDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var serviceUpdateModel = await _serviceManager.ServiceManagementService.EditMasterServiceAsync(serviceUpdateDto, cancellationToken);
            return Ok(serviceUpdateModel);
        }
        /// <summary>
        /// Update service status Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditServiceStatusModels(UpdateServiceStatusDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var serviceUpdateModel = await _serviceManager.ServiceManagementService.EditServiceStatusAsync(serviceUpdateDto, cancellationToken);
            return Ok(serviceUpdateModel);
        }
        /// <summary>
        /// Update service provider Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditServiceProviderModels(UpdateServiceProviderDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var serviceUpdateModel = await _serviceManager.ServiceManagementService.EditServiceProviderAsync(serviceUpdateDto, cancellationToken);
            return Ok(serviceUpdateModel);
        }
        /// <summary>
        /// Update service provider staus Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditServiceProviderstausModels(UpdateServiceProviderStatusDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var serviceUpdateModel = await _serviceManager.ServiceManagementService.EditServiceProviderStatusAsync(serviceUpdateDto, cancellationToken);
            return Ok(serviceUpdateModel); 
        }
        /// <summary>
        /// Get All service Type details
        /// </summary>
        /// <returns>All service type details</returns>
        [HttpGet]
        public async Task<ActionResult> GetViewBySupplierType(int supplierid, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ServiceManagementService.GetViewBySupplierAsync(supplierid,cancellationToken);

            return Ok(serviceResponseModel);
        }
        /// <summary>
        /// Get All service Provider 
        /// </summary>
        /// <returns>All service Provider </returns>
        [HttpGet]
        public async Task<ActionResult> GetServiceProvider(int ServiceId, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ServiceManagementService.GetServiceProviderAsync(ServiceId, cancellationToken);

            return Ok(serviceResponseModel);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateServiceSupplierData(UpdateServiceSupplierDataDto serviceUpdateDto, CancellationToken cancellationToken)
        {
            var serviceUpdateModel = await _serviceManager.ServiceManagementService.UpdateServiceSupplierData(serviceUpdateDto, cancellationToken);
            return Ok(serviceUpdateModel);
        }

        [HttpGet(Name = "GetMobileDefaulter")]
        public async Task<ActionResult> GetMobileDefaulterAsync(int id, CancellationToken cancellationToken = default)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetMobileDefaulterByIdAsync(id, cancellationToken);

            return Ok(userResponseModel);
        }

        [HttpGet(Name = "GetPanDefaulter")]
        public async Task<ActionResult> GetPanDefaulterAsync(int id, CancellationToken cancellationToken = default)
        {
            var userResponseModel = await _serviceManager.ServiceManagementService.GetPanDefaulterByIdAsync(id, cancellationToken);

            return Ok(userResponseModel);
        }



        [HttpPost]
        public async Task<ActionResult> DeactivateAssignProviderBySuppId(int suppspmapid, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ServiceManagementService.DeactivateAssignProviderBySuppId(suppspmapid, cancellationToken);

            return Ok(serviceResponseModel);
        }
        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchService(DynamicSearchRequestDto entity, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ServiceManagementService.GetDynamicSearchService(entity, cancellationToken);

            return Ok(serviceResponseModel);
        }
        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchServiceProvider(DynamicSearchRequestDto entity, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ServiceManagementService.GetDynamicSearchServiceProvider(entity, cancellationToken);

            return Ok(serviceResponseModel);
        }
        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchServiceSupplier(DynamicSearchRequestDto entity, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ServiceManagementService.GetDynamicSearchServiceSupplier(entity, cancellationToken);

            return Ok(serviceResponseModel);
        }

    }
}
