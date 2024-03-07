using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;
using Domain.Entities.Acquisition;
using Domain.Entities.Product;
using static Dapper.SqlMapper;
using Domain.Entities.Account;
using Contracts.Role;
using Domain.Entities.RoleManagement;
using System.Data.Common;
using System.Text;
using Contracts.Account;
using System.Net.NetworkInformation;
using System;

namespace Persistence.Onboarding
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string query = String.Format("SELECT * FROM servicemanagement.tbl_sm_product");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<Product>();
                }
            }
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            string query = String.Format(@"SELECT * FROM servicemanagement.tbl_sm_product where productid={0}", id);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<Product>(query);
            }
        }


        public async Task<Tuple<IEnumerable<ProductList>, int>> GetAllProductList(int channelId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            
            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("select Count(1) from servicemanagement.tbl_sm_product tsp , onboarding.tbl_chm_channel a where tsp.channelid = a.channelId  and tsp.channelid =  (CASE WHEN {0} > 0 THEN {0} ELSE tsp.channelid END)   ",channelId);
            if (!string.IsNullOrEmpty(searchBy))
            {
                stringBuilder.Append($" and ( LOWER(tsp.productname) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(tsp.productdesc AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(tsp.channelid AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(tsp.status AS text)) LIKE LOWER('%'||'{searchBy}'||'%'))\r\n");
            }
            stringBuilder.AppendFormat("; SELECT productId, productName, productDesc, channelName, tsp.status, tsp.creationDate \r\nFROM servicemanagement.tbl_sm_product tsp, onboarding.tbl_chm_channel a WHERE tsp.channelid = a.channelId AND tsp.channelid = (CASE WHEN {0} > 0 THEN {0} ELSE tsp.channelid END) ",channelId);
            if (!string.IsNullOrEmpty(searchBy))
            {
                stringBuilder.Append($" and ( LOWER(tsp.productname) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR  LOWER(a.channelname) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(tsp.productdesc AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(tsp.productid AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(tsp.status AS text)) LIKE LOWER('%'||'{searchBy}'||'%'))\r\n");
            }
            stringBuilder.AppendFormat(" order by tsp.{0} ", orderByColumn ?? " productid ");
            stringBuilder.AppendFormat(" {0} ", orderBy ?? " asc ");
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
                        var users = multi.Read<ProductList>().ToList();
                        return new Tuple<IEnumerable<ProductList>, int>(users.ToList<ProductList>(), count);
                    }
            }
        }
        public async Task<Tuple<IEnumerable<ProductDetailsbyId>, int>> GetProductDetailsbyId(int productId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            //var query = string.Format("select Count(1) from servicemanagement.tbl_sm_serviceoffering tss \r\ninner join servicemanagement.tbl_mst_service tms2 \r\non tss.serviceId=tms2.serviceid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tss.supplierid =tms.supplierId\r\ninner join servicemanagement.tbl_mst_suppliers tms1 on tss.serviceproviderid =tms1.supplierId\r\ninner join servicemanagement.tbl_sm_product tsp \r\non tss.productid = tsp.productId left join commission.tbl_chm_commissionsharingdetails tcc \r\non tss.csmid = tcc.csmId \r\ninner join commission.tbl_chm_commissionsharingmodel tccm on tccm.csmid = tss.csmid \r\ninner join onboarding.tbl_chm_channel tcc1 on tcc1.channelid = tsp.channelid \r\nwhere tss.productId =  {0} ;" +
            //    " select tss.svcoffid,tss.productid,tsp.productname ,tss.serviceid,tms2.servicename  ,\r\ntss.supplierid,tms.suppliername  ,tss.serviceproviderid, tms1.suppliername as ServiceProvideName ,tss.csmid\r\n,tccm.csmname,tsp.channelid , tcc1.channelname  , tss.status \r\nfrom servicemanagement.tbl_sm_serviceoffering tss \r\ninner join servicemanagement.tbl_mst_service tms2 \r\non tss.serviceId=tms2.serviceid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tss.supplierid =tms.supplierId\r\ninner join servicemanagement.tbl_mst_suppliers tms1 on tss.serviceproviderid =tms1.supplierId\r\ninner join servicemanagement.tbl_sm_product tsp \r\non tss.productid = tsp.productId left join commission.tbl_chm_commissionsharingdetails tcc \r\non tss.csmid = tcc.csmId \r\ninner join commission.tbl_chm_commissionsharingmodel tccm on tccm.csmid = tss.csmid \r\ninner join onboarding.tbl_chm_channel tcc1 on tcc1.channelid = tsp.channelid \r\nwhere tss.productId =  {0} " +
            //    "order by tss.{1} {2} limit {3} offset {4}", productId, orderByColumn, orderBy, limit, offset);

            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("select Count(1) from servicemanagement.tbl_sm_serviceoffering tss \r\ninner join servicemanagement.tbl_mst_service tms2 \r\non tss.serviceId=tms2.serviceid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tss.supplierid =tms.supplierId\r\ninner join servicemanagement.tbl_mst_suppliers tms1 on tss.serviceproviderid =tms1.supplierId\r\ninner join servicemanagement.tbl_sm_product tsp \r\non tss.productid = tsp.productId \r\ninner join commission.tbl_chm_commissionsharingmodel tccm on tccm.csmid = tss.csmid \r\ninner join onboarding.tbl_chm_channel tcc1 on tcc1.channelid = tsp.channelid \r\nwhere tss.productId =  {0} ;", productId);
            stringBuilder.AppendFormat(" select tss.svcoffid,tss.productid,tsp.productname ,tss.serviceid,tms2.servicename  ,\r\ntss.supplierid,tms.suppliername  ,tss.serviceproviderid, tms1.suppliername as ServiceProvideName ,tss.csmid\r\n,tccm.csmname,tsp.channelid , tcc1.channelname  , tss.status \r\nfrom servicemanagement.tbl_sm_serviceoffering tss \r\ninner join servicemanagement.tbl_mst_service tms2 \r\non tss.serviceId=tms2.serviceid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tss.supplierid =tms.supplierId\r\ninner join servicemanagement.tbl_mst_suppliers tms1 on tss.serviceproviderid =tms1.supplierId\r\ninner join servicemanagement.tbl_sm_product tsp \r\non tss.productid = tsp.productId \r\ninner join commission.tbl_chm_commissionsharingmodel tccm on tccm.csmid = tss.csmid \r\ninner join onboarding.tbl_chm_channel tcc1 on tcc1.channelid = tsp.channelid \r\nwhere tss.productId =  {0} ", productId);

            //orderByColumn
            stringBuilder.AppendFormat("order by tss.{0} ", orderByColumn ?? " svcoffid ");

            //orderBy
            stringBuilder.AppendFormat(" {0} ", orderBy ?? " asc ");

            if (pageSize != null && pageSize != 0)
            {
                int? limit = pageSize;
                int? offset = (pageNumber - 1) * pageSize;
                stringBuilder.AppendFormat("limit {0} offset {1} ;", limit, offset);
            }
            var query = stringBuilder.ToString();

            //var query = string.Format("select Count(1) from servicemanagement.tbl_sm_serviceoffering tss \r\ninner join servicemanagement.tbl_mst_service tms2 \r\non tss.serviceId=tms2.serviceid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tss.supplierid =tms.supplierId\r\ninner join servicemanagement.tbl_mst_suppliers tms1 on tss.serviceproviderid =tms1.supplierId\r\ninner join servicemanagement.tbl_sm_product tsp \r\non tss.productid = tsp.productId \r\ninner join commission.tbl_chm_commissionsharingmodel tccm on tccm.csmid = tss.csmid \r\ninner join onboarding.tbl_chm_channel tcc1 on tcc1.channelid = tsp.channelid \r\nwhere tss.productId =  {0} ;" +
            //    " select tss.svcoffid,tss.productid,tsp.productname ,tss.serviceid,tms2.servicename  ,\r\ntss.supplierid,tms.suppliername  ,tss.serviceproviderid, tms1.suppliername as ServiceProvideName ,tss.csmid\r\n,tccm.csmname,tsp.channelid , tcc1.channelname  , tss.status \r\nfrom servicemanagement.tbl_sm_serviceoffering tss \r\ninner join servicemanagement.tbl_mst_service tms2 \r\non tss.serviceId=tms2.serviceid \r\ninner join servicemanagement.tbl_mst_suppliers tms on tss.supplierid =tms.supplierId\r\ninner join servicemanagement.tbl_mst_suppliers tms1 on tss.serviceproviderid =tms1.supplierId\r\ninner join servicemanagement.tbl_sm_product tsp \r\non tss.productid = tsp.productId \r\ninner join commission.tbl_chm_commissionsharingmodel tccm on tccm.csmid = tss.csmid \r\ninner join onboarding.tbl_chm_channel tcc1 on tcc1.channelid = tsp.channelid \r\nwhere tss.productId =  {0} " +
            //    "order by tss.{1} {2} limit {3} offset {4}", productId, orderByColumn, orderBy, limit, offset);

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<ProductDetailsbyId>().ToList();
                    return new Tuple<IEnumerable<ProductDetailsbyId>, int>(users.ToList<ProductDetailsbyId>(), count);
                }
            }
        }

        public async Task<int> ActivaeOrDeactivateProduct(int productId, int status, CancellationToken cancellationToken = default)
        {
            var query = "update servicemanagement.tbl_sm_product set status = @status where productId = @productId";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, new { productId, status });
                return result;
            }
        }

        public async Task<AddAcquisitionResponse> AddProductName(AddProduct addProduct, CancellationToken cancellationToken = default)
        {
            //try
            //{
            //    var query = "INSERT INTO servicemanagement.tbl_sm_product\r\n(productname, productdesc, channelid, status, creator, creationdate, modifier, modificationdate, enrollmentfee, lotsize)\r\n" +
            //    "VALUES(@productName, @productdesc, @channelid, 2, 0, @creationdate, 0, @modificationdate, 0, 0) RETURNING productid into '" + addProduct.productId + "'";
            //    var paramas = new { productName = addProduct.productName, productdesc = addProduct.productDesc, channelid = addProduct.channelId, creationdate = DateTime.Now, modificationdate = DateTime.Now };
            //    using (IDbConnection dbConnection = _context.CreateConnection())
            //    {
            //        dbConnection.Open();
            //        var result = await dbConnection.ExecuteAsync(query, paramas);
            //        return result;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            try
            {
                var procedureName = "\"servicemanagement\".stp_sm_addproduct";
                var parameters = new DynamicParameters();
                parameters.Add("p_productname", addProduct.productName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_productdesc", addProduct.productDesc, DbType.String, ParameterDirection.Input);
                parameters.Add("p_channelid", addProduct.channelId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_status", 2, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_creator", 0, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_enrollmentfee", addProduct.enrollmentfee, DbType.VarNumeric, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<AddAcquisitionResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
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
            return new AddAcquisitionResponse();
        }

        public async Task<int> DeactivateServiceOffering(int svcoffid, CancellationToken cancellationToken = default)
        {
            var query = "update servicemanagement.tbl_sm_serviceoffering set status = 3 where svcoffid = @svcoffid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, new { svcoffid });
                return result;
            }
        }
        public async Task<AddServiceOffering> AddServiceOffering(AddServiceOffering entity, CancellationToken cancellationToken = default)
        {
            var query = "INSERT INTO servicemanagement.tbl_sm_serviceoffering (productid, serviceid, supplierid, serviceproviderid,csmid, status, creator, creationdate, modifier, modificationdate)\r\n" +
                "VALUES(@productid, @serviceid, @supplierid, @serviceproviderid, @csmid, 2, 0, @creationdate, 0, null);";
            var paramas = new { productid = entity.productId, serviceid = entity.serviceId, supplierid = entity.supplierId,
                serviceproviderid = entity.serviceproviderId, csmid = entity.CSMId, creationdate = DateTime.Now };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<IEnumerable<ServiceSupplierList>> GetServiceSupplierListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default)
        {
            var query = "SELECT supplierid as ServiceSupplierId,suppliername as ServiceSupplierName,status as status FROM servicemanagement.tbl_mst_suppliers where serviceId=@serviceid and suppliertype =1 and status=@status;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ServiceSupplierList>(query, new { serviceid, status });
                return comissionDtls.ToList<ServiceSupplierList>();
            }
        }
        public async Task<IEnumerable<ServiceProviderList>> GetServiceProviderListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default)
        {
            var query = "SELECT supplierid as ServiceProvideId,suppliername as ServiceProvideName,status as status FROM servicemanagement.tbl_mst_suppliers where serviceId=@serviceid and suppliertype =2 and status=@status;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ServiceProviderList>(query, new { serviceid, status });
                return comissionDtls.ToList<ServiceProviderList>();
            }
        }

        public async Task<IEnumerable<ServiceCSMList>> GetServiceCSM( CancellationToken cancellationToken = default)
        {
            var query = "select csm.csmid as CSMId, csm.csmname as CSMName, csm.csmstatus as status from commission.tbl_chm_commissionsharingmodel csm where csm.csmstatus =2;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ServiceCSMList>(query);
                return comissionDtls.ToList<ServiceCSMList>();
            }
        }

        public async Task<int> UpdateProductDetailsById(UpdateProduct updateProduct, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = "update servicemanagement.tbl_sm_serviceoffering set productid = @productid, serviceid = @serviceid, supplierid = @supplierid , serviceproviderid = @serviceproviderid , csmid = @csmid , modificationdate = @modificationdate where svcoffid = @svcoffid; ";
                var paramas = new { productid = updateProduct.productId, serviceid = updateProduct.serviceId, supplierid = updateProduct.supplierId, serviceproviderid = updateProduct.serviceproviderId, csmid = updateProduct.CSMId, modificationdate = DateTime.Now , svcoffid =updateProduct.svcoffid};
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(query, paramas);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        Task<ProductList> IGenericRepository<ProductList>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProductList>> IGenericRepository<ProductList>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ProductList> IGenericRepository<ProductList>.AddAsync(ProductList entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ProductList> IGenericRepository<ProductList>.UpdateAsync(ProductList entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ProductList> IGenericRepository<ProductList>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductDetails(CancellationToken cancellationToken = default)
        {
            string query = String.Format("SELECT * FROM servicemanagement.tbl_sm_product");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    return multi.Read<Product>();
                }
            }
        }

        public async Task<IEnumerable<Product>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            string query = String.Format(@"SELECT * FROM servicemanagement.tbl_sm_product where productid={0}", id);
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    return await dbConnection.QueryFirstOrDefaultAsync<Product>(query);
            //}
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<Product>(query);
                return comissionDtls.ToList<Product>();
            }
        }

    }
}
