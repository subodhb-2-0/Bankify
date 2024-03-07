using Contracts.Common;
using Contracts.Onboarding;

namespace Services.Abstractions
{
    public interface ISalesChannelService
    {
        Task<ResponseModelDto> GetInventoryDetails(int orgId, CancellationToken token);
        Task<ResponseModelDto> GetCpafDetails(int cpafNumber, CancellationToken token);
        Task<ResponseModelDto> SellInventory(SaleInventoryRequestDto request, CancellationToken token);
        Task<ResponseModelDto> GetCPAcquistionDetails(CPAquisitionDetailRequestDto request, CancellationToken token);
        Task<ResponseModelDto> SalesChannelGetProductDetails(string? mobilenumber, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListOfActiveProduct(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCPAquisitionReportDetails(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken);
        Task<ResponseModelDto> RetailerProductUpdateByProductId(int? productId, string? mobileNumber, string? orgCode, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCPDetilsByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetEmployeeDetailsByEmployeeCodeAndMobileNumber(string employeeCode, string mobileNumber, CancellationToken cancellationToken);
        Task<ResponseModelDto> ReMapEmployeeToCP(EmployeeCPMapDto employeeCPMappingDto, CancellationToken cancellationToken);
        Task<ResponseModelDto> GetLoginIds(string loginId, CancellationToken cancellationToken);
        Task<ResponseModelDto> SubstituteEmployee(SubstituteEmployeeDto substituteEmployeeDto, CancellationToken cancellationToken);
    }
}