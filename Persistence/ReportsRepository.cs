using Contracts.Report;
using Dapper;
using Domain.Entities.Acquisition;
using Domain.Entities.Reports;
using Domain.RepositoryInterfaces;
using System.Data;
using static Dapper.SqlMapper;

namespace Persistence
{
    public class ReportsRepository : IReportRepository
    {
        private readonly DapperContext _context;
        public ReportsRepository(DapperContext context)
        {
            _context = context;
        } 
        public async Task<IEnumerable<txndetailsresponse>> GetTxnreportDetails(gettxndetailsreports entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "\"transaction\".stp_txn_getAlltxndetails";
            var parameters = new DynamicParameters();
            //var ids = entity.p_transactionids.ToArray();
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_orgid", entity.p_orgid, DbType.Int16, ParameterDirection.Input);
            if (string.IsNullOrEmpty(entity.p_fromdate))
            {
                parameters.Add("p_fromdate", DBNull.Value, DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", DBNull.Value, DbType.Date, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("p_fromdate", Convert.ChangeType(entity.p_fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", Convert.ChangeType(entity.p_todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            }
            parameters.Add("p_transactionids", entity.p_transactionids, DbType.String, ParameterDirection.Input);
            parameters.Add("p_supplierid", entity.p_supplierid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_serviceid", entity.p_serviceid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_txnstatus", entity.p_txnstatus, DbType.Int16, ParameterDirection.Input);
            if (string.IsNullOrEmpty(entity.p_rtorgcode))
            {
                parameters.Add("p_rtorgcode", "0", DbType.String, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("p_rtorgcode", entity.p_rtorgcode, DbType.String, ParameterDirection.Input);
            }
            parameters.Add("p_serviceproviderid", entity.p_serviceproviderid, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<txndetailsresponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<GetretailerledgerResponse>> GetledgerDetails(Getretailerledger entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "accounts.stp_acc_getretdistsupdistledgerbyorgid";
            var parameters = new DynamicParameters();
            parameters.Add("p_fromdate", Convert.ChangeType(entity.p_fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todate", Convert.ChangeType(entity.p_todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_orgid", entity.p_orgid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_orgtype", entity.p_orgtype, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);

            parameters.Add("p_count", entity.p_count, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_debit", entity.p_debit, DbType.VarNumeric, ParameterDirection.Input);
            parameters.Add("p_credit", entity.p_credit, DbType.VarNumeric, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<GetretailerledgerResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<GetretailerledgerResponse>> GetwithdrawalledgerDetails(cashwithdrawalledgerRequest entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "accounts.stp_acc_cashwithdrawal_ledger_type";
            var parameters = new DynamicParameters();
            parameters.Add("p_fromdate", Convert.ChangeType(entity.p_fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todate", Convert.ChangeType(entity.p_todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_orgid", entity.p_orgid, DbType.Int16, ParameterDirection.Input);

            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);

            parameters.Add("p_count", entity.p_count, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_debit", entity.p_debit, DbType.VarNumeric, ParameterDirection.Input);
            parameters.Add("p_credit", entity.p_credit, DbType.VarNumeric, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<GetretailerledgerResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<AllpaymentdetailsResponse>> GetAllpaymentdetails(Getallpaymentdetails entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.stp_acc_getallpaymentdetails";
            var parameters = new DynamicParameters();
            parameters.Add("p_orgid", entity.p_orgid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fromdt", Convert.ChangeType(entity.p_fromdt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todt", Convert.ChangeType(entity.p_todt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_status", entity.p_status, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<AllpaymentdetailsResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<GetPaymentHistoryResponse>> GetAllPaymentHistory(GetPaymentHistory entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.get_payments_detailsbyorgidanddate";
            var parameters = new DynamicParameters();
            parameters.Add("p_orgid", entity.p_orgid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fromdate", Convert.ChangeType(entity.p_fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todate", Convert.ChangeType(entity.p_todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<GetPaymentHistoryResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<GetcancelTxnResponse>> GetTxnCancelOrSsucsessDetails(GetcancelTxn entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.txn_cancelletion";
            var parameters = new DynamicParameters();
            parameters.Add("service_id", entity.service_id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_fromdt", Convert.ChangeType(entity.p_fromdt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todt", Convert.ChangeType(entity.p_todt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("txn_status", entity.txn_status, DbType.Int16, ParameterDirection.Input);
            //parameters.Add("org_id", entity.org_id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<GetcancelTxnResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<RetalierSalsAndCancelationResponse>> GetRetalierSalsAndCancelation(RetalierSalsAndCancelation entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.get_sales_canceletion_retailer";
            var parameters = new DynamicParameters();
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_serviceid", entity.p_serviceid, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_distributorid", entity.p_distributorid, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_fromdt", Convert.ChangeType(entity.p_fromdt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todt", Convert.ChangeType(entity.p_todt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("statusone", entity.statusone, DbType.Int16, ParameterDirection.Input);
            parameters.Add("statustwo", entity.statustwo, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<RetalierSalsAndCancelationResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<DistributerSalsAndCancelationResponse>> GetDistributerSalsAndCancelation(DistributerSalsAndCancelationModel entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.get_sales_canceletion_distributor";
            var parameters = new DynamicParameters();
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_service_id", entity.p_service_id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_fromdt", Convert.ChangeType(entity.p_fromdt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todt", Convert.ChangeType(entity.p_todt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("statusone", entity.statusone, DbType.Int16, ParameterDirection.Input);
            parameters.Add("statustwo", entity.statustwo, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<DistributerSalsAndCancelationResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<State>> getListofState(CancellationToken cancellationToken = default)
        {
            string query = @"select s.stateid, s.statename, s.statecode, s.countryid, tmc.conuntryname, s.status
            from common.tbl_mst_state s inner join common.tbl_mst_country tmc on tmc.countrycode = coalesce(CAST(s.countryid AS text), 'N/A')";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<State>(query);
                return result;
            }
        }
        public async Task<IEnumerable<ServiceDto>> getListofServices(CancellationToken cancellationToken = default)
        {
            string query = @"select serviceid as servicId, servicename  as serviceName, status  as status from servicemanagement.tbl_mst_service order by serviceid asc";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<ServiceDto>(query);
                return result;
            }
        }
        public async Task<IEnumerable<SupplierDto>> getListofSuppliers(int serviceId,CancellationToken cancellationToken = default)
        {
            var query = "select supplierid as supplierId ,suppliername as supplierName,status as status \r\nfrom servicemanagement.tbl_mst_suppliers  where serviceid = (CASE WHEN @serviceId > 0 THEN @serviceId ELSE serviceid END)";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<SupplierDto>(query, new { serviceId });
                return comissionDtls.ToList<SupplierDto>();
            }

        }

        public async Task<IEnumerable<ServiceProviderDto>> getListofServiceProviders(int serviceId, int supplierId, CancellationToken cancellationToken = default)
        {
            var query = "select supplierid as spId ,suppliername as spName,status as status \r\nfrom servicemanagement.tbl_mst_suppliers  where serviceid = (CASE WHEN @serviceId > 0 THEN @serviceId ELSE serviceid END) and supplierid  = (CASE WHEN @supplierid > 0 THEN @supplierid ELSE supplierid END)";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ServiceProviderDto>(query, new { serviceId, supplierId });
                return comissionDtls.ToList<ServiceProviderDto>();
            }

        }
        public async Task<IEnumerable<PaymentModeDto>> getListofPaymentMode(int? orgid, int? orgType, CancellationToken cancellationToken = default)
        {
            var query = "select id as paymentModeId , paymentMode ,status from payments.paymentMode ;";
                //"where serviceid = (CASE WHEN @serviceId > 0 THEN @serviceId ELSE serviceid END) and supplierid  = (CASE WHEN @supplierid > 0 THEN @supplierid ELSE supplierid END)";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<PaymentModeDto>(query, new { orgid, orgType });
                return comissionDtls.ToList<PaymentModeDto>();
            }

        }
        public async Task<IEnumerable<channelserviceSalesandCancelResponseModel>> GetchannelserviceSalsAndCancelation(ChannelParametersRequestModel entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.get_sales_canceletion_channelservice";
            var parameters = new DynamicParameters();
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_channelid", entity.p_channelid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_service_id", entity.p_service_id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_fromdt", Convert.ChangeType(entity.p_fromdt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todt", Convert.ChangeType(entity.p_todt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("statusone", entity.statusone, DbType.Int16, ParameterDirection.Input);
            parameters.Add("statustwo", entity.statustwo, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<channelserviceSalesandCancelResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<TransactionDetailsByTxnId> GetTransactionDetailsByTxnId(long p_txnid, CancellationToken cancellationToken = default)
        {
            var procedureName = "transaction.stp_txn_transactiondetailbytxnid";
            var parameters = new DynamicParameters();

            parameters.Add("p_txnid", p_txnid, DbType.Int64, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var response = await connection.QueryFirstOrDefaultAsync<TransactionDetailsByTxnId>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<IEnumerable<TransactionResponse>> ViewTxnsOfCPs(TransactionRequest entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "\"onboarding\".stp_chm_getCPtransactionBydistributor";
            var parameters = new DynamicParameters();
            parameters.Add("p_offsetrows", entity.offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.fetchrows, DbType.Int16, ParameterDirection.Input);
            if (string.IsNullOrEmpty(entity.fromdate))
            {
                parameters.Add("p_fromdate", DBNull.Value, DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", DBNull.Value, DbType.Date, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("p_fromdate", Convert.ChangeType(entity.fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", Convert.ChangeType(entity.todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            }
            parameters.Add("p_distributorid", entity.distributorid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_txnstatus", entity.txnstatus, DbType.Int16, ParameterDirection.Input);
            
            parameters.Add("p_txnid", entity.txnid, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_retailerorgid", entity.retailerorgid, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<TransactionResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }

        public async Task<IEnumerable<RetailerTxnReportResponse>> GetRetailerTxnReport(RetailerTxnReport entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "payments.getretailertxnreport";
            var parameters = new DynamicParameters();
            parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_orgcode", entity.p_retailercode, DbType.String, ParameterDirection.Input);
            parameters.Add("p_fromdt", Convert.ChangeType(entity.p_fromdt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_todt", Convert.ChangeType(entity.p_todt, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
            parameters.Add("p_status", entity.p_status, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryAsync<RetailerTxnReportResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }


        public async Task<IEnumerable<PurchaseOrderDetailsReport>> GetPurchaseOrderDetailsAsync(PurchaseOrderReportDto purchaseOrderReportDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "store.stp_purchase_order_history_report";
                var parameters = new DynamicParameters();

                parameters.Add("p_fromdate", string.IsNullOrEmpty(purchaseOrderReportDto.FromDate) ? null
                                :Convert.ChangeType(purchaseOrderReportDto.FromDate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", string.IsNullOrEmpty(purchaseOrderReportDto.ToDate) ? null
                                :Convert.ChangeType(purchaseOrderReportDto.ToDate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);

                parameters.Add("p_status", purchaseOrderReportDto.OrderStatus, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_offsetvalue", purchaseOrderReportDto.PageNumber, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", purchaseOrderReportDto.PageSize, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = await connection.QueryAsync<PurchaseOrderDetailsReport>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return reports;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderReportResponseModel>> GetOrderReportCP(OrderReportRequestModel entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "accounts.stp_acc_getorderdetails";
                var parameters = new DynamicParameters();

                parameters.Add("p_fromdate", string.IsNullOrEmpty(entity.p_fromdate) ? null : Convert.ChangeType(entity.p_fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", string.IsNullOrEmpty(entity.p_todate) ? null : Convert.ChangeType(entity.p_todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_orgcode", entity.p_orgcode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_status", entity.p_status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
                
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = await connection.QueryAsync<OrderReportResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return reports;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<InventoryReportResponseModel>> GetInventoryReport(InventoryReportRequestModel entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "accounts.stp_acc_getinventorydetails";
                var parameters = new DynamicParameters();

                parameters.Add("p_fromdate", string.IsNullOrEmpty(entity.p_fromdate) ? null : Convert.ChangeType(entity.p_fromdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", string.IsNullOrEmpty(entity.p_todate) ? null : Convert.ChangeType(entity.p_todate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);

                using(var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = await connection.QueryAsync<InventoryReportResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return reports;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<InventoryIdDetailsResponse>> GetInventoryIdDetails(int p_inventoryid, CancellationToken cancellationToken = default)
        {
            
                var procedureName = "accounts.stp_acc_getinventorydetailsbyid";
                var parameters = new DynamicParameters();
                parameters.Add("p_inventoryid", p_inventoryid, DbType.Int16, ParameterDirection.Input);

                using(var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = await connection.QueryAsync<InventoryIdDetailsResponse>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return reports;
                }         
        }
    }
}
