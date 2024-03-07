using Dapper;
using Domain.Entities.Comission;
using Domain.RepositoryInterfaces;
using System.Data;
using System.Text;

namespace Persistence
{
    public class ComissionRepository : IComissionRepository
    {
        private readonly DapperContext _context;
        public ComissionRepository(DapperContext context)
        {
            _context = context;
        }

        public Task<Comission> AddAsync(Comission entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Comission> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comission>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Comission> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Comission>> GetAllComissionType(CancellationToken cancellationToken = default)
        {
            var query = "select param_group,param_value1,param_key,param_id  from common.tbl_mst_parameter";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comission = await dbConnection.QueryAsync<Comission>(query);
                return comission.ToList<Comission>();
            }
        }

        public async Task<IEnumerable<Comission>> GetComissionType(string commType, CancellationToken cancellationToken = default)
        {
            var query = "select param_group,param_value1,param_value2,param_key,param_id  from common.tbl_mst_parameter where LOWER(param_group)=LOWER(@commType)";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comission = await dbConnection.QueryAsync<Comission>(query, new { commType });
                return comission.ToList<Comission>();
            }
        }
        public async Task<IEnumerable<GetbaseParamById>> GetbaseParamByCommType(string Value2, CancellationToken cancellationToken = default)
        {
            var query = "SELECT tmp.param_id as Id,tmp.param_key as KeyParam,tmp.param_value1 as Value1,tmp.param_value2 as Value2 FROM common.tbl_mst_parameter tmp\r\nWHERE tmp.param_group='commBaseParam' and tmp.param_value2 =@Value2";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comission = await dbConnection.QueryAsync<GetbaseParamById>(query, new { Value2 });
                return comission.ToList<GetbaseParamById>();
            }
        }
        public async Task<IEnumerable<CommReceivablesDtls>> GetCommReceivablesDtls(int commReceivableId, int Userid, CancellationToken cancellationToken = default)
        {
            var query = "select tcc.crdid as commReceivableDetailsId,tcc.crid as commReceivableId,tcc.\"minvalue\" as minimumValue,tcc.\"maxvalue\" as maximumValue,tcc.commtype as commType,\r\ntcc.baseparam as baseParam,tcc.bankifyshare as sevenPayShare,tcc.status as statusId,tcc.creationdate as createdDate,tuu.firstname as createdBy \r\nfrom commission.tbl_chm_commissionreceivabledetails tcc\r\nleft join user_management.tbl_um_users tuu on tuu.userid = tcc.creator \r\nwhere tcc.crid = @commReceivableId and tcc.creator = @Userid ORDER BY createdDate DESC";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<CommReceivablesDtls>(query, new { commReceivableId, Userid });
                return comissionDtls.ToList<CommReceivablesDtls>();
            }
        }
        public async Task<IEnumerable<CompairCommReceivablesDtls>> GetCompairCommReceivablesDtlsValue(int crid, int minimumValue, int maximumValue, CancellationToken cancellationToken = default)
        {
            var query = "select tcc.crid from commission.tbl_chm_commissionreceivabledetails tcc where tcc.\"minvalue\" <=@minimumValue and tcc.\"maxvalue\" >=@maximumValue and tcc.crid = @crid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<CompairCommReceivablesDtls>(query, new { crid, minimumValue, maximumValue });
                return comissionDtls.ToList<CompairCommReceivablesDtls>();
            }
        }
        public async Task<CommReceivablesDtls> CompairCommReceivablesDtls(int commReceivableId, int crdid, CancellationToken cancellationToken = default)
        {
            var query = "select tcc.crdid as commReceivableDetailsId,tcc.crid as commReceivableId,tcc.\"minvalue\" as minimumValue,tcc.\"maxvalue\" as maximumValue,tcc.commtype as commType,\r\ntcc.baseparam as baseParam,tcc.bankifyshare as sevenPayShare,tcc.status as statusId,tcc.creationdate as createdDate,tuu.firstname as createdBy \r\nfrom commission.tbl_chm_commissionreceivabledetails tcc\r\nleft join user_management.tbl_um_users tuu on tuu.userid = tcc.creator \r\nwhere tcc.crid = @commReceivableId and tcc.crdid = @crdid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<CommReceivablesDtls>(query, new { commReceivableId, crdid });
                return comissionDtls;
            }
        }
        public async Task<IEnumerable<GetCommSharingModelDtls>> GetCommSharingModelDtls(int commSharingId, int Userid, CancellationToken cancellationToken = default)
        {
            var query = "select csm.csmid as commSharingId,csd.csdid as commSharingDtlsId,csd.\"minvalue\" as minimumValue,\r\ncsd.\"maxvalue\" as maximumValue,csd.baseparam as baseParam,csd.commtype as commType,csd.status as status,\r\ncsd.retailershare as retailerShare,csd.distshare as DistShare,csd.bankifyshare as sevenPayShare,csd.creationdate as CreatedDate,csd.superdistshare as SuperDistShare,\r\ntuu.firstname as userName,csd.creator as UserId\r\nfrom commission.tbl_chm_commissionsharingdetails csd\r\ninner join commission.tbl_chm_commissionsharingmodel csm on csd.csmid = csm.csmid \r\nleft join user_management.tbl_um_users tuu on tuu.userid = csd.creator \r\nwhere csd.csmid = @commSharingId and csd.creator = @UserId ORDER BY createdDate DESC";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<GetCommSharingModelDtls>(query, new { commSharingId, Userid });
                return comissionDtls.ToList<GetCommSharingModelDtls>();
            }
        }
        public async Task<CreateCommReceivablesDtls> GetCommReceivablesDtlsByminAndMaxValue(int minimumValue, int maximumValue, int UserId, int commReceivableId, CancellationToken cancellationToken = default)
        {
            var query = "select * from commission.tbl_chm_commissionreceivabledetails tcc \r\nwhere tcc.\"minvalue\" <=@minimumValue and tcc.\"maxvalue\" >=@maximumValue and tcc.creator = @UserId and tcc.crid = @commReceivableId and tcc.status =2";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<CreateCommReceivablesDtls>(query, new { minimumValue, maximumValue, UserId, commReceivableId });
                return comissionDtls;
            }
        }
        public async Task<CreateCommSharingModelDtls> GetCommSharingDtlsByminAndMaxValue(int minimumValue, int maximumValue, int UserId, int commSharingId, CancellationToken cancellationToken = default)
        {
            var query = "select * from commission.tbl_chm_commissionsharingdetails tcc \r\nwhere tcc.\"minvalue\" <=@minimumValue and tcc.\"maxvalue\" >=@maximumValue and tcc.creator = @UserId and tcc.csmid=@commSharingId and tcc.status =2";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<CreateCommSharingModelDtls>(query, new { minimumValue, maximumValue, UserId, commSharingId });
                return comissionDtls;
            }
        }
        public async Task<ComissionReciveIdReturn> GetRecentIdOfComissionRecive(string crname, CancellationToken cancellationToken = default)
        {
            var query = "select  cr.crid as EntityId  from commission.tbl_chm_commissionreceivable cr \r\nwhere cr.crname = @crname\r\norder by cr.creationdate asc";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<ComissionReciveIdReturn>(query, new { crname });
                return comissionDtls;
            }
        }
        public async Task<ComissionReciveIdReturn> GetRecentIdOfComissionSharing(string csmname, CancellationToken cancellationToken = default)
        {
            var query = "select tcc.csmid as EntityId  from commission.tbl_chm_commissionsharingmodel tcc \r\nwhere tcc.csmname = @csmname order by tcc.creationdate asc";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<ComissionReciveIdReturn>(query, new { csmname });
                return comissionDtls;
            }
        }
        public async Task<ComissionReciveableStatus> GetCommReceivablesStatusAsync(int CRID, CancellationToken cancellationToken = default)
        {
            var query = "select tsssm.crid as CRID,tcc.crstatus as Status from commission.tbl_chm_commissionreceivable tcc \r\ninner join servicemanagement.tbl_sm_supp_sp_map tsssm on tcc.crid = tsssm.crid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tms.supplierid = tsssm.supplierid \r\nwhere tcc.crid = @CRID";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<ComissionReciveableStatus>(query, new { CRID });
                return comissionDtls;
            }
        }
        public async Task<ComissionReciveableStatus> CheckCommisionShairingAsync(int CommisionSharingId, CancellationToken cancellationToken = default)
        {
            var query = "select tcc2.csmid as CRID, tcc2.csmstatus as Status  from commission.tbl_chm_commissionsharingdetails tcc \r\ninner join commission.tbl_chm_commissionsharingmodel tcc2 on tcc2.csmid = tcc.csmid \r\nwhere tcc2.csmid = @CommisionSharingId";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<ComissionReciveableStatus>(query, new { CommisionSharingId });
                return comissionDtls;
            }
        }
        
        public async Task<Tuple<IEnumerable<ComissionSharing>, int>> GetComissionSharingModel(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            try
            {
                pageNumber = pageNumber ?? 1;
                string searchFilterQuery = "where tcc.csmid::TEXT LIKE '%{searchValue}%' or lower(tcc.csmname) like lower('%'||'{searchValue}'||'%') or tcc.csmstatus ::TEXT LIKE '%{searchValue}%'";
               
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Select count(*) from commission.tbl_chm_commissionsharingmodel; ");
                stringBuilder.AppendLine("select * from commission.tbl_chm_commissionsharingmodel tcc ");

                if(!string.IsNullOrEmpty(searchBy))
                {
                    searchFilterQuery = searchFilterQuery.Replace("{searchValue}", searchBy);
                    stringBuilder.AppendLine(searchFilterQuery);
                }
                    
                stringBuilder.AppendFormat(" order by {0} ", string.IsNullOrEmpty(orderByColumn) ? " creationdate " : orderByColumn);
                stringBuilder.AppendFormat(" {0} ", string.IsNullOrEmpty(orderBy) ? " asc " : orderBy);

                if (pageSize != null && pageSize != 0)
                {
                    int? limit = pageSize;
                    int? offset = (pageNumber - 1) * pageSize;
                    stringBuilder.AppendFormat("limit {0} offset {1}", limit, offset);
                }

                var query = stringBuilder.ToString();
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    using (var multi = await dbConnection.QueryMultipleAsync(query))
                    {
                        var count = multi.Read<int>().Single();
                        var servicecat = multi.Read<ComissionSharing>().ToList();
                        return new Tuple<IEnumerable<ComissionSharing>, int>(servicecat.ToList<ComissionSharing>(), count);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }  
        }

        public async Task<Tuple<IEnumerable<ComissionReceivable>, int>> GetComissionReceivableModel(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            try
            {
                string searchFilterQuery = "WHERE  (tcc .crid::TEXT LIKE '%{searchValue}%' OR\r\n       LOWER(tcc.crname) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       tcc .crstatus::TEXT LIKE '%{searchValue}%')";
                pageNumber = pageNumber ?? 1;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Select count(*) from commission.tbl_chm_commissionreceivable; ");
                stringBuilder.AppendLine("SELECT * FROM commission.tbl_chm_commissionreceivable tcc");

                if (!string.IsNullOrEmpty(searchBy))
                {
                    searchFilterQuery = searchFilterQuery.Replace("{searchValue}", searchBy);
                    stringBuilder.AppendLine(searchFilterQuery);
                }

                stringBuilder.AppendFormat("ORDER BY {0} ", string.IsNullOrEmpty(orderByColumn) ? " crid " : orderByColumn);
                stringBuilder.AppendFormat(" {0} ", string.IsNullOrEmpty(orderBy) ? " ASC " : orderBy);

                if (pageSize != null && pageSize != 0)
                {
                    int? limit = pageSize;
                    int? offset = (pageNumber - 1) * pageSize;
                    stringBuilder.AppendFormat("limit {0} offset {1}", limit, offset);
                }
                var query = stringBuilder.ToString();
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    using (var multi = await dbConnection.QueryMultipleAsync(query))
                    {
                        var count = multi.Read<int>().Single();
                        var servicecat = multi.Read<ComissionReceivable>().ToList();
                        return new Tuple<IEnumerable<ComissionReceivable>, int>(servicecat.ToList<ComissionReceivable>(), count);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<ComissionReciveableStatus> UpdateCommReceivablesStatusAsync(ComissionReciveableStatus entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE commission.tbl_chm_commissionreceivable SET crstatus=@crstatus , modificationdate = NOW() WHERE crid = @crid";

            var paramas = new { crstatus = entity.Status, crid = entity.CRID };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<UpdateCommReceivablesDtls> UpdateCommReceivablesDtlsAsync(UpdateCommReceivablesDtls entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE commission.tbl_chm_commissionreceivabledetails SET crid=@crid,status=@status,modificationdate=NOW() WHERE crdid=@crdid";

            var paramas = new { crid = entity.commReceivableId, status = entity.statusId, crdid = entity.commReceivableDtlsId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<UpdateCommSharingModel> UpdateCommSharingModel(UpdateCommSharingModel entity, CancellationToken cancellationToken = default)
        {
            //var query = "UPDATE commission.tbl_chm_commissionsharingmodel SET csmstatus = @csmstatus, modificationdate = NOW() WHERE csmId = @csmId AND NOT EXISTS (SELECT 1 FROM servicemanagement.tbl_sm_serviceoffering tsm WHERE tsm.csmid = @csmId);";
            var query = "UPDATE commission.tbl_chm_commissionsharingmodel SET csmstatus=@csmstatus,modificationdate=NOW() WHERE csmId=@csmId";
            var paramas = new { csmId = entity.CsmId, csmstatus = entity.CsmStatus };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<UpdateCommSharingModelDtls> UpdateCommSharingModelDtls(UpdateCommSharingModelDtls entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE commission.tbl_chm_commissionsharingdetails SET status=@status,modificationdate=NOW() WHERE csdid=@csdid";

            var paramas = new { csdid = entity.CommSharingDtlsId, status = entity.StatusId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<CreateComissionReceivable> AddComissionReceivableAsync(CreateComissionReceivable entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO commission.tbl_chm_commissionreceivable ( crname, crstatus, creator, creationdate) VALUES 
                                                                                 ( @crname, @crstatus, @creator, @creationdate)";

            var paramas = new { crname = entity.ComissionReceivableName, crstatus = entity.Status, creator = entity.UserId, creationdate = DateTime.Now };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }

        public async Task<bool> CheckDuplicateName(CreateComissionReceivable entity)
        {
            
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                string name = entity.ComissionReceivableName;
                string query = "SELECT EXISTS(SELECT 1 FROM commission.tbl_chm_commissionreceivable WHERE crname = @Name)";
                bool Duplicate = await dbConnection.QueryFirstOrDefaultAsync<bool>(query, new { Name = name });
                return Duplicate;
            }
            
        }
        public async Task<CreateCommSharingModel> AddComissionSharingModelAsync(CreateCommSharingModel entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO commission.tbl_chm_commissionsharingmodel ( csmname, csmstatus, creator, creationdate) VALUES 
                                                                                   ( @csmname, @csmstatus, @creator, @creationdate)";

            var paramas = new { csmname = entity.CommSharingModelName, csmstatus = entity.CommSharingModelStatus, creator = entity.UserId, creationdate = DateTime.Now };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }
        public async Task<CreateCommReceivablesDtls> AddComissionReceivableDtlsAsync(CreateCommReceivablesDtls entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO commission.tbl_chm_commissionreceivabledetails (
crid, minvalue, maxvalue,commtype,baseparam,bankifyshare,status,creator,creationdate)
	VALUES (  @crid, @minvalue, @maxvalue,@commtype,@baseparam,@bankifyshare, @status, @creator, @creationdate)";

            var paramas = new { crid = entity.commReceivableId, minvalue = entity.minimumValue, maxvalue = entity.maximumValue, commtype = entity.commType, baseparam = entity.baseParam, bankifyshare = entity.sevenPayShare, status = entity.statusId, creator = entity.UserId, creationdate = DateTime.Now };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }
        public async Task<CreateCommSharingModelDtls> AddComissionSharingModelDtlsAsync(CreateCommSharingModelDtls entity, CancellationToken cancellationToken = default)
        {
            //           string query = @"INSERT INTO commission.tbl_chm_commissionsharingdetails (
            //  csmid, minvalue, maxvalue,commtype,baseparam,retailershare,distshare,bankifyshare,status,creator,creationdate)
            //VALUES (  @csmid, @minvalue, @maxvalue,@commtype,@baseparam,@retailershare,@distshare,@bankifyshare, @status, @creator, @creationdate)";
            //var paramas = new { csmid = entity.commSharingId, minvalue = entity.minimumValue, maxvalue = entity.maximumValue, commtype = entity.commType, baseparam = entity.baseParam, retailershare = entity.retailerShare, distshare = entity.DistShare, bankifyshare = entity.sevenPayShare, status = entity.status, creator = entity.UserId, creationdate = DateTime.Now };
            string query = @"INSERT INTO commission.tbl_chm_commissionsharingdetails (
	  csmid, minvalue, maxvalue,commtype,baseparam,retailershare,distshare,bankifyshare,status,creator,creationdate,superdistshare)
	VALUES (  @csmid, @minvalue, @maxvalue,@commtype,@baseparam,@retailershare,@distshare,@bankifyshare, @status, @creator, @creationdate,@superdistshare)";
            var paramas = new { csmid = entity.commSharingId, minvalue = entity.minimumValue, maxvalue = entity.maximumValue, commtype = entity.commType, baseparam = entity.baseParam, retailershare = entity.retailerShare, distshare = entity.DistShare, bankifyshare = 0, status = entity.status, creator = entity.UserId, creationdate = DateTime.Now, superdistshare = entity.SuperDistShare };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }
        public Task<Comission> UpdateAsync(Comission entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateCommSharingModel> GetComissionSharingModelAsync(string CommSharingModelName, CancellationToken cancellationToken = default)
        {
            var query = "select csmname  as CommSharingModelName from commission.tbl_chm_commissionsharingmodel where csmname = @CommSharingModelName";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryFirstOrDefaultAsync<CreateCommSharingModel>(query, new { CommSharingModelName });
                return comissionDtls;
            }
        }

        public async Task<IEnumerable<DynamicSearchComissionReceiveableModel>> GetDynamicSearchComissionReceiveable(DynamicSearchComissionReceiveable entity, CancellationToken cancellationToken = default)
        {
            List<DynamicSearchComissionReceiveableModel> slist = new List<DynamicSearchComissionReceiveableModel>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("SELECT\r\n  COUNT(crid) OVER() AS totalcount, \r\ncrid,\r\n  crname ,\r\n  crstatus ,\r\n  creator ,\r\n  creationdate \r\nFROM commission.tbl_chm_commissionreceivable\r\n ");
                queryBuilder.Append($" WHERE\r\n LOWER(crname) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(crstatus AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nORDER BY crid DESC");
                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicSearchComissionReceiveableModel>(queryBuilder.ToString());
                    return (IEnumerable<DynamicSearchComissionReceiveableModel>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;

        }

        public async Task<IEnumerable<DynamicSearchSharingModels>> GetDynamicSearchSharingModels(DynamicSearchComissionReceiveable entity, CancellationToken cancellationToken = default)
        {
            List<DynamicSearchSharingModels> slist = new List<DynamicSearchSharingModels>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("SELECT\r\n  COUNT(csmid) OVER() AS totalcount,\r\n  csmid,\r\ncsmname,\r\ncsmstatus,\r\ncreationdate \r\nFROM commission.tbl_chm_commissionsharingmodel ");
                queryBuilder.Append($" WHERE\r\n LOWER(csmname) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(csmstatus AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nORDER BY csmid DESC");
                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicSearchSharingModels>(queryBuilder.ToString());
                    return (IEnumerable<DynamicSearchSharingModels>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;

        }

    }
}
