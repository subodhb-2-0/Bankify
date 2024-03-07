using Dapper;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using Domain.RepositoryInterfaces;
using System.Data;
using System.Linq;
using static Dapper.SqlMapper;

namespace Persistence
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly DapperContext _context;
        public MasterDataRepository(DapperContext context)
        {
            _context = context;
        }

        public Task<MasterDataModel> AddAsync(MasterDataModel entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<MasterDataModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MasterDataModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<MasterDataModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MasterDataModel>> GetDepartmentListAsync(CancellationToken cancellationToken = default)
        {
            var query = "SELect param_key as Key, param_value1 as Value FROM common.tbl_mst_parameter where LOWER(param_group)='dept'";


            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var designaitonList = await dbConnection.QueryAsync<MasterDataModel>(query);
                return designaitonList;
            }
        }

        public async Task<IEnumerable<MasterDataModel>> GetDesignationListAsync(CancellationToken cancellationToken = default)
        {
            var query = "SELect param_key as Key, param_value1 as Value FROM common.tbl_mst_parameter where LOWER(param_group)='desig'";


            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var designaitonList = await dbConnection.QueryAsync<MasterDataModel>(query);
                return designaitonList;
            }
        }

        public Task<MasterDataModel> UpdateAsync(MasterDataModel entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}