using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;
using Domain.Entities.Account;
using Contracts.Onboarding;
using Domain.Entities.Servicemanagement;
using System.Text;
using Domain.Entities.Product;
using static Dapper.SqlMapper;

namespace Persistence.Onboarding
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DapperContext _context;
        public InventoryRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Inventory> AddAsync(Inventory entity, CancellationToken cancellationToken = default)
        {
            string query = "INSERT INTO \"Inventory\".tbl_sc_inventory(orgid, \"channelId\", productid, totalinventory, totalamt, purchasedate) VALUES( @orgid, @channelId, @productid, @totalinventory, @totalamt, CURRENT_DATE) RETURNING \"inventoryId\"";
            var paramas = new
            {
                
                orgid = entity.OrgId,
                channelId = entity.ChannelId,
                productid = entity.ProductId,
                totalinventory = entity
                .totalInventory,
                totalamt = entity.totalAmt,
            };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteScalarAsync<int>(query, paramas);
                entity.inventoryId = result;
                return entity;
            }
        }

        public Task<Inventory> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string query = String.Format("SELECT * FROM \"Inventory\".tbl_sc_inventory");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<Inventory>();
                }
            }
        }

        public Task<Inventory> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Inventory>> GetByOrgIdAsync(int orgId, CancellationToken cancellationToken = default)
        {
            string query = String.Format("SELECT * FROM \"Inventory\".tbl_sc_inventory where orgId = {0}", orgId);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<Inventory>();
                }
            }
        }

        public Task<Inventory> UpdateAsync(Inventory entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<RetailInventoryResponseModel> BuyRetailerInventory(BuyRetailerInventoryRequestDto requestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"Inventory\".stp_acc_requestpurchageinventory"; //"\"payments\".func_acc_addpayments";
                var parameters = new DynamicParameters();
                  // integer, p_channelid integer, p_productid integer, p_amount numeric, p_userid bigint, p_remarks text
                parameters.Add("p_distorgid", requestDto.OrgId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("p_totalinventorycount", requestDto.totalLot, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_channelid", requestDto.channelId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_productid", requestDto.productId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_amount", requestDto.totalAmount, DbType.VarNumeric, ParameterDirection.Input);
                parameters.Add("p_userid", requestDto.userid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_remarks", requestDto.remark, DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<RetailInventoryResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new RetailInventoryResponseModel();
        }
        public async Task<SaleInventoryResponseModel> SellInventory(SaleInventoryRequestDto requestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"Inventory\".stp_acc_updatesaleinventory"; 
                var parameters = new DynamicParameters();
                parameters.Add("p_orgid", requestDto.OrgId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("p_status", requestDto.status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_cpafnnumber", requestDto.cpafnnumber, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<SaleInventoryResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new SaleInventoryResponseModel();
        }

        public async Task<IEnumerable<RetailerBalance>> GetRetailerBalance(int parantorgid, CancellationToken cancellationToken = default)
        {
            var query = "select orgname||'('||orgcode||')' as orgname ,orgid from onboarding.tbl_chm_channelpartners tcc\r\n where parentorgid =@parantorgid and orgtype=2;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<RetailerBalance>(query, new { parantorgid });
                return comissionDtls.ToList<RetailerBalance>();
            }
        }

        public async Task<IEnumerable<ProductWiseBuySaleInventoryDetailsResInfo>> GetProductWiseBuySaleInventoryDetails(string fromdate, string todate, int productid, int channelid, int distributororgid, CancellationToken cancellationToken = default)
        {
            List<ProductWiseBuySaleInventoryDetailsResInfo> slist = new List<ProductWiseBuySaleInventoryDetailsResInfo>();
            try
            {
                var procedureName = "\"onboarding\".stp_acc_getproductwisebuysaleinventorydetails";
                var parameters = new DynamicParameters();
                parameters.Add("p_fromdate", Convert.ChangeType(fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", Convert.ChangeType(todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_productid", productid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_channelid", channelid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_distributororgid", distributororgid, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<ProductWiseBuySaleInventoryDetailsResInfo>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<ProductWiseBuySaleInventoryDetailsResInfo>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<RetailerInfoByDistributorId>> GetRetailerInfoByDistributorId(string fromdate, string todate, int offsetrows, int fetchrows, int distributororgid, CancellationToken cancellationToken = default)
        {
            List<RetailerInfoByDistributorId> slist = new List<RetailerInfoByDistributorId>();
            try
            {
                var procedureName = "\"onboarding\".stp_acc_getretailerinfobydistributorid";
                var parameters = new DynamicParameters();
                parameters.Add("p_fromdate", Convert.ChangeType(fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", Convert.ChangeType(todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_offsetrows", offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", fetchrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_distributororgid", distributororgid, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<RetailerInfoByDistributorId>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<RetailerInfoByDistributorId>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<DistributortxnDetails>> GetDistributortxnDetails(int distributororgid, string fromdate, string todate, CancellationToken cancellationToken = default)
        {
            List<DistributortxnDetails> slist = new List<DistributortxnDetails>();
            try
            {
                var procedureName = "\"onboarding\".stp_chm_getdistributortxndetails";
                var parameters = new DynamicParameters();
                parameters.Add("p_distributorid", distributororgid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fromdate", Convert.ChangeType(fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", Convert.ChangeType(todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<DistributortxnDetails>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<DistributortxnDetails>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<DistributorDetails>> GetDistributorDetails(int distributororgid, CancellationToken cancellationToken = default)
        {
            var query = "select tcc.orgid , tcc.orgcode , tcc.orgname ,acc.runningbalance  FROM accounts.tbl_acc_wallet acc \r\ninner join onboarding.tbl_chm_channelpartners tcc on acc.orgid = tcc.orgid \r\nwhere acc.orgid  =@distributororgid;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<DistributorDetails>(query, new { distributororgid });
                return comissionDtls.ToList<DistributorDetails>();
            }
        }
        public async Task<IEnumerable<DynamicSearchProduct>> GetDynamicSearchProduct(DynamicSearchRequest entity, CancellationToken cancellationToken = default)
        {
            List<DynamicSearchProduct> slist = new List<DynamicSearchProduct>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                //queryBuilder.Append("SELECT\r\n  COUNT(productid) OVER() AS totalcount,productId,\r\n  productname, productdesc, channelid,status,creator,\r\n  creationdate AS creationdate\r\nFROM servicemanagement.tbl_sm_product  ");
                //queryBuilder.Append($" where LOWER(productname) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(productdesc AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(channelid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(status AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nORDER BY productid DESC");

                queryBuilder.Append("SELECT\r\n  COUNT(productid) OVER() AS totalcount,productId, productName, productDesc, channelName, tsp.status, tsp.creationDate\r\nFROM servicemanagement.tbl_sm_product tsp , onboarding.tbl_chm_channel a where tsp.channelid = a.channelId ");
                queryBuilder.Append($" and ( LOWER(tsp.productname) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tsp.productdesc AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tsp.channelid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(tsp.status AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%'))\r\nORDER BY tsp.productid DESC");

                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($"  LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicSearchProduct>(queryBuilder.ToString());
                    return (IEnumerable<DynamicSearchProduct>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

       
        public async Task<IEnumerable<SalesChannelGetProductDetails>> SalesChannelGetProductDetails(string? mobilenumber, CancellationToken cancellationToken = default)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("select tuu.status , tuu.mobilenumber , tcc.orgcode , tcc.orgtype, tcc.orgname, tsp.productname , tsp.productid, concat(toc.perm_dist, toc.perm_sub_dist, toc.perm_landmark, toc.perm_house_no, toc.perm_pincode) ::character varying as EmployeeAddress");
                stringBuilder.AppendLine("from user_management.tbl_um_users tuu");
                stringBuilder.AppendLine("join onboarding.tbl_chm_channelpartners tcc  on tcc.orgid = tuu.orgid");
                stringBuilder.AppendLine("join servicemanagement.tbl_sm_product tsp  on tcc.productid = tsp.productid");
                stringBuilder.AppendLine("join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid ");
                stringBuilder.AppendLine("where tcc.status = 2 and");

                string q2 = "LOWER(tuu.mobilenumber) LIKE LOWER('%'||'{mobilenumber}'||'%');";
                q2 = q2.Replace("{mobilenumber}", mobilenumber);

                stringBuilder.AppendLine(q2);
                string query = stringBuilder.ToString();

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var rawData = await connection.QueryAsync<SalesChannelGetProductDetails>(query);
                    return rawData.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GetListOfActiveProduct>> GetListOfActiveProduct(CancellationToken cancellationToken = default)
        {
            try
            {
                string query = "select tsp.productid , tsp.productname , tsp.status from servicemanagement.tbl_sm_product tsp where tsp.status  = 2 order by tsp.productid  asc;";
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var rawData =await  connection.QueryAsync<GetListOfActiveProduct>(query);
                    return rawData.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> RetailerProductUpdateByProductId(int? productId, string? mobileNumber, string? orgCode, CancellationToken cancellationToken = default)
        {
            try
            {
                /*string query = @$" UPDATE onboarding.tbl_chm_channelpartners tcc SET productid= @productid 
                                 FROM user_management.tbl_um_users tuu 
                                   WHERE tuu.userid = tcc.orgid and tcc.orgtype = 2 AND tuu.mobilenumber = '{mobilenumber}' AND tcc.orgid= '{orgcode}'";*/
                string query = @$"UPDATE onboarding.tbl_chm_channelpartners  AS cp SET productid = {productId} FROM user_management.tbl_um_users  AS u WHERE cp.orgid = u.orgid       AND cp.orgcode = '{orgCode}'     AND u.mobilenumber = '{mobileNumber}';";
              
                var parameters = new { productid = productId };

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
    }

}
