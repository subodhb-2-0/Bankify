using Contracts.Onboarding;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SalesChannelController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        private readonly BaseEntityHelper _baseEntityHelper;
        private readonly IUserProvider _userProvider;
        public SalesChannelController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
            _baseEntityHelper = new BaseEntityHelper();
            _userProvider = new UserProvider(new HttpContextAccessor { HttpContext = HttpContext});
        }


        /// <summary>
        /// Get cpaf details
        /// </summary>
        /// <returns>Get cpaf details</returns>
        [HttpGet]
        public async Task<ActionResult> GetCpafDetails(int cpafNumber, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetCpafDetails(cpafNumber, cancellationToken);
            return Ok(userResponseModel);
        }

        /// <summary>
        /// Get cpaf details
        /// </summary>
        /// <returns>Get cpaf details</returns>
        [HttpGet]
        public async Task<ActionResult> GetInventoryDetails(int orgId, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetInventoryDetails(orgId, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// GetCPAquisiotion details
        /// </summary>
        /// <returns>Channel partner aquisition details</returns>
        [HttpPost]
        public async Task<ActionResult> GetCPAcquistionDetails(CPAquisitionDetailRequestDto request, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetCPAcquistionDetails(request, cancellationToken);                                                                 
            return Ok(userResponseModel);
        }


        /// <summary>
        /// sell inventory
        /// </summary>
        /// <param name="SellInventoryRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>response model</returns>
        [HttpPost]
        public async Task<ActionResult> SellInventory(SaleInventoryRequestDto request, CancellationToken cancellationToken)
        {
            //var userName = _userProvider.GetUserName();
            var userResponseModel = await _serviceManager.SalesPartnerService.SellInventory(request, cancellationToken);
            return Ok(userResponseModel);
        }
        

        [HttpPost]
        public async Task<ActionResult> SalesChannelGetProductDetails(string? mobilenumber, CancellationToken cancellationToken)
        {
            var getProductDetails = await _serviceManager.SalesPartnerService.SalesChannelGetProductDetails( mobilenumber, cancellationToken);
            return Ok(getProductDetails);
        }
        [HttpGet]
        public async Task<ActionResult> GetListOfActiveProduct( CancellationToken cancellationToken)
        {
            var getProductDetails = await _serviceManager.SalesPartnerService.GetListOfActiveProduct( cancellationToken);
            return Ok(getProductDetails);
        }
        [HttpPut]
        public async Task<ActionResult> RetailerProductUpdateByProductId(int? productId, string? mobileNumber, string? orgCode, CancellationToken cancellationToken)
        {
            var getProductDetails = await _serviceManager.SalesPartnerService.RetailerProductUpdateByProductId(productId, mobileNumber, orgCode, cancellationToken);
            return Ok(getProductDetails);
        }
        [HttpGet(Name = "GetCPAquisitionReportDetails")]
        public async Task<ActionResult> GetCPAquisitionReportDetails(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetCPAquisitionReportDetails(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet(Name = "GetAEPSOnBoardingDetails")]
        public async Task<ActionResult> GetAEPSOnBoardingDetails(int? pageSize, int? pageNumber, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.aEPSService.GetAEPSOnBoardingDetails(pageSize, pageNumber, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetCPDetilsByMobileNumber")]
        public async Task<ActionResult> GetCPDetilsByMobileNumber(string mobileNumber, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetCPDetilsByMobileNumber(mobileNumber, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name ="GetEmployeeDetails")]
        public async Task<ActionResult> GetEmployeeDetailsByEmployeeCodeAndMobileNumber(string employeeCode, string mobileNumber, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetEmployeeDetailsByEmployeeCodeAndMobileNumber(employeeCode, mobileNumber, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPut]
        public async Task<ActionResult> ReMapEmployeeToCP(EmployeeCPMapDto employeeCPMappingDto, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.ReMapEmployeeToCP(employeeCPMappingDto, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetLoginIds(string loginId, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.GetLoginIds(loginId, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPut]
        public async Task<ActionResult> SubstituteEmployee(SubstituteEmployeeDto substituteEmployeeDto, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.SalesPartnerService.SubstituteEmployee(substituteEmployeeDto, cancellationToken);
            return Ok(userResponseModel);
        }
        
    }
}
