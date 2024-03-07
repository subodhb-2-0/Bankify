using Dapper;
using Domain.Entities.PageManagement;
using Domain.Entities.RoleManagement;
using Domain.RepositoryInterfaces;
using System.Data;
using System.Text;
using static Dapper.SqlMapper;

namespace Persistence
{
    public class RoleRepository : IRoleRepository
    {
        
        private readonly DapperContext _context;
        public RoleRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<Role> AddAsync(Role entity, CancellationToken cancellationToken = default)
        {

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                string insertRoleQuery = @"INSERT INTO user_management.tbl_um_role(roleid, rolename, roledescription, status, creator, creationdate) 
                                           VALUES(@roleid, @rolename, @roledescription, @status, @creator, @creationdate);
                                          SELECT CAST(SCOPE_IDENTITY() as int);";
                dbConnection.Open();
                var id = dbConnection.QuerySingle<int>(insertRoleQuery, entity);

                entity.RoleId = id;
                return entity;
            }
        }

        public async Task<Role> CreateAndAssignPageAccess(Role entity, List<RolePage> rolePages, CancellationToken cancellationToken = default)
        {
            
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
              

                StringBuilder commandBuilder = new StringBuilder();
                commandBuilder.AppendLine($"DO $$");
                commandBuilder.AppendLine($"DECLARE insertedRoleId integer;") ;
                commandBuilder.AppendLine($"BEGIN");
                //commandBuilder.AppendLine($"INSERT INTO user_management.tbl_um_role(rolename, roledescription, status, creator, creationdate) VALUES('{entity.RoleName.Trim()}', '{entity.RoleDescription}', 2 , {entity.Creator}, NOW()) RETURNING roleId into insertedRoleId;");
                commandBuilder.AppendLine($"INSERT INTO user_management.tbl_um_role(rolename, roledescription, status, creator, creationdate) VALUES('{entity.RoleName.Trim()}', '{entity.RoleDescription}', {entity.Status} , {entity.Creator}, NOW()) RETURNING roleId into insertedRoleId;");

                foreach (RolePage rolePage  in rolePages)
                {
                    commandBuilder.AppendLine($"INSERT INTO user_management.tbl_um_rolepage(roleid, pageid, mode, status, creator, creationdate) VALUES (insertedRoleId, {rolePage.PageId}, {rolePage.Mode}, {rolePage.PageStatus}, {entity.Creator}, NOW());");
                }

                commandBuilder.AppendLine($"END$$");
                dbConnection.Open();
               await dbConnection.ExecuteAsync(commandBuilder.ToString());

                return entity;
            }
        }

        public async Task<RolepageeditByRoleId> EditRoleAndPageAccess(RolepageeditByRoleId entity, List<RolePageEdit> rolePages, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (RolePageEdit rolePage in rolePages)
                {
                    string recordCheckQuery = string.Format($"SELECT CASE WHEN EXISTS ( SELECT 1 FROM user_management.tbl_um_rolepage tur WHERE tur.roleid = {entity.RoleId} AND tur.pageid = {rolePage.PageId} ) THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS result;");

                    string updateQuery = string.Format($"UPDATE user_management.tbl_um_rolepage SET mode = {rolePage.Mode}, status= {rolePage.PageStatus}, modificationdate=NOW() WHERE roleid = {entity.RoleId} and pageid= {rolePage.PageId};");

                    string addQuery = string.Format($"INSERT INTO user_management.tbl_um_rolepage\r\n( roleid , pageid , mode, status , creator , creationdate , modifier ,\r\nmodificationdate)\r\nVALUES ( {entity.RoleId}, {rolePage.PageId} , {rolePage.Mode}, {rolePage.PageStatus} , {entity.Creator} , now() , {entity.Modifier}, now());");

                    //var query = "UPDATE user_management.tbl_um_rolepage SET pageid=@pageid, mode = @mode, status=@status, modificationdate=NOW() WHERE roleid = @roleid";
                    //var updateQuery = "UPDATE user_management.tbl_um_rolepage SET mode = @mode, status=@status, modificationdate=NOW() WHERE roleid = @roleid and pageid=@pageid";

                    //var paramas = new { pageid = rolePage.PageId, mode = rolePage.Mode, status = rolePage.PageStatus, roleid = entity.RoleId };

                    using (IDbConnection dbConnection = _context.CreateConnection())
                    {
                        dbConnection.Open();
                        bool result = await dbConnection.QueryFirstOrDefaultAsync<bool>(recordCheckQuery);
                        var response = result ? await dbConnection.ExecuteAsync(updateQuery) : await dbConnection.ExecuteAsync(addQuery);

                       // result = result.res ? await dbConnection.ExecuteAsync(query, paramas) : ;
                        //  var result = await dbConnection.ExecuteAsync(query, paramas);
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }  
        }

        public Task<Role> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = "select * from user_management.tbl_um_role tur\r\nwhere tur.roleid != 4 and tur.roleid != 5 and tur.roleid != 6 and tur.roleid != 7 and tur.roleid != 8";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var roles = await dbConnection.QueryAsync<Role>(query);
                return roles.ToList<Role>();
            }
        }

        public async Task<Role> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = "Select * from user_management.tbl_um_role where roleId= @Id";

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var role = await dbConnection.QueryFirstAsync<Role>(query, new { id });
                    return role;
                }
        }

        public async Task<Role> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            var query = "Select * from user_management.tbl_um_role where LOWER(rolename)= LOWER(@roleName)";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var role = await dbConnection.QueryFirstOrDefaultAsync<Role>(query, new { roleName });
                return role;
            }
        }

        public Task<Role> UpdateAsync(Role entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoleAccess>> GetPageAccess(int roleId, int pageSourceId, CancellationToken cancellationToken = default)
        {
            var procedure = string.Format(@"SELECT rp.RolePageId, rp.RoleId, rp.PageId, rp.Mode, rp.Status, rp.Creator, rp.CreationDate, rp.Modifier, rp.ModificationDate, p.PageName, p.PageDesc, p.PagePath, p.CorpId, p.ParentId,p.SortOrder, null as icon, null as category  FROM user_management.tbl_um_rolepage rp INNER JOIN user_management.tbl_um_page p  ON rp.PageId = p.PageId WHERE RoleId = {0} AND PageSource = {1} AND rp.Status = 2
                UNION 
                SELECT rp.RolePageId, rp.RoleId, rp.PageId, rp.Mode, rp.Status, rp.Creator, rp.CreationDate, rp.Modifier, rp.ModificationDate, p.PageName, p.PageDesc, p.PagePath, p.CorpId, p.ParentId,p.SortOrder,null as icon, null as category
                FROM user_management.tbl_um_rolepage rp INNER JOIN user_management.tbl_um_page p ON rp.PageId = p.PageId WHERE P.ParentId IS NULL AND rp.RoleId = (CASE WHEN {1} = 1 THEN 1 ELSE 0 END) AND rp.Status = 2", roleId, pageSourceId);

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var roles = await dbConnection.QueryAsync<RoleAccess>(procedure, new { vroleid = roleId, vpagesource = pageSourceId });
                return roles;
            }
        }
        public async Task<AssignRole> AssignRoleAsync(AssignRole entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE user_management.tbl_um_users SET roleid=@roleid,modificationdate=NOW() WHERE loginid=@loginid";

            var paramas = new { roleid = entity.RoleId, loginid = entity.LoginId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<IEnumerable<RoleAccess>> GetRolePages(int roleId, CancellationToken cancellationToken = default)
        {
            var procedure = string.Format(@"
                                            SELECT  rp.RolePageId, rp.RoleId, P.PageId, rp.Mode, 
                                                    rp.Status, rp.Creator, rp.CreationDate, rp.Modifier,
                                                    rp.ModificationDate, p.PageName, p.PageDesc, p.PagePath,
                                                    p.CorpId, p.ParentId,p.SortOrder, null as icon, null as category
                                            FROM user_management.tbl_um_page p 
                                            LEFT JOIN user_management.tbl_um_rolepage rp   ON rp.PageId = p.PageId AND rp.RoleId = {0}

                                            UNION ALL

                                            SELECT  rp.RolePageId, rp.RoleId, P.PageId, rp.Mode, 
                                                    rp.Status, rp.Creator, rp.CreationDate, rp.Modifier, 
                                                    rp.ModificationDate, p.PageName, p.PageDesc, p.PagePath, 
                                                    p.CorpId, p.ParentId,p.SortOrder, null as icon, null as category  
                                            FROM user_management.tbl_um_page p 
                                            LEFT JOIN user_management.tbl_um_rolepage rp   ON rp.PageId = p.PageId AND rp.RoleId = {0}
                                            WHERE p.ParentId IS NULL
                            ", roleId);

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var roles = await dbConnection.QueryAsync<RoleAccess>(procedure);
                return roles;
            }
        }

        public async Task<IEnumerable<RoleAccess>> GetAllPagesAsync(CancellationToken cancellationToken = default)
        {
            var procedure = @"SELECT  P.PageId, p.PageName, p.PageDesc, p.PagePath, 
                                                    p.CorpId, p.ParentId,p.SortOrder, null as icon, null as category  
                                            FROM user_management.tbl_um_page p ";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var roles = await dbConnection.QueryAsync<RoleAccess>(procedure);
                return roles;
            }
        }

        public async Task<IEnumerable<ViewRolePages>> ViewRolePages(int roleid, CancellationToken cancellationToken = default)
        {
            var query = "select tur.roleid, tup.pageid ,tup.pagename ,tup.parentid , tup.pagemodeparam , tup.pagesource ,tur.rolepageid ,tur.roleid  \r\nfrom user_management.tbl_um_page tup,user_management.tbl_um_rolepage tur where tup.pageid =tur.pageid and tur.roleid = (CASE WHEN @roleid > 0 THEN @roleid ELSE tur.roleid END)";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ViewRolePages>(query, new { roleid });
                return comissionDtls.ToList<ViewRolePages>();
            }
        }
        public async Task<ActiveDeactiveRole> ActiveDeactiveRoleAsync(ActiveDeactiveRole entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE user_management.tbl_um_role SET status=@status,modificationdate=NOW() WHERE roleid=@roleid";

            var paramas = new { status = entity.status, roleid = entity.roleid };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<IEnumerable<ListofRole>> GetListofRole(CancellationToken cancellationToken = default)
        {
            var query = "SELECT roleid, rolename, roledescription, status, creator, creationdate, modifier, modificationdate FROM user_management.tbl_um_role\r\nwhere status=2;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListofRole>(query);
                return comissionDtls.ToList<ListofRole>();
            }
        }

        public async Task<IEnumerable<RoleAccess>> GetRoleByRoleId(int roleId, CancellationToken cancellationToken = default)
        {
            try
            {
                /*var procedure = string.Format(@"
                                           SELECT  rp.RolePageId, rp.RoleId, P.PageId, rp.Mode, 
                                                   rp.Status as PageStatus, rp.Creator, rp.CreationDate, rp.Modifier,
                                                   rp.ModificationDate, p.PageName, p.PageDesc, p.PagePath,
                                                   p.CorpId, p.ParentId,p.SortOrder, null as icon, null as category
                                           FROM user_management.tbl_um_page p 
                                           INNER JOIN user_management.tbl_um_rolepage rp   ON rp.PageId = p.PageId AND rp.RoleId = {0}
                           ", roleId);*/
                string query = string.Format($"SELECT  rp.RolePageId, rp.RoleId, P.PageId, rp.Mode, rp.Status as PageStatus, rp.Creator, rp.CreationDate, rp.Modifier,\r\nrp.ModificationDate, p.PageName, p.PageDesc, p.PagePath, p.CorpId, p.ParentId,p.SortOrder, null as icon, null as category\r\nFROM user_management.tbl_um_page p \r\nleft JOIN user_management.tbl_um_rolepage rp \r\nON rp.PageId = p.PageId AND rp.RoleId = {roleId};");
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var roles = await dbConnection.QueryAsync<RoleAccess>(query);
                    return roles;
                }
            }
            catch(Exception ex)
            {
                throw;
            } 
        }

        public async Task<IEnumerable<PageDetailsByRole>> GetPageDetailsByRoleId(int roleId, CancellationToken cancellationToken = default)
        {
            List<PageDetailsByRole> slist = new List<PageDetailsByRole>();
            try
            {
                var procedureName = "\"user_management\".stp_um_getpagedetailsbyroleid";
                var parameters = new DynamicParameters();
                parameters.Add("p_roleid", roleId, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<PageDetailsByRole>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<PageDetailsByRole>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<Tuple<IEnumerable<Role>, int>> GetAllPagesAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            StringBuilder stringBuilder = new();
            string query = string.Empty;
            
            try
            {
                searchBy = searchBy ?? string.Empty;
                string totalRecordQuery = string.Format($"select Count(1) from user_management.tbl_um_role tur where tur.roleid not in(4,5,6,7,8) and lower(tur.rolename) like lower('%'||'{searchBy}'||'%')");
                string rawDataQuery = string.Format($"select * from user_management.tbl_um_role tur where tur.roleid not in(4,5,6,7,8) and lower(tur.rolename) like lower('%'||'{searchBy}'||'%')");
               
                if (pageSize == null || pageSize == 0)
                {
                    stringBuilder.AppendLine(totalRecordQuery + ";");
                    stringBuilder.AppendLine(rawDataQuery);
                    query = stringBuilder.ToString();
                    using (IDbConnection dbConnection = _context.CreateConnection())
                    {
                        dbConnection.Open();
                        using (var multi = await dbConnection.QueryMultipleAsync(query))
                        {
                            var count = multi.Read<int>().Single();
                            var users = multi.Read<Role>().ToList();
                            return new Tuple<IEnumerable<Role>, int>(users.ToList<Role>(), count);
                        }
                    } 
                }
                else
                {
                    int? limit = pageSize;
                    int? offset = (pageNumber - 1) * pageSize;
                    stringBuilder.AppendLine(totalRecordQuery + " ;");
                    stringBuilder.AppendLine(rawDataQuery + $"order by tur.roleid desc limit {limit} offset {offset};");
                    query = stringBuilder.ToString();
                    using (IDbConnection dbConnection = _context.CreateConnection())
                    {
                        dbConnection.Open();
                        using (var multi = await dbConnection.QueryMultipleAsync(query))
                        {
                            var count = multi.Read<int>().Single();
                            var users = multi.Read<Role>().ToList();
                            return new Tuple<IEnumerable<Role>, int>(users.ToList<Role>(), count);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<IEnumerable<PageDetails>, int>> GetAllPageDetails(int? roleId, string? serachValue, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();

            if(roleId == 0 || roleId == null)
            {
                stringBuilder.AppendLine("SELECT count(1) from user_management.tbl_um_page ");
                // for search
                if (serachValue != null)
                {
                    stringBuilder.AppendFormat(" WHERE CAST(PageId AS TEXT) LIKE '%{0}%' OR LOWER(PageName) LIKE LOWER('%{0}%') OR LOWER(PagePath) LIKE LOWER('%{0}%') OR LOWER(PageDesc) LIKE LOWER('%{0}%') ", serachValue);
                }

                stringBuilder.AppendLine("; SELECT tup.pageid as PageId, tup.pagename as PageName, tup.pagecode as PageCode, tup.pagedesc as PageDesc, tup.pagepath as PagePath, tup.pagesource as PageSource, tup.status as Status, 'AllRole' as RoleName, tup.parentid as ParentId, tup.creationdate as creationdate from user_management.tbl_um_page tup ");

                // for search
                if(serachValue != null)
                {
                    stringBuilder.AppendFormat(" WHERE CAST(PageId AS TEXT) LIKE '%{0}%' OR LOWER(PageName) LIKE LOWER('%{0}%') OR LOWER(PagePath) LIKE LOWER('%{0}%') OR LOWER(PageDesc) LIKE LOWER('%{0}%') ", serachValue);
                }
                //orderByColumn
                stringBuilder.AppendFormat(" ORDER BY {0} ", orderByColumn ?? " creationdate ");

                //orderBy
                stringBuilder.AppendFormat(" {0} , pageid DESC", orderBy ?? " DESC ");

                //Limit and Offset
                if (pageSize != null && pageSize != 0)
                {
                    int? limit = pageSize;
                    int? offset = (pageNumber - 1) * pageSize;
                    stringBuilder.AppendFormat(" limit {0} offset {1}", limit, offset);
                }
            }
            else
            {
                stringBuilder.AppendLine("SELECT count(1) from user_management.tbl_um_page tup inner join user_management.tbl_um_rolepage tur on tup.pageid =tur.pageid inner join user_management.tbl_um_role tmr on tur.roleid =tmr.roleid ");

                stringBuilder.AppendFormat(" WHERE tup.status =2 and tur.status=2 and tur.roleid = {0} ", roleId);
                // for search
                if (serachValue != null)
                {
                    stringBuilder.AppendFormat(" AND CAST(PageId AS TEXT) LIKE '%{0}%' OR LOWER(PageName) LIKE LOWER('%{0}%') OR LOWER(PagePath) LIKE LOWER('%{0}%') OR LOWER(PageDesc) LIKE LOWER('%{0}%') ", serachValue);
                }

                stringBuilder.AppendLine("; SELECT tup.pageid as PageId, tup.pagename as PageName, tup.pagecode as PageCode, tup.pagedesc as PageDesc, tup.pagepath as PagePath, tup.pagesource as PageSource, tur.\"mode\" as PageModeId, tup.status as Status, tmr.rolename as RoleName, tup.parentid as ParentId, tup.creationdate as creationdate FROM user_management.tbl_um_page tup inner join user_management.tbl_um_rolepage tur on tup.pageid =tur.pageid inner join user_management.tbl_um_role tmr on tur.roleid =tmr.roleid ");
                
                stringBuilder.AppendFormat(" WHERE tup.status =2 and tur.status=2 and tur.roleid = {0} ", roleId);

                // for search
                if (serachValue != null)
                {
                    stringBuilder.AppendFormat(" AND CAST(PageId AS TEXT) LIKE '%{0}%' OR LOWER(PageName) LIKE LOWER('%{0}%') OR LOWER(PagePath) LIKE LOWER('%{0}%') OR LOWER(PageDesc) LIKE LOWER('%{0}%') ", serachValue);
                }
                //orderByColumn
                stringBuilder.AppendFormat(" ORDER BY {0} ", orderByColumn ?? " creationdate ");

                //orderBy
                stringBuilder.AppendFormat(" {0} , pageid DESC ", orderBy ?? " DESC ");

                //Limit and Offset
                if (pageSize != null && pageSize != 0)
                {
                    int? limit = pageSize;
                    int? offset = (pageNumber - 1) * pageSize;
                    stringBuilder.AppendFormat(" limit {0} offset {1}", limit, offset);
                }
            }
            var query = stringBuilder.ToString();

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();

                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var pageDetails = multi.Read<PageDetails>().ToList();

                    return new Tuple<IEnumerable<PageDetails>, int>(pageDetails.ToList<PageDetails>(), count);
                }
            }
        }

        public async Task<bool> CheckDuplicatePage(AddPageDetail entity)
        {
            string query = "SELECT EXISTS(SELECT 1 FROM user_management.tbl_um_page " +
                "WHERE LOWER(pagename) = LOWER(@pagename) OR LOWER(pagepath) = LOWER(@pagepath));";
            var paramas = new { pagename = entity.PageName, pagepath = entity.PagePath };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                bool Duplicate = await dbConnection.QueryFirstOrDefaultAsync<bool>(query, paramas);
                return Duplicate;
            }
        }

        public async Task<AddPageDetail> CreatePageDetails(AddPageDetail entity, CancellationToken cancellationToken)
        {
            
            if (entity.ParentId == 0)
            {
                var query = @"INSERT INTO user_management.tbl_um_page ( pagename, pagepath, pagecode,creator, creationdate, pagedesc, status, corpid, pagesource, pagemodeparam, sortorder) VALUES 
                                                                                 ( @pagename, @pagepath, @pagecode, @creator, @creationdate, 'Parent', 2, 1, 1, 2, 1)";
                var paramas = new { pagename = entity.PageName, pagepath = entity.PagePath, pagecode = entity.PageCode , creator = entity.Creator, creationdate = DateTime.Now };

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(query, paramas);
                    return entity;
                }
            }
            else
            {
                var query = @"INSERT INTO user_management.tbl_um_page(pagename, pagepath, pagecode, creator, creationdate, parentid, pagedesc, status, corpid, pagesource, pagemodeparam, sortorder) VALUES
                                                                                 (@pagename, @pagepath, @pagecode, @creator, @creationdate, @parentid, 'Child', 2, 1, 1, 2, 1)";
                var paramas = new { pagename = entity.PageName, pagepath = entity.PagePath,pagecode = entity.PageCode, creator = entity.Creator, creationdate = DateTime.Now, parentid = entity.ParentId };
                
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(query, paramas);
                    return entity;
                }
            }
        }

        public async Task<IEnumerable<ParentPageDetails>> GetAllParentPage(CancellationToken cancellationToken)
        {
            string query = "SELECT tup.pagecode  as ParentPageCode, tup.pagename as ParentPageName FROM user_management.tbl_um_page tup WHERE LOWER(pagedesc) = LOWER('parent') AND tup.pagecode IS NOT NULL ;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var parentPages = await dbConnection.QueryAsync<ParentPageDetails>(query);
                return parentPages.ToList<ParentPageDetails>();
            }
        }
    }
}