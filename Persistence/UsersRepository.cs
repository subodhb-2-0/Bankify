using Contracts.UserManager;
using Dapper;
using Domain.Entities.UserManagement;
using Domain.RepositoryInterfaces;
using System.Data;
using System.Text;

namespace Persistence
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;
        public UsersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Users> AddAsync(Users entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO user_management.tbl_um_users (
	firstname, middlename, lastname, loginid, password, orgid, emailid, mobilenumber, departmentid, designationid, reportsto,  needspasschange, roleid, status, creator, creationdate, modificationdate, lastpwdchangedate)
	VALUES (  @firstname, @middlename, @lastname, @loginid, @password, 1, @emailid, @mobilenumber, @departmentid, @designationid, @reportsto,  @needspasschange, @roleid, @status, @creator, @creationdate, @modificationdate, @lastpwdchangedate)";

            var paramas = new { firstname = entity.FirstName, middlename = entity.MiddleName, lastname = entity.LastName, loginid = entity.LoginId, password = entity.Password, orgid = entity.OrgId, emailid = entity.EmailId, mobilenumber = entity.MobileNumber, departmentid = entity.DepartmentId, designationid = entity.DesignationId, reportsto = entity.ReportsTo, needspasschange = 1, roleid = entity.RoleId, status = 2, creator = entity.CreatedBy, creationdate = DateTime.Now, modificationdate = DateTime.Now, lastpwdchangedate = DateTime.Now };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }

        public Task<Users> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ActivateOrDeActivateUser(int userId, int status, CancellationToken cancellationToken = default)
        {
            var query = "update user_management.tbl_um_users set status = @status,failedcount = 0 where userid = @userId";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, new { userId, status });
                return result;
            }
        }

        public async Task<IEnumerable<Users>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            var query = "Select * from user_management.tbl_um_users where loginid != 'SUPERADMIN' or userid !=1;";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var users = await dbConnection.QueryAsync<Users>(query);
                return users.ToList<Users>();
            }
        }

        public async Task<Tuple<IEnumerable<Users>, int>> GetAllUsersAsync(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default)
        {
            int limit = pageSize;
            int offset = (pageNumber - 1) * pageSize;
            
            var query = string.Format("Select Count(1) from user_management.tbl_um_users TU JOIN user_management.tbl_um_role TR on TU.roleid = TR.roleid JOIN onboarding.tbl_chm_channelpartners TC ON TC.OrgId = TU.orgid;" +
                " Select TU.*, TC.orgcode, TR.rolename from user_management.tbl_um_users TU JOIN user_management.tbl_um_role TR on TU.roleid = TR.roleid JOIN onboarding.tbl_chm_channelpartners TC ON TC.OrgId = TU.orgid where TU.usertypeid is null order by TU.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();

                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<Users>().ToList();

                    return new Tuple<IEnumerable<Users>, int>(users.ToList<Users>(), count);
                }
            }
        }

        public async Task<Users> GetAuthenticateUserAsync(string loginId, string loginPassword, CancellationToken cancellationToken = default)
        {
            var query = "Select * from user_management.tbl_um_users where LOWER(loginId)= LOWER(@loginId) and password = @loginPassword";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstAsync<Users>(query, new { loginId, loginPassword });
                return user;
            }
        }
        public async Task<UsersData> GetAuthenticateUsersAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default)
        {
            var procedureName = "user_management.get_authenticate";
            var parameters = new DynamicParameters();

            parameters.Add("p_loginid", loginId, DbType.String, ParameterDirection.Input);
            parameters.Add("p_password", loginPassword, DbType.String, ParameterDirection.Input);
            parameters.Add("p_preactivestatus", 1, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_changepassdatediff", 60, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_activestatus", 2, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_ipaddress", ipAddress, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = connection.QueryFirstOrDefaultAsync<UsersData>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await user;
            }
        }
        public async Task<Users> GetUserByLoginId(string loginId, CancellationToken cancellationToken = default)
        {
            var query = "Select * from user_management.tbl_um_users where LOWER(loginId)= LOWER(@loginId)";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstOrDefaultAsync<Users>(query, new { loginId });
                return user;
            }
        }
        public async Task<UserDtlsWithRole> GetUserByUserId(long userid, CancellationToken cancellationToken = default)
        {
            var query = "select tuu.userid as UserId,tuu.orgid as OrgId,tuu.loginid as LoginId,\r\ntuu.firstname as FirstName,tuu.middlename as MiddleName,tuu.lastname as LastName,tuu.emailid as EmailId,\r\ntuu.mobilenumber as MobileNumber,tuu.status as Status,tuu.departmentid as DepartmentId,\r\ntuu.designationid as DesignationId,tuu.reportsto as ReportsTo,tuu.roleid as RoleId,tur.rolename as RoleName,\r\ntmp.param_value1 as Dept,tmp2.param_value1 as Desig\r\nfrom user_management.tbl_um_users tuu \r\nleft join user_management.tbl_um_role tur on tur.roleid = tuu.roleid \r\nleft join common.tbl_mst_parameter tmp on tmp.param_id  = tuu.departmentid  and tmp.param_group = 'Dept'\r\nleft join common.tbl_mst_parameter tmp2 on tmp2.param_id = tuu.designationid and tmp2.param_group = 'Desig'\r\nwhere tuu.userid = @userid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstOrDefaultAsync<UserDtlsWithRole>(query, new { userid });
                return user;
            }
        }
        public async Task<Users> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = "Select * from user_management.tbl_um_users where userid= @Id";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstOrDefaultAsync<Users>(query, new { id });
                return user;
            }
        }

        public async Task<Users> UpdateAsync(Users entity, CancellationToken cancellationToken = default)
        {
            var query = @"  UPDATE user_management.tbl_um_users 
                            SET firstname=@firstname, middlename=@middlename, 
                                lastname=@lastname, orgid=@orgid, 
                                emailid=@emailid, departmentid=@departmentid, 
                                designationid=@designationid, reportsto=@reportsto, 
                                roleid=@roleid, modifier=@modifier, 
                                modificationdate=NOW() 
                            WHERE LOWER(loginid) = LOWER(@loginid);";

            var paramas = new { firstname = entity.FirstName, middlename = entity.MiddleName, lastname =entity.LastName, orgid = entity.OrgId, emailid = entity.EmailId, departmentid = entity.DepartmentId, designationid = entity.DesignationId, reportsto = entity.ReportsTo, roleid = entity.RoleId, modifier = entity.UpdatedBy,  loginid = entity.LoginId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<UserEdit> UpdateUserAsync(UserEdit entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE user_management.tbl_um_users SET firstname=@firstname, middlename=@middlename,lastname=@lastname,departmentid = @departmentid, designationid = @designationid,reportsto=@reportsto,roleid=@roleid,modificationdate=NOW() WHERE userid = @userid";

            var paramas = new { firstname = entity.FirstName, middlename = entity.MiddleName, lastname = entity.LastName, departmentid = entity.DepartmentId, designationid = entity.DesignationId, reportsto = entity.ReportsTo, roleid = entity.RoleId, userid = entity.UserId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<Users> GetUserByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default)
        {
            var query = "Select * from user_management.tbl_um_users where mobileNumber= @mobileNumber";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstOrDefaultAsync<Users>(query, new { mobileNumber });
                return user;
            }
        }
        public async Task<IEnumerable<UserByMobileAndType>> GetUserByMobileAndUsertype(string mobileNumber, int usertypeid, CancellationToken cancellationToken = default)
        {
            var query = "select tuu.userid, tuu.orgid, tuu.loginid, tuu.firstname, tuu.middlename, tuu.lastname from user_management.tbl_um_users tuu";

            switch (usertypeid)
            {
                case 2:
                    query = "select tuu.userid, tuu.orgid, tuu.loginid, tuu.firstname, tuu.middlename, tuu.lastname from \r\nuser_management.tbl_um_users tuu\r\ninner join onboarding.tbl_chm_channelpartners tcc on tcc.orgid = tuu.orgid \r\nwhere\r\ntuu.mobilenumber = @mobileNumber and tuu.usertypeid not in(2) and tuu.usertypeid in (4) and tcc.status =2";
                    break;
                case 4:
                    query = "select tuu.userid, tuu.orgid, tuu.loginid, tuu.firstname, tuu.middlename, tuu.lastname from \r\nuser_management.tbl_um_users tuu\r\ninner join onboarding.tbl_chm_channelpartners tcc on tcc.orgid = tuu.orgid \r\nwhere\r\ntuu.mobilenumber = @mobileNumber and tuu.usertypeid not in(4) and tuu.usertypeid in (6) and tcc.status =2";
                    break;
                case 6:
                    query = "select tuu.userid, tuu.orgid, tuu.loginid, tuu.firstname, tuu.middlename, tuu.lastname from \r\nuser_management.tbl_um_users tuu\r\ninner join onboarding.tbl_chm_channelpartners tcc on tcc.orgid = tuu.orgid \r\nwhere\r\ntuu.mobilenumber = @mobileNumber and tuu.usertypeid not in(6) and tuu.usertypeid in (1) and tcc.status =2";
                    break;
                default:
                    break; // No need to change the query for other cases
            }
            //var query = "select userid,orgid,loginid ,firstname ,middlename ,lastname from user_management.tbl_um_users";
            //if (usertypeid == 2)
            //{
            //    query = "select userid,orgid,loginid ,firstname ,middlename ,lastname from user_management.tbl_um_users where mobilenumber = @mobileNumber and usertypeid not in(2) and usertypeid in (4)";
            //}
            //else if (usertypeid == 4)
            //{
            //    query = "select userid,orgid,loginid ,firstname ,middlename ,lastname from user_management.tbl_um_users where mobilenumber = @mobileNumber and usertypeid not in(4) and usertypeid in (6)";
            //}
            //else if (usertypeid == 6)
            //{
            //    query = "select userid,orgid,loginid ,firstname ,middlename ,lastname from user_management.tbl_um_users where mobilenumber = @mobileNumber and usertypeid not in(6) and usertypeid in (1)";
            //}
            //else
            //{
            //    query = "select userid,orgid,loginid ,firstname ,middlename ,lastname from user_management.tbl_um_users ";
            //}
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var userDtls = await dbConnection.QueryAsync<UserByMobileAndType>(query, new { mobileNumber });
                return userDtls.ToList<UserByMobileAndType>();
            }
        }
        public async Task<IEnumerable<UserDetailsQuery>> GetAllUserAsync( string? loginid, string? emailid, string? mobilenumber, CancellationToken cancellationToken = default)
        {

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select tuu.userid,tuu.firstname,tuu.middlename,\r\ntuu.lastname,tuu.loginid,tuu.orgid,\r\ntuu.emailid,tuu.mobilenumber,\r\ntuu.status,tuu.creator,tuu.creationdate,tuu.modificationdate \r\nfrom user_management.tbl_um_users tuu");
            queryBuilder.Append(" where ");

            if (loginid != null)
                queryBuilder.Append($" tuu.loginid = '{loginid}' ");
  
            if (emailid != null)
                queryBuilder.Append($" or tuu.emailid = '{emailid}' ");
          
            if (mobilenumber != null)
                queryBuilder.Append($" or tuu.mobilenumber = '{mobilenumber}' ");
           

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var UserDetails = await dbConnection.QueryAsync<UserDetailsQuery>(queryBuilder.ToString());
                return (IEnumerable<UserDetailsQuery>)UserDetails;
            }
        }
        public async Task<IEnumerable<UserDetailsQuery>> GetUserBySearchFiltter(string? loginid, string? emailid, string? mobilenumber, string? firstname, int? roleid, int? status, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select Count(1) over() as totalrecord, tuu.userid,tuu.firstname,tuu.middlename,\r\ntuu.lastname,tuu.loginid,tuu.orgid,\r\ntuu.emailid,tuu.mobilenumber,\r\ntuu.status,tuu.creator,tuu.creationdate,tuu.modificationdate \r\nfrom user_management.tbl_um_users tuu");
            queryBuilder.Append(" where 1=1 ");

            if (loginid != null)
            {
                queryBuilder.Append($" AND tuu.loginid = '{loginid}' ");
            }

            if (emailid != null)
            {
                queryBuilder.Append($" or tuu.emailid = '{emailid}' ");
            }

            if (mobilenumber != null)
            {
                queryBuilder.Append($" or tuu.mobilenumber = '{mobilenumber}' ");
            }
            if (firstname != null)
            {
                queryBuilder.Append($" or tuu.firstname = '{firstname}' ");
            }
            if (roleid > 0)
            {
                queryBuilder.Append($" or tuu.roleid = {roleid} ");
            }
            if (status > 0)
            {
                queryBuilder.Append($" or tuu.status = {status} ");
            }
            if (p_fetchrows > 0 && p_offsetrows != null)
            {
                queryBuilder.Append($" ORDER BY  tuu.userid asc OFFSET {p_offsetrows}  LIMIT {p_fetchrows} ");
            }
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var UserDetails = await dbConnection.QueryAsync<UserDetailsQuery>(queryBuilder.ToString());
                return (IEnumerable<UserDetailsQuery>)UserDetails;
            }
        }
        public async Task<IEnumerable<UserDetailsQuery>> GetDynamicSearchUser(DynamicSearchUserModel entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "accounts.stp_acc_getuserdetails";
            var parameters = new DynamicParameters();
            parameters.Add("p_loginid", entity.p_loginid, DbType.String, ParameterDirection.Input);
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
           
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<UserDetailsQuery>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<int> UpdatePassword(string loginId, string password, CancellationToken cancellationToken = default)
        {
            string updatePasswordQuery = "UPDATE user_management.tbl_um_users SET lastpassword=password,password=@password, modifier=userid, modificationdate=NOW(), lastpwdchangedate=NOW() WHERE LOWER(loginid) = LOWER(@loginId);";


            

            var paramas = new { loginid = loginId, password = password};

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(updatePasswordQuery, paramas);
                return result;
            }
        }
        public async Task<IEnumerable<ActiveUserDetailsByRoleId>> GetAllActiveUser(int roleid, CancellationToken cancellationToken = default)
        {
            var query = "select firstname,lastname,mobilenumber,lastlogindate,userid from user_management.tbl_um_users\r\n\r\nwhere roleId= @roleid and status =2";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var UserDtls = await dbConnection.QueryAsync<ActiveUserDetailsByRoleId>(query, new { roleid });
                return UserDtls.ToList<ActiveUserDetailsByRoleId>();
            }
        }
        public async Task<ResetMobileAndEmailModel> UpdateUserMobileAndEmailAsync(ResetMobileAndEmailModel entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE user_management.tbl_um_users SET mobilenumber=@mobilenumber, emailid=@emailid,modificationdate=NOW() WHERE loginid = @loginid";

            var paramas = new { mobilenumber = entity.mobilenumber, emailid = entity.emailid, loginid = entity.loginid };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<Users> GetAuthenticateUserAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default)
        {
            var procedureName = "user_management.get_authenticate";
            var parameters = new DynamicParameters();

            parameters.Add("p_loginid", loginId, DbType.String, ParameterDirection.Input);
            parameters.Add("p_password", loginPassword, DbType.String, ParameterDirection.Input);
            parameters.Add("p_preactivestatus", 1, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_changepassdatediff", 60, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_activestatus", 2, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_ipaddress", ipAddress, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var user = connection.QueryFirstOrDefaultAsync<Users>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await user;
            }
        }

        public async Task<UserOnBoardingModel> GetOnBoardingStatusByOrgId(long orgId, CancellationToken cancellationToken = default)
        {
            var query = "select status from onboarding.tbl_chm_channelpartners where orgId= @orgId";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstOrDefaultAsync<UserOnBoardingModel>(query, new { orgId });
                return user;
            }
        }
        public async Task<GetTokenDataByUserid> GetTokenDataByUserid(long userid, string ipaddress, CancellationToken cancellationToken = default)
        {
            var query = "select tutb .userid ,tutb.gettoken , tutb .blacklisteddate  from user_management.tbl_user_token_blacklist tutb \r\nwhere tutb .userid = @userid and tutb .ipaddress = @ipaddress";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var user = await dbConnection.QueryFirstOrDefaultAsync<GetTokenDataByUserid>(query, new { userid, ipaddress });
                return user;
            }
        }

        public async Task<UpdateUsersToken> UpdateUserToken(UpdateUsersToken entity, CancellationToken cancellationToken = default)
        {
            var query = "update user_management.tbl_user_token_blacklist set isvalid = false where userid = @userid and ipaddress = @ipaddress";

            var paramas = new { ipaddress = entity.ipaddress, userid = entity.userid };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<OpperetionStausModel> InsertToken(InsertTokenDataModel entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "user_management.stp_kyc_inserttoken";
            var parameters = new DynamicParameters();

            parameters.Add("p_userid", entity.p_userid, DbType.Int64, ParameterDirection.Input);
            parameters.Add("p_gettoken", entity.p_gettoken, DbType.String, ParameterDirection.Input);
            parameters.Add("p_ipaddress", entity.p_ipaddress, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var response = await connection.QueryFirstOrDefaultAsync<OpperetionStausModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<int> DeactivePageStaus(string? pageCode, CancellationToken cancellationToken = default)
        {
            try
            {
                string query = @$"UPDATE user_management.tbl_um_page tup SET status = 
                                    CASE status
                                    WHEN 2 THEN 3
                                    WHEN 3 THEN 2
                                    END
                                   WHERE tup.pagecode = '{pageCode}'";
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var response = await connection.ExecuteAsync(query);
                    return response;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<int> EditPageDetail(EditPageDetails editPageDetails, string? pageCode, CancellationToken cancellationToken = default)
        {
            try
            {
                string query = string.Format($"UPDATE user_management.tbl_um_page tup \r\nSET pagename = '{editPageDetails.pageName}', pagepath = '{editPageDetails.pagePath}'\r\nWHERE pagecode= '{pageCode}';");

                var parameters = new {pagename= editPageDetails.pageName, pagepath = editPageDetails.pagePath};
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var response = await connection.ExecuteAsync(query, parameters);
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}