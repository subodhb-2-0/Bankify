using Contracts.Common;
using Contracts.Onboarding;
using Contracts.ServiceManagement;

namespace Services.Abstractions
{
    public interface IChannelPartnerService
    {
        Task<ResponseModelDto> GetChannelTypes(CancellationToken token);
        Task<ResponseModelDto> GetProductDetails(CancellationToken token);
        Task<ResponseModelDto> BuyRetailerInventory(BuyRetailerInventoryRequestDto request, CancellationToken token);
        Task<ResponseModelDto> GetRetailerBalance(int parantorgid, CancellationToken token);
        Task<ResponseModelDto> GetProductWiseBuySaleInventoryDetails(string fromdate, string todate, int productid, int channelid, int distributororgid, CancellationToken token);

        Task<ResponseModelDto> GetRetailerInfoByDistributorId(string fromdate, string todate, int offsetrows, int fetchrows, int distributororgid, CancellationToken token);
        Task<ResponseModelDto> GetDistributortxnDetails(int distributororgid, string fromdate, string todate, CancellationToken token);
        Task<ResponseModelDto> GetDistributorDetails(int distributororgid, CancellationToken token);

        Task<ResponseModelDto> GetDynamicSearchProduct(DynamicSearchProductRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateCPDetails(UpdateCPDetailsDto updateCPDetailsDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetCPDetailsByOrgCode(string orgCode, CancellationToken cancellationToken);
    }
}