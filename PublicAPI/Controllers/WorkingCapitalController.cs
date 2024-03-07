using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;
using Contracts.WorkingCapital;
using Contracts.Account;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class WorkingCapitalController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public WorkingCapitalController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }

        [HttpGet]
        public async Task<ActionResult> GetListOfPaymentMode(int orgId, int orgType, CancellationToken cancellationToken)
        {
            var getListOfPaymentMode = await _serviceManager.WorkingCapitalService.GetListOfPaymentMode(orgId, orgType, cancellationToken);
            return Ok(getListOfPaymentMode);
        }
        [HttpPost]
        public async Task<ActionResult> CheckWCPayments(WCPaymentRequestDTO request, CancellationToken cancellationToken)
        {
            var getListOfPaymentMode = await _serviceManager.WorkingCapitalService.CheckWCPayments(request, cancellationToken);
            return Ok(getListOfPaymentMode);
        }



        [HttpPost]
        public async Task<ActionResult> InitiatePayment(WCInitiatePaymentDto request, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.WorkingCapitalService.InititatePayment(request, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> UPIUpdatePayment(PaymentUpdateInModelDto entity, CancellationToken cancellationToken = default)
        {
            var serviceCreateModel = await _serviceManager.FundTransferService.UPIUpdatePayment(entity, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetOrgDetail(CancellationToken cancellationToken)
        {
            var getListOfPaymentMode = await _serviceManager.WorkingCapitalService.GetOrgDetail(cancellationToken);
            return Ok(getListOfPaymentMode);
        }
    }

}
