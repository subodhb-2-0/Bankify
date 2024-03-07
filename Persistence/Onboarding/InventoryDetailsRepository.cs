using Contracts.Onboarding;
using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;

namespace Persistence.Onboarding
{
    public class InventoryDetailsRepository : IInventoryDetailsRepository
    {
        private readonly DapperContext _context;
        public InventoryDetailsRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<InventoryDlts> AddAsync(InventoryDlts entity, CancellationToken cancellationToken = default)
        {
            string query = "INSERT INTO \"Inventory\".tbl_sc_inventory_dtls(inventoryId, orgid, solddate, status) VALUES(@inventoryid, @orgid, @solddate, @status)";
            var paramas = new
            {
                inventoryId = entity.inventoryId,
                orgid = entity.orgId,
                soldDate = entity.soldDate,
                status = entity.status,
            };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public Task<InventoryDlts> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InventoryDlts>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string query = String.Format("SELECT * FROM Inventory.tbl_sc_inventory_dtls");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<InventoryDlts>();
                }
            }
        }

        public Task<InventoryDlts> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InventoryDlts>> GetByOrgIdAndInventoryIdAsync(int orgId, int inventoryid, CancellationToken cancellationToken = default)
        {
            string query = "SELECT * FROM \"Inventory\".tbl_sc_inventory_dtls where orgId = @orgId AND inventoryid = @inventoryid";
            var _params = new { orgId = orgId, inventoryid = inventoryid };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query, _params))
                {
                    return multi.Read<InventoryDlts>();
                }
            }
        }

        public Task<InventoryDlts> UpdateAsync(InventoryDlts entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}
