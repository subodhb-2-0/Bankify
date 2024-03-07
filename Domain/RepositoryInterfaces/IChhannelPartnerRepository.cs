using Domain.Entities.Onboarding;

namespace Domain.RepositoryInterfaces
{
    public interface IChhannelPartnerRepository: IGenericRepository<ChannelPartner> {
        Task<IEnumerable<ChannelPartner>> GetCpafDetailsAsync(int cpafNumber, CancellationToken cancellationToken = default);
        Task<CPDetailsByMobileNumber> GetCPDetilsByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default);
        Task<EmployeeDetailCPMapping> GetEmployeeDetailsByEmployeeCodeAndMobileNumber(string employeeCode, string mobileNumber, CancellationToken cancellationToken = default);
        Task<int> ReMapEmployeeToCP(MapEmployeeToCP mapEmployeeToCP, CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetLoginIds(string loginId, CancellationToken cancellationToken = default);
        Task<int> SubstituteEmployee(SubstituteEmployee substituteEmployeeDto, CancellationToken cancellationToken = default);
        Task<int> UpdateCPDetails(UpdateCPDetails updateCPDetails, CancellationToken cancellationToken = default);
        Task<bool> CheckDuplicateMobileNumber(string mobileNumber, int orgId);
        Task<bool> CheckDuplicateEmailId(string emailAddress, int orgId);
        Task<CPDetailsByOrgCode> GetCPDetailsByOrgCode(string orgCode, CancellationToken cancellationToken = default);
    }
}
