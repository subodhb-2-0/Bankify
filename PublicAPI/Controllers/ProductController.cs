using Contracts;
using Contracts.Acquisition;
using Contracts.Product;
using Contracts.Security;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public ProductController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }
        /// <summary>
        /// Get All Service Catagory
        /// </summary>
        /// <returns>Service Catagory</returns>
        [HttpGet(Name = "GetAllProductList")]
        public async Task<ActionResult> GetAllProductList(int channelId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ProductService.GetAllProductList(channelId, pageSize, pageNumber, orderByColumn, orderBy,searchBy, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetProductDetailsbyId")]
        public async Task<ActionResult> GetProductDetailsbyId(int productId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ProductService.GetProductDetailsbyId( productId,  pageSize,  pageNumber,  orderByColumn,  orderBy, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPost]
        public async Task<ActionResult> ActivaeOrDeactivateProduct(int productId, int status, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ProductService.ActivaeOrDeactivateProduct(productId, status, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddProductName(AddProductDto addProductDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ProductService.AddProductName(addProductDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> AddServiceOffering(AddServiceOfferingDto addServiceOfferingDto , CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ProductService.AddServiceOffering(addServiceOfferingDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> DeactivateServiceOffering(int svcoffid, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ProductService.DeactivateServiceOffering(svcoffid, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpGet(Name = "GetServiceSupplierListbyServiceId")]
        public async Task<ActionResult> GetServiceSupplierListbyServiceId(int serviceid, int status, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ProductService.GetServiceSupplierListbyServiceId( serviceid,  status, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetServiceProviderListbyServiceId")]
        public async Task<ActionResult> GetServiceProviderListbyServiceId(int serviceid, int status, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ProductService.GetServiceProviderListbyServiceId(serviceid, status, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetServiceCSM")]
        public async Task<ActionResult> GetServiceCSM( CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ProductService.GetServiceCSM( cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProductDetailsById(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ProductService.UpdateProductDetailsById(updateProductDto, cancellationToken);
            return Ok(serviceCreateModel);
        }

    }

}
