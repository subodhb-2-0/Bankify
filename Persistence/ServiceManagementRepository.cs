using Dapper;
using Domain.Entities.Servicemanagement;
using Domain.RepositoryInterfaces;
using System.Data;
using System.Text;

namespace Persistence
{
    public class ServiceManagementRepository : IServiceManagementRepository
    {
        private readonly DapperContext _context;
        public ServiceManagementRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Tuple<IEnumerable<GetServiceManagement>, int>> GetAllServiceCatagoryAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Select count(*) from servicemanagement.tbl_mst_service_category; ");
            stringBuilder.AppendLine("select * from servicemanagement.tbl_mst_service_category ");

            //orderByColumn
            stringBuilder.AppendFormat(" order by {0} ", orderByColumn ?? " creator ");

            //orderBy
            stringBuilder.AppendFormat(" {0} ", orderBy ?? " asc ");

            //Limit and Offset
            if (pageSize != null && pageSize != 0)
            {
                int? limit = pageSize;
                int? offset = (pageNumber - 1) * pageSize;
                stringBuilder.AppendFormat("limit {0} offset {1}", limit, offset);
            }
            var query = stringBuilder.ToString();
            //var query = "";
            //if (pageSize == null || pageSize == 0)
            //{
            //    query = string.Format("Select count(*) from servicemanagement.tbl_mst_service_category;" +
            //    " select * from servicemanagement.tbl_mst_service_category order by {0} {1}", orderByColumn, orderBy);
            //}
            //else
            //{
            //    int? limit = pageSize;
            //    int? offset = (pageNumber - 1) * pageSize;

            //    query = string.Format("Select count(*) from servicemanagement.tbl_mst_service_category;" +
            //    " select * from servicemanagement.tbl_mst_service_category order by {0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            //}
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();

                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var servicecat = multi.Read<GetServiceManagement>().ToList();

