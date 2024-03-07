using Contracts.AEPS;
using Contracts.Common;

namespace Services.Abstractions
{
    public interface IAEPSService 
    {
        Task<ResponseModelDto> OnboardingAsync(AgentOnboardingRequestDto entity,CancellationToken cancellationToken = default);
        Task<ResponseModelDto> OnboardingPaytmAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAEPSOnBoardingDetails(int? pageSize, int? pageNumber, CancellationToken cancellationToken);
        Task<ResponseModelDto> GetPaySprintOnboardingDetailsAsync(PaySprintOnboardingDetailsDto request, CancellationToken cancellationToken = default);
    }
}
;