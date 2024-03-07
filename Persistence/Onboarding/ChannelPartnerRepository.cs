using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;
using System.Text;

namespace Persistence.Onboarding
{

    public class ChannelPartnerRepository : IChhannelPartnerRepository
    {
        private readonly DapperContext _context;
        public ChannelPartnerRepository(DapperContext context)
        {
            _context = context;
        }
        public Task<ChannelPartner> AddAsync(ChannelPartner entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ChannelPartner> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChannelPartner>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string query = String.Format("Select * from onboarding.tbl_chm_channelpartners");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<ChannelPartner>();
                }
            }
        }

        public Task<ChannelPartner> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChannelPartner>> GetCpafDetailsAsync(int cpafNumber, CancellationToken cancellationToken = default)
        {
            string query = string.Format("select tcc.orgId, tcc.orgCode, tcc.orgName, tcc.status from \"Inventory\".tbl_sc_inventory_dtls tsid, onboarding.tbl_chm_channelpartners tcc where tsid.cpafnnumber = tcc.cpaf and tsid.cpafnnumber = {0}", cpafNumber);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<ChannelPartner>();
                }
            }
        }

        public async Task<CPDetailsByMobileNumber> GetCPDetilsByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SELECT CONCAT_WS(' ', NULLIF(tuu.firstname, ''), NULLIF(tuu.middlename, ''), NULLIF(tuu.lastname, '')) as RTName, tuu.loginid as LoginCode, tcc.orgid as OrgId,tuu2.userid as MappedEmployeeId, CONCAT_WS(' ', NULLIF(tuu2.firstname,''), NULLIF(tuu2.middlename,''), NULLIF(tuu2.lastname,'')) as MappedEmployeeName, tcc.status as Status ");
            stringBuilder.AppendLine("FROM user_management.tbl_um_users tuu ");
            stringBuilder.AppendLine("join onboarding.tbl_chm_channelpartners tcc  on tcc.orgid = tuu.orgid ");
            stringBuilder.AppendLine("left join user_management.tbl_um_users tuu2 on tcc.fieldstaffid = tuu2.userid ");
            stringBuilder.AppendFormat("WHERE LOWER(tuu.mobilenumber) = LOWER('{0}')", mobileNumber);

            var query = stringBuilder.ToString();

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var cpDetails = await dbConnection.QueryFirstOrDefaultAsync<CPDetailsByMobileNumber>(query);
                return cpDetails;
            }
        }

        public async Task<EmployeeDetailCPMapping> GetEmployeeDetailsByEmployeeCodeAndMobileNumber(string employeeCode, string mobileNumber, CancellationToken cancellationToken = default)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SELECT CONCAT_WS(' ', NULLIF(tuu2.firstname,''), NULLIF(tuu2.middlename,''), NULLIF(tuu2.lastname,'')) as ReportTo, tuu.userid as EmployeeId, CONCAT_WS(' ', NULLIF(tuu.firstname, ''), NULLIF(tuu.middlename, ''), NULLIF(tuu.lastname, '')) as EmployeeName, tuu.emailid as EmployeeEmail ");
            stringBuilder.AppendLine("FROM user_management.tbl_um_users tuu ");
            stringBuilder.AppendLine("LEFT join onboarding.tbl_chm_channelpartners tcc  on tcc.fieldstaffid = tuu.userid ");
            stringBuilder.AppendLine("LEFT join user_management.tbl_um_users tuu2 on tcc.orgid = tuu2.orgid ");
            stringBuilder.AppendFormat("WHERE LOWER(tuu.loginid) = LOWER('{0}') AND LOWER(tuu.mobilenumber) = LOWER('{1}') ;", employeeCode, mobileNumber);

            var query = stringBuilder.ToString();

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var cpDetails = await dbConnection.QueryFirstOrDefaultAsync<EmployeeDetailCPMapping>(query);
                return cpDetails;
            }
        }

        public async Task<int> ReMapEmployeeToCP(MapEmployeeToCP mapEmployeeToCP, CancellationToken cancellationToken)
        {
            try
            {
                string query = @$"UPDATE onboarding.tbl_chm_channelpartners  AS cp SET fieldstaffid = @fieldstaffid FROM user_management.tbl_um_users  AS u WHERE cp.orgid = @orgid AND EXISTS (SELECT 1 FROM user_management.tbl_um_users WHERE userid = {mapEmployeeToCP.EmployeeId});";
                var parameters = new { fieldstaffid = mapEmployeeToCP.EmployeeId, orgid = mapEmployeeToCP.OrgId };

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(query, parameters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetLoginIds(string loginId, CancellationToken cancellationToken = default)
        {
            string query = $"SELECT tuu.loginid FROM user_management.tbl_um_users tuu WHERE LOWER(loginid) LIKE LOWER('{loginId}%') ORDER BY tuu.loginid ASC;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var loginIds = await dbConnection.QueryAsync<string>(query);
                return loginIds.ToList<string>();
            }
        }

        public async Task<int> SubstituteEmployee(SubstituteEmployee substituteEmployeeDto, CancellationToken cancellationToken = default)
        {
            string query = @$"UPDATE onboarding.tbl_chm_channelpartners AS tcc
                                SET fieldstaffid = tuu_new.userid
                                FROM user_management.tbl_um_users AS tuu_old
                                JOIN user_management.tbl_um_users AS tuu_new ON tuu_new.loginid = @newEmployeeId
                                WHERE tcc.fieldstaffid = tuu_old.userid
                                AND tuu_old.loginid = @oldEmployeeId;";

            var parameters = new { oldEmployeeId = substituteEmployeeDto.OldEmployeeLoginId, newEmployeeId = substituteEmployeeDto.NewEmployeeLoginId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, parameters);
                return result;
            }
        }

        public async Task<int> UpdateCPDetails(UpdateCPDetails updateCPDetails, CancellationToken cancellationToken = default)
        {
            var query = "update user_management.tbl_um_users set mobilenumber = @mobilenumber , emailid = @emailaddress , modificationdate = @modificationdate  where orgid = @orgid AND EXISTS (SELECT 1 FROM onboarding.tbl_chm_channelpartners WHERE orgid = @orgid ) ;";

            var paramas = new { orgid = updateCPDetails.OrgId, mobilenumber = updateCPDetails.MobileNumber, emailaddress = updateCPDetails.EmailAddress, modificationdate = DateTime.Now };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return result;
            }
        }
        public async Task<CPDetailsByOrgCode> GetCPDetailsByOrgCode(string orgCode, CancellationToken cancellationToken = default)
        {
            var query = "SELECT tuu.orgid as OrgId, CONCAT_WS(' ', NULLIF(firstname,''), NULLIF(middlename,''), NULLIF(lastname,'')) as RTName , loginid as RTId ,tuu.mobilenumber as MobileNumber, tuu.emailid as EmailId FROM user_management.tbl_um_users tuu JOIN onboarding.tbl_chm_channelpartners tcc on tcc.orgid = tuu.orgid WHERE tcc.orgcode = @orgcode ";
            var paramas = new { orgcode = orgCode };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var cpDetails = await dbConnection.QueryFirstOrDefaultAsync<CPDetailsByOrgCode>(query, paramas);
                return cpDetails;
            }
        }
        public async Task<bool> CheckDuplicateMobileNumber(string mobileNumber, int orgId)
        {
            string query = "SELECT EXISTS(SELECT 1 FROM user_management.tbl_um_users WHERE LOWER(mobilenumber) = LOWER(@mobilenumber) AND orgid != @orgid);";
            var parameters = new { mobilenumber = mobileNumber, orgid = orgId };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                bool isDuplicate = await dbConnection.ExecuteScalarAsync<bool>(query, parameters);
                return isDuplicate;
            }
        }
        public async Task<bool> CheckDuplicateEmailId(string emailAddress, int orgId)
        {
            string query = "SELECT EXISTS(SELECT 1 FROM user_management.tbl_um_users WHERE LOWER(emailid) = LOWER(@emailaddress) AND orgid != @orgid);";
            var paramas = new { emailaddress = emailAddress, orgid = orgId };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                bool Duplicate = await dbConnection.QueryFirstOrDefaultAsync<bool>(query, paramas);
                return Duplicate;
            }
        }
        public Task<ChannelPartner> UpdateAsync(ChannelPartner entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
