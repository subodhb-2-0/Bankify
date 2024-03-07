using Contracts.AEPS;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AEPSController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        public AEPSController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
        }
        [HttpPost]
        public async Task<ActionResult> OnboardingAsync(AgentOnboardingRequestDto entity, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.aEPSService.OnboardingAsync(entity, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> OnboardingPaySprintAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _serviceManager.aEPSService.OnboardingPaytmAsync(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> GetPaySprintOnboardingDetails(PaySprintOnboardingDetailsDto request, CancellationToken cancellationToken)
        {
            var response = await _serviceManager.aEPSService.GetPaySprintOnboardingDetailsAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
