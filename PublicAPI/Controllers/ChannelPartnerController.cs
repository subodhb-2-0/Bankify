using Contracts;
using Contracts.Onboarding;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Middleware;
using PublicAPI.Utility;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ChannelPartnerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public ChannelPartnerController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }

        /// <summary>
        /// GetChannelType
        /// </summary>
        /// <returns>Channel types</returns>
        [HttpGet]
        public async Task<ActionResult> GetChannelType(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetChannelTypes(cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get product details
        /// </summary>
        /// <returns>Product details</returns>
        [HttpGet]
        public async Task<ActionResult> GetProductDetails(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetProductDetails(cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Buy retailers inventory
        /// </summary>
        /// <param name="BuyRetailerInventoryRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response model</returns>
        [HttpPost]
        public async Task<ActionResult> BuyRetailerInventory(BuyRetailerInventoryRequestDto request, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.BuyRetailerInventory(request, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetRetailerBalance(int parenetorgid, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetRetailerBalance(parenetorgid, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetProductWiseBuySaleInventoryDetails(string fromdate, string todate, int productid, int channelid, int distributororgid, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetProductWiseBuySaleInventoryDetails(fromdate, todate, productid, channelid, distributororgid, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetRetailerInfoByDistributorId(string fromdate, string todate, int offsetrows, int fetchrows, int distributororgid, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetRetailerInfoByDistributorId(fromdate, todate, offsetrows, fetchrows, distributororgid, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetDistributortxnDetails(int distributororgid, string fromdate, string todate, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetDistributortxnDetails(distributororgid,  fromdate,  todate, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetDistributorDetails(int distributororgid, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ChannelPartnerService.GetDistributorDetails(distributororgid, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchProduct(DynamicSearchProductRequestDto entity, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ChannelPartnerService.GetDynamicSearchProduct(entity, cancellationToken);

            return Ok(serviceResponseModel);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCPDetails(UpdateCPDetailsDto updateCPDetailsDto, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ChannelPartnerService.UpdateCPDetails(updateCPDetailsDto, cancellationToken);
            return Ok(serviceResponseModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetCPDetailsByOrgCode(string orgCode, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.ChannelPartnerService.GetCPDetailsByOrgCode(orgCode, cancellationToken);
            return Ok(serviceResponseModel);
        }
    }
}