                    return new Tuple<IEnumerable<GetServiceManagement>, int>(servicecat.ToList<GetServiceManagement>(), count);
                }
            }
        }

        public async Task<Tuple<IEnumerable<GetMasterService>, int>> GetAllServiceAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            try
            {
                string searchFilterQuery = "where tcc.servicecategoryid::TEXT LIKE '%{searchValue}%' or lower(tcc.servicename) like lower('%'||'{searchValue}'||'%') or tcc.status ::TEXT LIKE '%{searchValue}%' or  tcc.serviceid ::TEXT LIKE '%{searchValue}%'";
                pageNumber = pageNumber ?? 1;
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("Select count(*) from servicemanagement.tbl_mst_service; ");
                stringBuilder.AppendLine("select * from servicemanagement.tbl_mst_service tcc ");

                if (!string.IsNullOrEmpty(searchBy))
                {
                    searchFilterQuery = searchFilterQuery.Replace("{searchValue}", searchBy);
                    stringBuilder.AppendLine(searchFilterQuery);
                }

                stringBuilder.AppendFormat("order by {0} ", string.IsNullOrEmpty(orderByColumn) ? " serviceid " : orderByColumn);
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
                        var servicecat = multi.Read<GetMasterService>().ToList();
                        return new Tuple<IEnumerable<GetMasterService>, int>(servicecat.ToList<GetMasterService>(), count);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<IEnumerable<GetSupplier>, int>> GetAllSupplierAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            try
            {
                //string searchFilterQuery = "WHERE tms.suppliertype = 1 AND \r\n      (tms.supplierid::TEXT LIKE '%{searchValue}%' OR\r\n       LOWER(tms.suppliercode) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       LOWER(tms.suppliername) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       LOWER(tms2.servicename) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       tms.status::TEXT LIKE '%{searchValue}%' OR\r\n       tms.suppliertype::TEXT LIKE '%{searchValue}%')";
                string searchFilterQuery = " AND  (tms.supplierid::TEXT LIKE '%{searchValue}%' OR\r\n       LOWER(tms.suppliercode) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       LOWER(tms.suppliername) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       LOWER(tms2.servicename) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       tms.status::TEXT LIKE '%{searchValue}%' OR\r\n       tms.suppliertype::TEXT LIKE '%{searchValue}%')";
                pageNumber = pageNumber ?? 1;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Select count(*) from servicemanagement.tbl_mst_suppliers tms\r\ninner join servicemanagement.tbl_mst_service tms2 on tms.serviceid = tms2.serviceid where tms.suppliertype = 1;");
                stringBuilder.AppendLine("SELECT tms.supplierid,tms.suppliercode,tms.suppliername,tms.supplierdesc,tms.status,tms.creationdate,tms2.servicename,tms.suppliertype \r\nFROM servicemanagement.tbl_mst_suppliers tms\r\nINNER JOIN servicemanagement.tbl_mst_service tms2 on tms.serviceid = tms2.serviceid WHERE tms.suppliertype = 1");
                
                if (!string.IsNullOrEmpty(searchBy))
                {
                    searchFilterQuery = searchFilterQuery.Replace("{searchValue}", searchBy);
                    stringBuilder.AppendLine(searchFilterQuery);
                }

                stringBuilder.AppendFormat("ORDER BY {0} ", string.IsNullOrEmpty(orderByColumn) ? " tms.supplierid " : orderByColumn);
                stringBuilder.AppendFormat(" {0} ", string.IsNullOrEmpty(orderBy) ? " DESC " : orderBy);

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
                        var servicecat = multi.Read<GetSupplier>().ToList();
                        return new Tuple<IEnumerable<GetSupplier>, int>(servicecat.ToList<GetSupplier>(), count);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<IEnumerable<GetSupplier>, int>> GetAllProviderAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            try
            {
                string searchFilterQuery = " AND  (tms.supplierid::TEXT LIKE '%{searchValue}%' OR\r\n       LOWER(tms.suppliercode) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       LOWER(tms.suppliername) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       LOWER(tms2.servicename) LIKE LOWER('%'||'{searchValue}'||'%') OR\r\n       tms.status::TEXT LIKE '%{searchValue}%' OR\r\n       tms.suppliertype::TEXT LIKE '%{searchValue}%')";
                pageNumber = pageNumber ?? 1;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Select count(*) from servicemanagement.tbl_mst_suppliers tms\r\ninner join servicemanagement.tbl_mst_service tms2 on tms.serviceid = tms2.serviceid where tms.suppliertype = 2; ");
                stringBuilder.AppendLine("SELECT tms.supplierid, tms.suppliercode, tms.suppliername, tms.supplierdesc, tms.status, tms.creationdate, tms2.servicename, tms.suppliertype \r\nFROM servicemanagement.tbl_mst_suppliers tms\r\nINNER JOIN servicemanagement.tbl_mst_service tms2 ON tms.serviceid = tms2.serviceid WHERE tms.suppliertype = 2 ");

                if (!string.IsNullOrEmpty(searchBy))
                {
                    searchFilterQuery = searchFilterQuery.Replace("{searchValue}", searchBy);
                    stringBuilder.AppendLine(searchFilterQuery);
                }

                stringBuilder.AppendFormat("ORDER BY {0} ", string.IsNullOrEmpty(orderByColumn) ? " tms.supplierid " : orderByColumn);
                stringBuilder.AppendFormat(" {0} ", string.IsNullOrEmpty(orderBy) ? " DESC " : orderBy);

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
                        var servicecat = multi.Read<GetSupplier>().ToList();
                        return new Tuple<IEnumerable<GetSupplier>, int>(servicecat.ToList<GetSupplier>(), count);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<MasterServiceCreate> AddMasterServiceAsync(MasterServiceCreate entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO servicemanagement.tbl_mst_service (
	 servicecode, servicename,status, creator, creationdate,servicecategoryid,remarks,servicedescription)
	VALUES ( @servicecode, @servicename,@status, @creator, @creationdate,@servicecategoryid,@remarks,@servicedescription)";

            var paramas = new { servicecode = entity.serviceCode, servicename = entity.serviceName, status = entity.Status, creator = entity.UserId, creationdate = DateTime.Now, servicecategoryid = entity.servicecategoryId, remarks = entity.remarks, servicedescription = entity.servicedescription };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }

        public async Task<MasterSupplierResponse> AddMasterSupplierAsync(CreateMasterSupplier entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"servicemanagement\".Stp_sm_AddSuppliers";
                var parameters = new DynamicParameters();
                parameters.Add("p_suppliercode", entity.SupplierCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_suppliername", entity.supplierName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_status", entity.remarks, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_creator", 0, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_supplierdesc", entity.SupplierDesc, DbType.String, ParameterDirection.Input);
                parameters.Add("p_serviceid", entity.serviceId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_suppliertype", entity.supplierType, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<MasterSupplierResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new MasterSupplierResponse();
        }
        public async Task<CreatePstoSuppliers> AddPstoSupplierAsync(CreatePstoSuppliers entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO servicemanagement.tbl_sm_supp_sp_map (
	 serviceid, supplierid,status, creator, creationdate,serviceproviderpid,crid)
	VALUES ( @serviceid, @supplierid,@status, @creator, @creationdate,@serviceproviderpid,@crid)";

            var paramas = new { serviceid = entity.serviceId, supplierid = entity.supplierId, status = entity.Status, creator = entity.UserId, creationdate = DateTime.Now, serviceproviderpid = entity.serviceProviderId, crid = entity.commReceivableId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }

        }
        public async Task<MasterServiceUpdate> UpdateMasterServiceAsync(MasterServiceUpdate entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE servicemanagement.tbl_mst_service SET servicecode=@servicecode, servicename=@servicename,servicecategoryId=@servicecategoryId, status=@status, remarks=@remarks,servicedescription=@servicedescription, modificationdate=NOW() WHERE serviceid = @serviceid";

            var paramas = new { servicecode = entity.serviceCode, servicename = entity.serviceName, status = entity.Status, serviceid = entity.serviceId, remarks = entity.remarks, servicedescription = entity.servicedescription, servicecategoryId = entity.servicecategoryId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<UpdateServiceProvider> UpdateServiceProviderAsync(UpdateServiceProvider entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE servicemanagement.tbl_mst_suppliers SET suppliercode=@suppliercode, suppliername=@suppliername,supplierdesc=@supplierdesc, status=@status, modificationdate=NOW() WHERE supplierid = @supplierid";

            var paramas = new { suppliercode = entity.Suppliercode, suppliername = entity.Suppliername, supplierdesc = entity.Supplierdesc, status = entity.Status, supplierid = entity.Supplierid };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<UpdateServiceStatus> UpdateServiceStatusAsync(UpdateServiceStatus entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE servicemanagement.tbl_mst_service SET status=@status , modificationdate = NOW() WHERE serviceid = @serviceid";

            var paramas = new { status = entity.Status, serviceid = entity.ServiceId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<UpdateServiceProviderStatus> UpdateServiceProviderStatusAsync(UpdateServiceProviderStatus entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE servicemanagement.tbl_mst_suppliers SET status=@status, modificationdate = NOW()  WHERE supplierid = @supplierid";

            var paramas = new { status = entity.Status, supplierid = entity.SupplierId };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<GetServiceManagement> UpdateAsync(GetServiceManagement entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE servicemanagement.tbl_mst_service_category SET servicecategoryname=@servicecategoryname, servicecategorydesc=@servicecategorydesc, creator=@creator,status=@status, modificationdate=@modificationdate WHERE tbl_mst_sc_id = @tbl_mst_sc_id";

            var paramas = new { servicecategoryname = entity.servicecategoryname, servicecategorydesc = entity.servicecategorydesc, creator = entity.creator, status = entity.status, modificationdate = DateTime.Now, tbl_mst_sc_id = entity.tbl_mst_sc_id };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<IEnumerable<GetViewBySupplier>> GetViewBySupplier(int supplierid, CancellationToken cancellationToken = default)
        {
            var query = "select distinct tsssm.serviceid,tsssm.serviceproviderpid,tsssm.suppspmapid,tsssm.crid,tsssm.status,tms2.servicename,\r\ntms.suppliername,tcc.crname,tms.supplierid, tsssm.creationdate as creationdate from servicemanagement.tbl_mst_suppliers tms\r\ninner join servicemanagement.tbl_sm_supp_sp_map tsssm on tsssm.serviceproviderpid  = tms.supplierid  \r\ninner join servicemanagement.tbl_mst_service tms2  on tms2.serviceid = tsssm.serviceid\r\ninner join commission.tbl_chm_commissionreceivable tcc on tcc.crid = tsssm.crid\r\nwhere tsssm.supplierid = @supplierid Order by creationdate desc";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var view = await dbConnection.QueryAsync<GetViewBySupplier>(query, new { supplierid });
                return view.ToList<GetViewBySupplier>();
            }
        }
        public async Task<IEnumerable<GetServiceProviderByService>> GetServiceProvider(int ServiceId, CancellationToken cancellationToken = default)
        {
            var query = "select tms2.suppliername as Serviceprovider,tms2.supplierid SupplierId from servicemanagement.tbl_mst_service tms \r\ninner join servicemanagement.tbl_mst_suppliers tms2 on tms2.serviceid = tms.serviceid\r\nwhere tms2.suppliertype = 2 and tms2.status = 2 and tms.serviceid = @serviceid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var view = await dbConnection.QueryAsync<GetServiceProviderByService>(query, new { ServiceId });
                return view.ToList<GetServiceProviderByService>();
            }
        }
        public async Task<IEnumerable<GetMasterService>> GetAllServiceBySearchAsync(int serviceid, string? servicename, int servicecategoryid, int status, int creator, DateTime creationdate, CancellationToken cancellationToken = default)
        {

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select tms.serviceid,tms.servicecode,tms.servicename,tms.status,tms.creator,\r\ntms.creationdate,tms.modifier,tms.modificationdate,tms.servicecategoryid\r\nfrom servicemanagement.tbl_mst_service tms ");
            queryBuilder.Append(" where 1=1 ");

            if (serviceid > 0)
            {
                queryBuilder.Append($" AND tms.serviceid = {serviceid} ");
            }

            if (servicename != null)
            {
                queryBuilder.Append($" AND tms.servicename = '{servicename}' ");
            }

            if (servicecategoryid > 0)
            {
                queryBuilder.Append($" AND tms.servicecategoryid = {servicecategoryid} ");
            }

            if (status > 0)
            {
                queryBuilder.Append($" AND tms.status = {status} ");
            }

            if (creator > 0)
            {
                queryBuilder.Append($" AND tms.creator = {creator} ");
            }



            if (creationdate != DateTime.MinValue)
            {
                queryBuilder.Append($" AND tms.creationdate  >= '{creationdate:yyyy-MM-dd}'::date ");

            }

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var Service = await dbConnection.QueryAsync<GetMasterService>(queryBuilder.ToString());
                return (IEnumerable<GetMasterService>)Service;
            }
        }
        public Task<GetServiceManagement> AddAsync(GetServiceManagement entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GetServiceManagement> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetServiceManagement>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GetServiceManagement> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<UpdateServiceSupplierData> UpdateServiceSupplierData(UpdateServiceSupplierData entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE servicemanagement.tbl_mst_suppliers SET suppliercode=@suppliercode, suppliername=@suppliername, status=@status, modificationdate=NOW() WHERE supplierid = @supplierid";

            var paramas = new { suppliercode = entity.Suppliercode, suppliername = entity.Suppliername, status = entity.Status, supplierid = entity.Supplierid };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<int> DeactivateAssignProviderBySuppId(int suppspmapid, CancellationToken cancellationToken = default)
        {
            var query = "update servicemanagement.tbl_sm_supp_sp_map set status = 3 where suppspmapid = @suppspmapid";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, new { suppspmapid });
                return result;
            }
        }

        public async Task<IEnumerable<DynamicManageService>> GetDynamicSearchService(DynamicSearchRequest entity, CancellationToken cancellationToken = default)
        {
            List<DynamicManageService> slist = new List<DynamicManageService>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("select COUNT(serviceid) OVER() AS totalcount,serviceid, servicecode,servicename,status,creator,servicecategoryid,\r\nremarks,servicedescription,\r\ncreationdate  \r\nfrom servicemanagement.tbl_mst_service ");
                queryBuilder.Append($" where LOWER(servicecode) LIKE LOWER('%'||'{entity.p_searchoption}'||'%') \r\nOR LOWER(CAST(servicename AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%') \r\nOR LOWER(CAST(servicecategoryid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nOR LOWER(CAST(servicedescription AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nOR LOWER(CAST(remarks AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nORDER BY serviceid DESC");
                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicManageService>(queryBuilder.ToString());
                    return (IEnumerable<DynamicManageService>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<DynamicManageServiceProvider>> GetDynamicSearchServiceProvider(DynamicSearchRequest entity, CancellationToken cancellationToken = default)
        {
            List<DynamicManageServiceProvider> slist = new List<DynamicManageServiceProvider>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append("SELECT\r\n  COUNT(supplierid) OVER() AS totalcount,\r\n  suppliercode, suppliername, supplierdesc,serviceid,suppliertype,status,creator,\r\n  creationdate AS creationdate\r\nFROM servicemanagement.tbl_mst_suppliers ");
                //queryBuilder.Append($" where suppliertype = 2 and LOWER(suppliercode) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(suppliername AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(supplierdesc AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(serviceid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(suppliertype AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(status AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nORDER BY supplierid DESC");

                queryBuilder.Append("SELECT\r\n  COUNT(tms.supplierid) OVER() AS totalcount,\r\n  tms.supplierid, tms.suppliercode, tms.suppliername, tms.supplierdesc, tms.status, tms.creationdate, tms2.servicename, tms.suppliertype from servicemanagement.tbl_mst_suppliers tms\r\ninner join servicemanagement.tbl_mst_service tms2 on tms.serviceid = tms2.serviceid where tms.suppliertype = 2 ");
                queryBuilder.Append($" and ( LOWER(tms.suppliercode) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.suppliername AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.supplierdesc AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.serviceid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.suppliertype AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.status AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%'))\r\nORDER BY tms.supplierid DESC");

                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicManageServiceProvider>(queryBuilder.ToString());
                    return (IEnumerable<DynamicManageServiceProvider>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<DynamicManageServiceProvider>> GetDynamicSearchServiceSupplier(DynamicSearchRequest entity, CancellationToken cancellationToken = default)
        {
            List<DynamicManageServiceProvider> slist = new List<DynamicManageServiceProvider>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("SELECT\r\n  COUNT(tms.supplierid) OVER() AS totalcount,\r\n  tms.supplierid, tms.suppliercode, tms.suppliername, tms.supplierdesc, tms.status, tms.creationdate, tms2.servicename, tms.suppliertype from servicemanagement.tbl_mst_suppliers tms\r\ninner join servicemanagement.tbl_mst_service tms2 on tms.serviceid = tms2.serviceid where tms.suppliertype = 1 ");
                queryBuilder.Append($" and ( LOWER(tms.suppliercode) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.suppliername AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.supplierdesc AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.serviceid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.suppliertype AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tms.status AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%'))\r\nORDER BY tms.supplierid DESC");
                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicManageServiceProvider>(queryBuilder.ToString());
                    return (IEnumerable<DynamicManageServiceProvider>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<PanDefault> GetPanDefaulterById(int id, CancellationToken cancellationToken = default)
        {
            var query = "SELECT tdpl.id, tdpl.pan_number \r\nFROM common.tbl_defaulter_pan_list tdpl\r\n WHERE tdpl.id = @id;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<PanDefault>(query, new { id });
                return result;
            }
        }

        public async Task<MobileDefault> GetMobileDefaulterById(int id, CancellationToken cancellationToken = default)
        {
            var query = "SELECT id, mobile_number \r\nFROM common.tbl_defaulter_mobile_list\r\n WHERE id = @id;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<MobileDefault>(query, new { id });
                return result;
            }
        }

    }
}


