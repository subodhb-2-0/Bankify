using Contracts.Acquisition;
using Contracts.Bbps;
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
    public class AcquisitionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public AcquisitionController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }
        /// <summary>
        /// Get All Service Catagory
        /// </summary>
        /// <returns>Service Catagory</returns>
        [HttpGet(Name = "GetAcquisitionDetails")]
        public async Task<ActionResult> GetAcquisitionDetails(int? Id, string? Value, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetAcquisitionDetails(Id, Value, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.AddAcquisitonDetails(addAcquisitionDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "VerifyCPApplication")]
        public async Task<ActionResult> VerifyCPApplication(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.VerifyCPApplication( pageSize,  pageNumber,  orderByColumn,  orderBy, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet(Name = "GetListOrgType")]
        public async Task<ActionResult> GetListOrgType(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetListOrgType(cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetListofFeildStaff")]
        public async Task<ActionResult> GetListofFeildStaff(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetListofFeildStaff(cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetParentOrgId")]
        public async Task<ActionResult> GetParentOrgId(int? OrgType, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetParentOrgId(OrgType, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "VerifyChannelPartner")]
        public async Task<ActionResult> VerifyChannelPartner(int? Id, string? Value, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.VerifyChannelPartner(Id, Value, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet(Name = "GetCPDetailsforApproval")]
        public async Task<ActionResult> GetCPDetailsforApproval(int? OrgId, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetCPDetailsforApproval(OrgId, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet(Name = "GetCpDetailsById")]
        public async Task<ActionResult> GetCpDetailsById(int? orgId, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetCpDetailsById(orgId, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmRejectCP(ConfirmRejectCPInfoDto addAcquisitionDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.ConfirmRejectCP(addAcquisitionDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCPStatus(int orgid, int status, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.UpdateCPStatus(orgid,status, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "ViewActiveCP")]
        public async Task<ActionResult> ViewActiveCP(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.ViewActiveCP( pageSize,  pageNumber,  orderByColumn,  orderBy,cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetListofChannels")]
        public async Task<ActionResult> GetListofChannels(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetListofChannels(cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetProductsforChannel")]
        public async Task<ActionResult> GetProductsforChannel(int? channelId,CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AcquisitionService.GetProductsforChannel(channelId,cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "getCPDetailsByName")]
        public async Task<ActionResult> getCPDetailsByName(int? orgType, string? orgName, CancellationToken cancellationToken)
        {
            var CPDetailsResponseModel = await _serviceManager.AcquisitionService.GetCpdetailsByNameAsync(orgType, orgName, cancellationToken);
            return Ok(CPDetailsResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> ApproveRejectCP(ApproveRejectCPInfoDto addAcquisitionDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.ApproveRejectCP(addAcquisitionDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCPByDistributor(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.CreateCPByDistributor(addAcquisitionDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        //GET: api/<LocationController>
        [HttpGet]
        [ActionName("GetStateCityByPinCodeAsync")]
        public async Task<ActionResult> GetStateCityByPinCodeAsync(int pinCode, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.GetCityAreaByPinCode(pinCode, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetCpafByOrgid(long orgid, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.GetCpafByOrgid(orgid, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpPost]
        public async Task<ActionResult> ApproveCPByActivateDate(int orgid, int status, string activationdate, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.ApproveCPByActivateDate(orgid, status, activationdate, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchCP(DynamicSearchModelCPDto request, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AcquisitionService.GetDynamicSearchCPAsync(request, cancellationToken);
            return Ok(serviceCreateModel);
        }
    }

}
