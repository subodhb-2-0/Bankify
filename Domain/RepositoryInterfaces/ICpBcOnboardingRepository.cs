using Contracts.AEPS;
using Domain.Entities.Onboarding;

namespace Domain.RepositoryInterfaces
{
    public interface ICpBcOnboardingRepository
    {
        Task<List<CpBcOnboarding>> GetAEPSOnBoardingDetails(int? pageSize, int? pageNumber, CancellationToken cancellationToken);
        Task<int> AddPaytmOnboardingAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken);
        Task<bool> CheckPaySprintOnboardingAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken);
        Task<int> UpdatePaytmOnboardingAsync(int updatedStatus, PaySprintOnboardingDetails request);
        Task<string> GetStateCodeByPincode(string pincode);
        Task<IEnumerable<PaySprintOnboardingDetails>> GetPaySprintOnboardingDetailsAsync(PaySprintOnboardingDetailsDto request, CancellationToken cancellationToken);
    }
}
