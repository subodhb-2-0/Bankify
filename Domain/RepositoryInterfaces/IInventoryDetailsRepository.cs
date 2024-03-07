using Domain.Entities.Onboarding;

namespace Domain.RepositoryInterfaces
{
    public interface IInventoryDetailsRepository: IGenericRepository<InventoryDlts> {
        Task<IEnumerable<InventoryDlts>> GetByOrgIdAndInventoryIdAsync(int orgId, int inventoryId, CancellationToken cancellationToken = default);
    }
}
