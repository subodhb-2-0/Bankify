using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;

namespace Persistence.Onboarding
{
    public class CPDetailRepository : ICPDetailRepository
    {
        private readonly DapperContext _context;
        public CPDetailRepository(DapperContext context)
        {
            _context = context;
        }
        public Task<OrgCpDetails> AddAsync(OrgCpDetails entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OrgCpDetails> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrgCpDetails>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<OrgCpDetails> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            string query = "Select * from onboarding.tbl_org_cpdetails where cpdetailid=@id";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                return await dbConnection.QueryFirstAsync<OrgCpDetails>(query, new { id = id});
            }
        }

        public Task<OrgCpDetails> UpdateAsync(OrgCpDetails entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
