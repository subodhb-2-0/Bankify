using Contracts;

namespace Services.Abstractions
{
    public interface IStoreService
    {
        Task<string> GetMerchandiseAsync(CancellationToken cancellationToken);
        Task<string> GetOrderAddressAsync(int orgId, CancellationToken cancellationToken);
        Task<string> PlaceOrderAsync(PlaceOrderDto request, CancellationToken cancellationToken);
        Task<string> GetOrderHistoryListAsync(OrderHistoryListDto request, CancellationToken cancellationToken);
    }
}
