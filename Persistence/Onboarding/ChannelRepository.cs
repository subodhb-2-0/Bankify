using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;
namespace Persistence.Onboarding
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly DapperContext _context;
        public ChannelRepository(DapperContext context)
        {
            _context = context;
        }
        public Task<Channel> AddAsync(Channel entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<Channel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Channel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string query = String.Format("Select * from onboarding.tbl_chm_channel");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<Channel>();
                }
            }
        }
        public async Task<IEnumerable<Channel>> GetAllByStatusAsync(int status, CancellationToken cancellationToken = default)
        {
            string query = String.Format("Select * from onboarding.tbl_chm_channel where status=@status");
            var _params = new { status = status };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query, _params))
                {
                    return multi.Read<Channel>();
                }
            }
        }
        public async Task<Channel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {

            string query = String.Format(@"Select * from onboarding.tbl_chm_channel where channelid={0}", id);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<Channel>(query);
            }
        }
        public async Task<Tuple<IEnumerable<Channel>, int>> GetByStatusAsync(int status, CancellationToken cancellationToken = default)
        {
            string query = String.Format("Select * from onboarding.tbl_chm_channel where status={0}", status);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var channels = multi.Read<Channel>().ToList();
                    return new Tuple<IEnumerable<Channel>, int>(channels.ToList<Channel>(), count);
                }
            }
        }
        public Task<Channel> UpdateAsync(Channel entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}