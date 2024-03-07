using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;
using Contracts.WorkingCapital;
using Contracts.FundTransfer;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class FundTransferController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public FundTransferController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }

        [HttpGet]
        public async Task<ActionResult> GetOrgDetails(int orgId, CancellationToken cancellationToken)
        {
            var getOrgDetails = await _serviceManager.FundTransferService.GetOrgDetails(orgId, cancellationToken);
            return Ok(getOrgDetails);
        }
        [HttpGet]
        public async Task<ActionResult> GetListOfChannelPartner(int distributororgid, int retailerorgid, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.FundTransferService.GetListOfChannelPartner(distributororgid, retailerorgid, cancellationToken);
            return Ok(channelPartnerList);
        }

        [HttpGet]
        public async Task<ActionResult> GetChannelPartnerList(int distributorid, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.FundTransferService.GetChannelPartnerList(distributorid, cancellationToken);
            return Ok(channelPartnerList);
        }

        [HttpPost]
        public async Task<ActionResult> InitiateFundTransfer(FundTransferRequestDto request, CancellationToken cancellationToken)
        {
            var fundTransferResponse = await _serviceManager.FundTransferService.InitiateFundTransfer(request, cancellationToken);
            return Ok(fundTransferResponse);
        }
    }

}
