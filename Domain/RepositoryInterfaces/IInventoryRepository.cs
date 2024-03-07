using Contracts.Onboarding;
using Domain.Entities.Account;
using Domain.Entities.Onboarding;
using Domain.Entities.Product;
using Domain.Entities.Servicemanagement;

namespace Domain.RepositoryInterfaces
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<IEnumerable<Inventory>> GetByOrgIdAsync(int orgId, CancellationToken cancellationToken = default);

        Task<RetailInventoryResponseModel> BuyRetailerInventory(BuyRetailerInventoryRequestDto requestDto, CancellationToken cancellationToken = default);

        Task<SaleInventoryResponseModel> SellInventory(SaleInventoryRequestDto requestDto, CancellationToken cancellationToken = default);

        Task<IEnumerable<RetailerBalance>> GetRetailerBalance(int parantorgid, CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductWiseBuySaleInventoryDetailsResInfo>> GetProductWiseBuySaleInventoryDetails(string fromdate, string todate, int productid, int channelid, int distributororgid, CancellationToken cancellationToken = default);

        Task<IEnumerable<RetailerInfoByDistributorId>> GetRetailerInfoByDistributorId(string fromdate, string todate, int offsetrows, int fetchrows, int distributororgid, CancellationToken cancellationToken = default);

        Task<IEnumerable<DistributortxnDetails>> GetDistributortxnDetails(int distributororgid, string fromdate, string todate, CancellationToken cancellationToken = default);
        Task<IEnumerable<DistributorDetails>> GetDistributorDetails(int distributororgid, CancellationToken cancellationToken = default);
        Task<IEnumerable<DynamicSearchProduct>> GetDynamicSearchProduct(DynamicSearchRequest entity, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<SalesChannelGetProductDetails>> SalesChannelGetProductDetails(string? mobilenumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetListOfActiveProduct>> GetListOfActiveProduct(CancellationToken cancellationToken = default);
        Task<int> RetailerProductUpdateByProductId(int? productId, string? mobileNumber, string? orgCode, CancellationToken cancellationToken = default);
    }
}
