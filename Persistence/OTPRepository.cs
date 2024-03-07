using Dapper;
using Domain.Entities.OTPManager;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class OTPRepository : IOTPRepository
    {
        private readonly DapperContext _context;
        public OTPRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<OTPDetails> AddAsync(OTPDetails entity, CancellationToken cancellationToken = default)
        {
            string insertOTPQuery = @"INSERT INTO common.tbl_check_otp(
	 loginid, otp, moduleid, expiredtimeinsecond, creator, creationdate, imeino, ipaddress)
	VALUES ( @loginid, @otp, @moduleid, @expiredtimeinsecond, @creator, NOW(), @imeino, @ipaddress)";

            var paramas = new { loginid = entity.LoginId, otp = entity.OTP, moduleid = entity.ModuleId, expiredtimeinsecond = entity.ExpiredTimeInSecond , creator = entity.Creator, imeino = entity.ImeiNo, ipaddress = entity .IPAddress};

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(insertOTPQuery, paramas);
                return entity;
            }
        }

        public Task<OTPDetails> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OTPDetails>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OTPDetails> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OTPDetails> UpdateAsync(OTPDetails entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<OTPDetails> Get(string loginId, int moduleId, CancellationToken cancellationToken = default)
        {
            string getOTPQuery = @"SELECT * FROM common.tbl_check_otp where LOWER(loginid) = LOWER(@loginid) and moduleid = @moduleid and (creationdate + expiredtimeinsecond * interval '1 second') > NOW() order by creationdate desc";

            var paramas = new { loginId , moduleId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<OTPDetails>(getOTPQuery, paramas);
                return result;
            }
        }

    }
}
