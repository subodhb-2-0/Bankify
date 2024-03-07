using Contracts.FundTransfer;
using Contracts.WorkingCapital;
using Dapper;
using Domain.Entities.Account;
using Domain.Entities.Common;
using Domain.Entities.Servicemanagement;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using static Dapper.SqlMapper;

namespace Persistence
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext _context;
        public AccountRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ViewLedger>> ViewLedgerAccount(CancellationToken cancellationToken = default)
        {
            var query = "select * from accounts.tbl_acc_accounts where status = 2";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ViewLedger>(query);
                return comissionDtls.ToList<ViewLedger>();
            }
        }

        public async Task<IEnumerable<ListOfOrgType>> GetListofOrgType(CancellationToken cancellationToken = default)
        {
            var query = "select * from onboarding.tbl_org_type";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListOfOrgType>(query);
                return comissionDtls.ToList<ListOfOrgType>();
            }
        }

        public async Task<CreateLedgerAccount> CreateLedgerAccount(CreateLedgerAccount entity, CancellationToken cancellationToken = default)
        {
            try
            {
                string query = @"INSERT INTO accounts.tbl_acc_accounts (orgtypeid, accid,accdescription , status,  creator, creationdate)
	                       VALUES ( @orgtypeid, @accid,@accdescription, @status, @creator, @creationdate)";
                var paramas = new { orgtypeid = entity.orgtypeid, accid = entity.accid, accdescription = entity.accdescription, status = 2, creator = 1, creationdate = DateTime.Now };

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(query, paramas);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new CreateLedgerAccount();
        }
        public async Task<UpdateLedgerAccount> UpdateLedgerAccount(UpdateLedgerAccount entity, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE accounts.tbl_acc_accounts SET orgtypeid=@orgtypeid,accid=@accid,accdescription=@accdescription,modifier=@modifier WHERE orgtypeaccid = @orgtypeaccid";
            var paramas = new { orgtypeid = entity.orgtypeid, accid = entity.accid, accdescription = entity.accdescription, modifier = entity.modifier };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<IEnumerable<ViewLedger>> GetLedgerAccount(int? Id, string? Value, CancellationToken cancellationToken = default)
        {
            //var query = "select tcc.orgid ,tcc.orgcode ,tcc.orgname ,tot.org_type  ,tuu.mobilenumber,tcc.status  from  onboarding.tbl_chm_channelpartners tcc,\r\nuser_management.tbl_um_users tuu , onboarding.tbl_org_type tot \r\nwhere tcc.orgid =tuu.orgid and tcc.orgtype =tot.org_type_id and tuu.reportsto is null and tcc.orgid = (CASE WHEN @Id > 0 THEN @Id ELSE tcc.orgid END) and tuu.mobilenumber = (CASE WHEN @Value <> '' THEN @Value ELSE tuu.mobilenumber END) order by tcc.orgid ";
            var query = "select * from accounts.tbl_acc_accounts where status = 2";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ViewLedger>(query, new { Id, Value });
                return comissionDtls.ToList<ViewLedger>();
            }
        }

        public async Task<IEnumerable<ListofAccType>> GetListofAccType(CancellationToken cancellationToken = default)
        {
            var query = "select orgtypeaccid,orgtypeid,accid,accdescription from accounts.tbl_acc_accounts where status = 2";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListofAccType>(query);
                return comissionDtls.ToList<ListofAccType>();
            }
        }
        public async Task<AccountResponseModel> AddJV(AddJV addJVDto, CancellationToken cancellationToken = default)
        {
            //AddJV addJV = new AddJV();
            //var procezreName = "\"DMT\".receiver_insert";
            try
            {

                var procedureName = "\"accounts\".stp_acc_addjv";
                var parameters = new DynamicParameters();
                parameters.Add("p_jv_name", addJVDto.jvname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_description", addJVDto.description, DbType.String, ParameterDirection.Input);
                parameters.Add("p_jv_date", DateTime.Now, DbType.Date, ParameterDirection.Input);
                parameters.Add("p_jvstatus", addJVDto.jvstatus, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_status", addJVDto.status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_creator", addJVDto.creator, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_corpid", addJVDto.creator, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<AccountResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
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
            return new AccountResponseModel();
        }

        public async Task<AccountResponseModel> AddJVDetails(JVDetails addJVDto, CancellationToken cancellationToken = default)
        {
            //JVDetails addJV = new JVDetails();
            try
            {
                var procedureName = "\"accounts\".stp_acc_addjvdetails";
                var parameters = new DynamicParameters();
                parameters.Add("p_jv_no", addJVDto.jvno, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_orgtype", addJVDto.orgtype, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_accid", addJVDto.accid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_orgid", addJVDto.orgid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_status", addJVDto.status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_amount", addJVDto.amount, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_debitcredit", addJVDto.debitcredit, DbType.String, ParameterDirection.Input);
                parameters.Add("p_narration", addJVDto.narration, DbType.String, ParameterDirection.Input);
                parameters.Add("p_creator", addJVDto.creator, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<AccountResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
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
            return new AccountResponseModel();
        }

        public async Task<UpdateJVDetail> UpdateJVDetailsId(UpdateJVDetail entity, CancellationToken cancellationToken = default)
        {
            //var query = "UPDATE accounts.tbl_acc_accounts SET orgtypeid=@orgtypeid,accid=@accid,accdescription=@accdescription,modifier=@modifier WHERE orgtypeaccid = @orgtypeaccid";
            var query = "update accounts.tbl_acc_jvdetails set status  = 2 , modifier = @modifier , modificationdate =@modificationdate  where jvdetailsid  = @jvdetailsid";
            var paramas = new { status = entity.status, jvdetailsid = entity.jvdetailsid, modifier = entity.ModifierBy, modificationdate = DateTime.Now };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }
        public async Task<UpdateJVDetail> RemoveJV(UpdateJVDetail entity, CancellationToken cancellationToken = default)
        {
            try
            {
                //var query = "update accounts.tbl_acc_jvdetails set status  = 3 where jvdetailsid  = @jvdetailsid";
                var query = "update accounts.tbl_acc_jvdetails set status  = 4 , modifier = @modifier , modificationdate =@modificationdate where jvdetailsid  = @jvdetailsid";
                var paramas = new { status = entity.status, jvdetailsid = entity.jvdetailsid, modifier = entity.ModifierBy, modificationdate = DateTime.Now };
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(query, paramas);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new UpdateJVDetail();

        }
        public async Task<ApproveRejectJV> ApproveRejectJV(ApproveRejectJV entity, CancellationToken cancellationToken = default)
        {
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendLine($"DO $$");
            commandBuilder.AppendLine($"BEGIN ");
            commandBuilder.AppendLine($"update accounts.tbl_acc_JV set status= {entity.status} , modificationdate= NOW() where jvno={entity.jvno}  ;");
            commandBuilder.AppendLine($"update accounts.tbl_acc_jvdetails set status= {entity.status} , modificationdate= NOW() where jvno={entity.jvno} and status != 4 ;");
            commandBuilder.AppendLine($" END$$");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(commandBuilder.ToString());
                result = 1;
                return entity;
            }
        }

        public async Task<AccountResponseModel> ApproveAndRejectJV(ApproveRejectJV addJVDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"accounts\".stp_acc_approverejectjv";
                var parameters = new DynamicParameters();
                parameters.Add("p_jv_no", addJVDto.jvno, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_jvstatus", addJVDto.status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_modifier", addJVDto.modifier, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_currentstatus", addJVDto.currentstatus, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<AccountResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
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
            return new AccountResponseModel();
        }
        public async Task<Tuple<IEnumerable<AccountPayment>, int>> GetListofWCRequest(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            pageNumber = pageNumber ?? 1;
            StringBuilder queryBuilder = new();
            queryBuilder.AppendLine("Select Count(1) from payments.tbl_acc_payments pa,\r\nonboarding.tbl_chm_channelpartners tcc\r\n, onboarding.tbl_org_type tot,common.tbl_mst_bank tmb  where pa.orgid = tcc.orgid and pa.bankid =tmb.bankid and tcc.orgtype = tot.org_type_id and pa.paymentmode != 1;");
            queryBuilder.AppendLine(" select pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,pa.bankaccount as AccountNumber,pa.paymentmode,pa.instrumentnumber,pa.status, pa.creationdate AT TIME ZONE 'UTC' AT TIME ZONE 'Asia/Kolkata' as creationdate, tmb.bankname DepositingBank,pa.bankpayinslip SlipNarration,\r\n  pa.modificationdate AT TIME ZONE 'UTC' AT TIME ZONE 'Asia/Kolkata' as ActionDateTime, pa.remark Narration,pa.bank_ref_no BankRefNo,pa.issuingifsccode IFSCCode from \r\npayments.tbl_acc_payments pa,onboarding.tbl_chm_channelpartners tcc ,onboarding.tbl_org_type tot ,common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid \r\nand pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode != 1 ");

            //orderByColumn2
            queryBuilder.AppendFormat(" Order By {0} ", orderByColumn ?? " paymentid ");

            //orderBy
            queryBuilder.AppendFormat(" {0} ", orderBy ?? " asc ");

            if (pageSize != null && pageSize != 0)
            {
                int? limit = pageSize;
                int? offset = (pageNumber - 1) * pageSize;
                queryBuilder.AppendFormat("limit {0} offset {1}", limit, offset);
            }
            var query=queryBuilder.ToString();

            //var query = string.Format("Select Count(1) from payments.tbl_acc_payments pa,\r\nonboarding.tbl_chm_channelpartners tcc\r\n,onboarding.tbl_org_type tot,common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid and pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode != 1;" +
            // " select pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,pa.paymentmode,pa.instrumentnumber,pa.status, pa.creationdate, tmb.bankname DepositingBank, pa.remark Narration,pa.bank_ref_no BankRefNo,pa.issuingifsccode IFSCCode from \r\npayments.tbl_acc_payments pa,onboarding.tbl_chm_channelpartners tcc ,onboarding.tbl_org_type tot ,common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid \r\nand pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode != 1 " +
            // //"and pa.instrumentnumber = (CASE WHEN {5} <> '' THEN {5} ELSE pa.instrumentnumber END) " +
            // "order by pa.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<AccountPayment>().ToList();
                    return new Tuple<IEnumerable<AccountPayment>, int>(users.ToList<AccountPayment>(), count);
                }
            }
        }
        public async Task<Tuple<IEnumerable<AccountPayment>, int>> ViewBankClaimDeposits(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            //var query = "select pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,pa.paymentmode,pa.instrumentnumber,pa.status \r\nfrom payments.tbl_acc_payments pa,\r\nonboarding.tbl_chm_channelpartners tcc\r\n,onboarding.tbl_org_type tot where pa.orgid  = tcc.orgid and tcc.orgtype  = tot.org_type_id and pa.paymentmode = 3 and  pa.status = 1 and pa.paymentid = (CASE WHEN @Id > 0 THEN @Id ELSE pa.paymentid END) and pa.instrumentnumber = (CASE WHEN @Value <> '' THEN @Value ELSE pa.instrumentnumber END)";
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //string imgUrl = configurationBuilder.GetSection("ImagePath")["ConsoleUrl"];
            string imgUrl = configurationBuilder.GetSection("ImagePath")["URL"];
            imgUrl = imgUrl + "Uploads/UPIPayments/";

            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Select Count(1) from payments.tbl_acc_payments pa,\r\nonboarding.tbl_chm_channelpartners tcc\r\n,onboarding.tbl_org_type tot,common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid and pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode = 1 ;");
            //stringBuilder.AppendLine("Select pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,pa.paymentmode,pa.instrumentnumber,pa.status,pa.creationdate, tmb.bankname DepositingBank, pa.remark Narration, concat('" + imgUrl + "', pa.bankpayinslip) SlipNarration,pa.bankaccount AccountNumber,\r\n  pa.modificationdate ActionDateTime,pa.bank_ref_no BankRefNo,pa.issuingifsccode IFSCCode from \r\npayments.tbl_acc_payments pa,onboarding.tbl_chm_channelpartners tcc ,onboarding.tbl_org_type tot, common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid \r\nand pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode = 1 ");
            stringBuilder.AppendLine("Select pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,pa.paymentmode,pa.instrumentnumber,pa.status,  pa.creationdate AT TIME ZONE 'UTC' AT TIME ZONE 'Asia/Kolkata' as creationdate  , tmb.bankname DepositingBank, pa.remark Narration, concat('" + imgUrl + "', pa.bankpayinslip) SlipNarration,pa.bankaccount AccountNumber,\r\n  pa.modificationdate AT TIME ZONE 'UTC' AT TIME ZONE 'Asia/Kolkata' as ActionDateTime,pa.bank_ref_no BankRefNo,pa.issuingifsccode IFSCCode from \r\npayments.tbl_acc_payments pa,onboarding.tbl_chm_channelpartners tcc ,onboarding.tbl_org_type tot, common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid \r\nand pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode = 1 ");

            //orderByColumn
            stringBuilder.AppendFormat(" Order By pa.{0} ", orderByColumn ?? " orgid ");

            //orderBy
            stringBuilder.AppendFormat(" {0} ", orderBy ?? " asc ");

            if (pageSize != null && pageSize != 0)
            {
                int? limit = pageSize;
                int? offset = (pageNumber - 1) * pageSize;
                stringBuilder.AppendFormat("limit {0} offset {1}", limit, offset);
            }
            var query = stringBuilder.ToString();


            //var query = string.Format("Select Count(1) from payments.tbl_acc_payments pa,\r\nonboarding.tbl_chm_channelpartners tcc\r\n,onboarding.tbl_org_type tot,common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid and pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode = 1 ;" +
            //" select pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,pa.paymentmode,pa.instrumentnumber,pa.status,pa.creationdate, tmb.bankname DepositingBank, pa.remark Narration, concat('" + imgUrl + "', pa.bankpayinslip) SlipNarration,pa.bankaccount AccountNumber,\r\n  pa.modificationdate ActionDateTime,pa.bank_ref_no BankRefNo,pa.issuingifsccode IFSCCode from \r\npayments.tbl_acc_payments pa,onboarding.tbl_chm_channelpartners tcc ,onboarding.tbl_org_type tot, common.tbl_mst_bank tmb where pa.orgid  = tcc.orgid \r\nand pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode = 1 " +
            // //"and pa.instrumentnumber = (CASE WHEN {5} <> '' THEN {5} ELSE pa.instrumentnumber END) " +
            // "order by pa.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<AccountPayment>().ToList();
                    return new Tuple<IEnumerable<AccountPayment>, int>(users.ToList<AccountPayment>(), count);
                }
            }
        }

        public async Task<IEnumerable<AccountPayment>> ViewBankClaimDepositsWithFiltter(int paymentmode, int status, string? orgCode, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            StringBuilder queryBuilder = new StringBuilder();
            try
            {
                queryBuilder.Append("select Count(1) over() as totalrecord, pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,\r\n pa.paymentmode,pa.instrumentnumber,pa.status,pa.creationdate, tmb.bankname DepositingBank,\r\n pa.remark Narration, pa.bankpayinslip SlipNarration,pa.bankaccount AccountNumber,\r\n pa.modificationdate ActionDateTime, issuingifsccode IFSCCode, pa.vpa UPI\r\n from onboarding.tbl_chm_channelpartners tcc \r\n inner join payments.tbl_acc_payments pa on tcc.orgid = pa.orgid \r\n left join onboarding.tbl_org_type tot on tcc.orgtype = tot.org_type_id \r\n left join  common.tbl_mst_bank tmb on pa.bankid = tmb.bankid \r\n where  pa.paymentmode not in (5,7)");
                
                if(paymentmode > 0) queryBuilder.Append($" AND pa.paymentmode = {paymentmode} ");
                if(status > 0) queryBuilder.Append($" AND pa.status = {status} ");
                if (!string.IsNullOrEmpty(orgCode)) queryBuilder.Append($" AND tcc.orgcode = '{orgCode}' ");
                if(fromDate != DateTime.MinValue) queryBuilder.Append($" AND pa.paymentdate  >= '{fromDate:yyyy-MM-dd}'::date ");
                if(toDate != DateTime.MinValue) queryBuilder.Append($" AND pa.paymentdate  <= '{toDate:yyyy-MM-dd}'::date ");

                queryBuilder.Append(" ORDER BY  pa.paymentid DESC ");
                if (p_fetchrows > 0 && p_offsetrows != null)
                {
                    int? limit = p_fetchrows;
                    int? offset = (p_offsetrows - 1) * p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }

                string query = queryBuilder.ToString();
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<AccountPayment>(query);
                    return (IEnumerable<AccountPayment>)TxnHistory.ToList();
                }
            }
            catch(Exception ex)
            {
                throw;
            }


          /*  StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select Count(1) over() as totalrecord, pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,\r\n pa.paymentmode,pa.instrumentnumber,pa.status,pa.creationdate, tmb.bankname DepositingBank,\r\n pa.remark Narration, pa.bankpayinslip SlipNarration,pa.bankaccount AccountNumber,\r\n pa.modificationdate ActionDateTime,\r\nissuingifsccode IFSCCode,\r\n pa.vpa UPI from\r\n payments.tbl_acc_payments pa,onboarding.tbl_chm_channelpartners tcc ,\r\n onboarding.tbl_org_type tot, common.tbl_mst_bank tmb");
            queryBuilder.Append(" where pa.orgid  = tcc.orgid\r\n and pa.bankid =tmb.bankid and tcc.orgtype  = tot.org_type_id and pa.paymentmode not in (5,7) ");
            if (paymentmode > 0)
            {
                queryBuilder.Append($" AND pa.paymentmode = {paymentmode} ");
            }
            if (status > 0)
            {
                queryBuilder.Append($" AND pa.status = {status} ");
            }
            if (orgCode != null)
            {
                queryBuilder.Append($" AND tcc.orgcode = '{orgCode}' ");
            }
            if (fromDate != DateTime.MinValue)
            {
                queryBuilder.Append($" AND pa.paymentdate  >= '{fromDate:yyyy-MM-dd}'::date ");

            }
            if (toDate != DateTime.MinValue)
            {
                queryBuilder.Append($" AND pa.paymentdate  <= '{toDate:yyyy-MM-dd}'::date ");
            }
            queryBuilder.Append(" ORDER BY  pa.paymentid DESC ");
            if (p_fetchrows > 0 && p_offsetrows != null)
            {
                int? limit = p_fetchrows;
                int? offset = (p_offsetrows - 1) * p_fetchrows;
                //queryBuilder.Append($" ORDER BY  pa.paymentid asc OFFSET {p_offsetrows}  LIMIT {p_fetchrows} ");
                queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
            }
            string query = queryBuilder.ToString();
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var TxnHistory = await dbConnection.QueryAsync<AccountPayment>(query);
                return (IEnumerable<AccountPayment>)TxnHistory.ToList();
            }*/
        }

        public async Task<ApproveRejectWC> ApproveRejectWC(ApproveRejectWC entity, CancellationToken cancellationToken = default)
        {
            //var query = "update payments.tbl_acc_payments set status  = @status where paymentid  = @paymentid";
            //var paramas = new { status = entity.status, paymentid = entity.paymentid };
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var result = await dbConnection.ExecuteAsync(query, paramas);
            //    return entity;
            //}
            try
            {
                var procedureName = "\"payments\".stp_acc_updatepaymenttopupcash";
                var parameters = new DynamicParameters();
                parameters.Add("p_paymentid", entity.paymentid, DbType.Int64, ParameterDirection.Input);
                parameters.Add("p_depostat", entity.status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_userid", entity.userid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_remarks", entity.remark, DbType.String, ParameterDirection.Input);
                parameters.Add("p_instrumentid", "", DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    //var result1 = await connection.QueryFirstOrDefaultAsync<AccountResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    var result = await connection.QueryFirstOrDefaultAsync<ApproveRejectWC>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {

            }
            return new ApproveRejectWC();


        }
        Task<ViewLedger> IGenericRepository<ViewLedger>.AddAsync(ViewLedger entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ViewLedger> IGenericRepository<ViewLedger>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ViewLedger>> IGenericRepository<ViewLedger>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ViewLedger> IGenericRepository<ViewLedger>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ViewLedger> IGenericRepository<ViewLedger>.UpdateAsync(ViewLedger entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JVDetailsList>> GetListofJVDetailsByJVNo(int? jvno, CancellationToken cancellationToken = default)
        {
            //int limit = pageSize;
            //int offset = (pageNumber - 1) * pageSize;
            //var query = string.Format("Select Count(1) from accounts.tbl_acc_jv taj , accounts.tbl_acc_jvdetails taj2 ,onboarding.tbl_org_type tot ,accounts.tbl_acc_accounts taa ,\r\nonboarding.tbl_chm_channelpartners tcc where\r\ntaj.jvno =taj2.jvno and \r\ntaj2.orgtype = tot.org_type_id and \r\ntaj2.accid =taa.accid and \r\ntaj2.orgid = tcc.orgid and\r\ntaj2.orgtype = taa.orgtypeid;" +
            //    " select taj2.jvdetailsid, taj.jvno ,jvname ,description ,jvdate ,taj.status ,orgTypeId, tot.org_type ,taj2.accid accountId,taa.accdescription accountType,\r\ntaj2.orgId,orgName,taj2.amount, taj2.debitcredit JVType,taj2.status jvdtlsStatus, taj2.narration ,taj2.creationdate \r\nfrom accounts.tbl_acc_jv taj , accounts.tbl_acc_jvdetails taj2 ,onboarding.tbl_org_type tot ,accounts.tbl_acc_accounts taa ,\r\nonboarding.tbl_chm_channelpartners tcc where\r\ntaj.jvno =taj2.jvno and \r\ntaj2.orgtype = tot.org_type_id and \r\ntaj2.accid =taa.accid and \r\ntaj2.orgid = tcc.orgid and\r\ntaj2.orgtype = taa.orgtypeid " +
            //     "order by taj.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    using (var multi = await dbConnection.QueryMultipleAsync(query))
            //    {
            //        var count = multi.Read<int>().Single();
            //        var users = multi.Read<JVDetailsList>().ToList();
            //        return new Tuple<IEnumerable<JVDetailsList>, int>(users.ToList<JVDetailsList>(), count);
            //    }
            //}


            var query = "select taj2.jvdetailsid, taj.jvno ,jvname ,description ,jvdate ,taj.status ,orgTypeId, tot.org_type ,taj2.accid accountId,taa.accdescription accountType,\r\ntaj2.orgId,orgName,taj2.amount, taj2.debitcredit JVType,taj2.status jvdtlsStatus, taj2.narration ,taj2.creationdate \r\nfrom accounts.tbl_acc_jv taj , accounts.tbl_acc_jvdetails taj2 ,onboarding.tbl_org_type tot ,accounts.tbl_acc_accounts taa ,\r\nonboarding.tbl_chm_channelpartners tcc where\r\ntaj.jvno =taj2.jvno and \r\ntaj2.orgtype = tot.org_type_id and \r\ntaj2.accid =taa.accid and \r\ntaj2.orgid = tcc.orgid and\r\ntaj2.orgtype = taa.orgtypeid and taj2.status in (0,1,2,3) and taj.jvno = (CASE WHEN @jvno > 0 THEN @jvno ELSE taj.jvno END ) ORDER BY taj2.creationdate DESC";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<JVDetailsList>(query, new { jvno });
                return comissionDtls.ToList<JVDetailsList>();
            }

        }

        public async Task<Tuple<IEnumerable<JVDetailsList>, int>> GetListofJVDetails(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default)
        {
            int limit = pageSize;
            int offset = (pageNumber - 1) * pageSize;
            var query = string.Format("Select Count(1) from accounts.tbl_acc_jv taj , accounts.tbl_acc_jvdetails taj2 ,onboarding.tbl_org_type tot ,accounts.tbl_acc_accounts taa ,\r\nonboarding.tbl_chm_channelpartners tcc where\r\ntaj.jvno =taj2.jvno and \r\ntaj2.orgtype = tot.org_type_id and \r\ntaj2.accid =taa.accid and \r\ntaj2.orgid = tcc.orgid and\r\ntaj2.orgtype = taa.orgtypeid and taj2.status in (0,1,2,3);" +
                " select taj2.jvdetailsid, taj.jvno ,jvname ,description ,jvdate ,taj.status ,orgTypeId, tot.org_type ,taj2.accid accountId,taa.accdescription accountType,\r\ntaj2.orgId,orgName,taj2.amount, taj2.debitcredit JVType,taj2.status jvdtlsStatus, taj2.narration ,taj2.creationdate \r\nfrom accounts.tbl_acc_jv taj , accounts.tbl_acc_jvdetails taj2 ,onboarding.tbl_org_type tot ,accounts.tbl_acc_accounts taa ,\r\nonboarding.tbl_chm_channelpartners tcc where\r\ntaj.jvno =taj2.jvno and \r\ntaj2.orgtype = tot.org_type_id and \r\ntaj2.accid =taa.accid and \r\ntaj2.orgid = tcc.orgid and\r\ntaj2.orgtype = taa.orgtypeid and taj2.status in (0,1,2,3) " +
                 "order by taj.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<JVDetailsList>().ToList();
                    return new Tuple<IEnumerable<JVDetailsList>, int>(users.ToList<JVDetailsList>(), count);
                }
            }
        }

        public async Task<IEnumerable<ListofAccType>> GetListAccByOrgeTypeId(int OrgTypeId, CancellationToken cancellationToken = default)
        {
            var query = "select orgtypeaccid,orgtypeid,accid,accdescription from accounts.tbl_acc_accounts where status = 2  and orgtypeid = (CASE WHEN @OrgTypeId > 0 THEN @OrgTypeId ELSE orgtypeid END) ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListofAccType>(query, new { OrgTypeId });
                return comissionDtls.ToList<ListofAccType>();
            }
        }

        public async Task<WCInitiatePaymentResponseModel> InititatePayment(WCInitiatePaymentDto paymentInModel, CancellationToken cancellationToken = default)
        {
            try
            {
                string depositdateTime = paymentInModel.p_depositDate + " " + paymentInModel.p_depositTime;
                var procedureName = "\"payments\".func_acc_addpayments"; //"\"payments\".func_acc_addpayments";
                var parameters = new DynamicParameters();
                parameters.Add("p_orgid", paymentInModel.p_orgid, DbType.Int64, ParameterDirection.Input);
                parameters.Add("p_paymentmode", paymentInModel.p_paymentmode, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_amount", paymentInModel.p_amount, DbType.VarNumeric, ParameterDirection.Input);
                parameters.Add("p_bankid", paymentInModel.p_bankid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_pgid", paymentInModel.p_pgid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_bankaccount", paymentInModel.p_bankaccount, DbType.String, ParameterDirection.Input);
                parameters.Add("p_status", paymentInModel.p_status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_remark", paymentInModel.p_remark, DbType.String, ParameterDirection.Input);
                parameters.Add("p_issueingbankid", paymentInModel.p_issueingbankid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_creator", paymentInModel.p_creator, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_bankpayinslip", paymentInModel.p_bankpayinslip, DbType.String, ParameterDirection.Input);
                parameters.Add("p_instrumentnumber", paymentInModel.p_instrumentnumber, DbType.String, ParameterDirection.Input);
                parameters.Add("p_issuingifsccode", paymentInModel.p_issuingifsccode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_vpa", paymentInModel.p_vpa, DbType.String, ParameterDirection.Input);
                parameters.Add("p_depositdate", depositdateTime, DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<WCInitiatePaymentResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new WCInitiatePaymentResponseModel();
        }

        public async Task<UpdateJVDetail> UpdateJVByJVDetailID(UpdateJVDetail entity, CancellationToken cancellationToken = default)
        {
            var query = "update accounts.tbl_acc_jvdetails set status  = @status , modifier = @modifier , modificationdate =@modificationdate where jvdetailsid  = @jvdetailsid";
            var paramas = new { status = entity.status, jvdetailsid = entity.jvdetailsid, modifier = entity.ModifierBy, modificationdate = DateTime.Now };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<Tuple<IEnumerable<JVInfoList>, int>> GetListofJV(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("select Count(1) from accounts.tbl_acc_jv taj ");
            if (!string.IsNullOrEmpty(searchBy))
            {
                stringBuilder.Append($" WHERE\r\n  LOWER(CAST(taj.jvno AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n OR  LOWER(CAST(taj.jvname AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(taj.description AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.jvdate AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.status AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.jvstatus AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.orgid AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n");
            }
            stringBuilder.AppendLine("; select * from accounts.tbl_acc_jv taj ");
            if (!string.IsNullOrEmpty(searchBy))
            {
                stringBuilder.Append($" WHERE\r\n  LOWER(CAST(taj.jvno AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n OR  LOWER(CAST(taj.jvname AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n  OR LOWER(CAST(taj.description AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.jvdate AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.status AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.jvstatus AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n   OR LOWER(CAST(taj.orgid AS text)) LIKE LOWER('%'||'{searchBy}'||'%')\r\n");
            }
            stringBuilder.AppendFormat(" order by taj.{0} ", orderByColumn ?? "jvno");
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
                    var users = multi.Read<JVInfoList>().ToList();
                    return new Tuple<IEnumerable<JVInfoList>, int>(users.ToList<JVInfoList>(), count);
                }
            }
        }

        public async Task<IEnumerable<WCPaymentResponseDTO>> CheckWCPayments(WCPaymentRequestDTO request, CancellationToken cancellationToken = default)
        {
            //var query = "select tcc.orgid as orgId,tcc.orgname as orgName ,tap.paymentmode as paymentMode,tap.amount as amount,\r\ntap.paymentdate paymentDate,tap.status as status,tap.bankpayinslip as bankPayInSlip,\r\ntap.instrumentnumber, tap.servicecharge as seviceCharge , tap.pgcharge ,tap.transferbyorgid ,tap.deposittime,tap.vpa ,tap.remark as remarks \r\nfrom  onboarding.tbl_chm_channelpartners tcc\r\nleft join payments.tbl_acc_payments tap on tcc.orgid = tap.orgid \r\nwhere tcc.orgid = (CASE WHEN @orgid > 0 THEN @orgid ELSE tcc.orgid END) ";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var comissionDtls = await dbConnection.QueryAsync<WCPaymentResponseDTO>(query, new { orgid =  request.orgId });
            //    return comissionDtls.ToList<WCPaymentResponseDTO>();
            //}

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select tcc.orgid as orgId,tcc.orgname as orgName ,tap.paymentmode as paymentMode,tap.amount as amount,\r\ntap.paymentdate paymentDate,tap.status as status,tap.bankpayinslip as bankPayInSlip,\r\ntap.instrumentnumber, tap.servicecharge as seviceCharge , tap.pgcharge ,tap.transferbyorgid ,tap.deposittime,tap.vpa ,tap.remark as remarks \r\nfrom  onboarding.tbl_chm_channelpartners tcc\r\nleft join payments.tbl_acc_payments tap on tcc.orgid = tap.orgid ");
            queryBuilder.Append(" where 1=1 ");
            if (request.orgId > 0)
            {
                queryBuilder.Append($" AND tcc.orgid = {request.orgId} ");
            }
            if (request.searchById > 0)
            {
                queryBuilder.Append($" AND tcc.orgid = {request.searchById} ");
                // queryBuilder.Append($" AND LOWER(orgName) like LOWER('%{orgName}%') ");
            }
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var cpdetails = await dbConnection.QueryAsync<WCPaymentResponseDTO>(queryBuilder.ToString());
                return (IEnumerable<WCPaymentResponseDTO>)cpdetails;
            }
        }

        public async Task<IEnumerable<OrgDetailDto>> GetOrgDetails(int orgId, CancellationToken cancellationToken = default)
        {
            var query = "select tcc.orgid as orgId,tcc.orgcode as orgCode,tcc.orgname as orgName,taw.openingbal as orgLimit,taw.runningbalance as walletBalance from onboarding.tbl_chm_channelpartners tcc \r\nleft join accounts.tbl_acc_wallet taw on tcc.orgid  = taw.orgid \r\nwhere tcc.orgid  = (CASE WHEN @orgid > 0 THEN @orgid ELSE tcc.orgid END) ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<OrgDetailDto>(query, new { orgid = orgId });
                return comissionDtls.ToList<OrgDetailDto>();
            }
        }
        public async Task<IEnumerable<ChannelPartnerDetailDto>> GetListOfChannelPartner(int distributororgid, int retailerorgid, CancellationToken cancellationToken = default)
        {
            //var query = "select tcc.orgid as orgId,tcc.orgname as orgName,tcc.orgcode as orgCode,tcc.status as status,\r\nconcat(toc.busi_house_no  ,toc.busi_road) as bussinessAddress,toc.busi_pincode as pinCode,toc.busi_sub_district  as city,\r\ntoc.busi_district  as state,'' as latlong\r\nfrom onboarding.tbl_chm_channelpartners tcc \r\nleft join onboarding.tbl_org_cpdetails toc  on tcc.orgid  = toc.orgid \r\nwhere tcc.orgid  = (CASE WHEN @orgid > 0 THEN @orgid ELSE tcc.orgid END) and tcc.orgid  = (CASE WHEN @orgid > 0 THEN @orgid ELSE tcc.orgid END) and tcc.orgtype  = (CASE WHEN @orgtype > 0 THEN @orgtype ELSE tcc.orgtype END) ";
            //var query = "select tcc.orgid as orgId,tcc.orgname as orgName,tcc.orgcode as orgCode,tcc.status as status,concat(toc.busi_house_no ,toc.busi_road)\r\nas bussinessAddress,toc.busi_pincode as pinCode,toc.busi_sub_district as city,toc.busi_district as state,'' as latlong,\r\ntaw.runningbalance as retailerbalance\r\nfrom onboarding.tbl_chm_channelpartners tcc inner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid\r\ninner join onboarding.tbl_chm_channelpartners Rettcc on Rettcc.parentorgid =tcc.orgid\r\ninner join accounts.tbl_acc_wallet taw on taw.orgid =Rettcc.orgid\r\nwhere tcc.orgid = (CASE WHEN @orgid > 0 THEN @orgid ELSE tcc.orgid END) and tcc.orgtype = (CASE WHEN @orgtype > 0 THEN @orgtype ELSE tcc.orgtype END) and Rettcc.orgid=3 ";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var comissionDtls = await dbConnection.QueryAsync<ChannelPartnerDetailDto>(query, new { orgid = orgId, orgtype = orgType });
            //    return comissionDtls.ToList<ChannelPartnerDetailDto>();
            //}
            List<ChannelPartnerDetailDto> slist = new List<ChannelPartnerDetailDto>();

            var procedureName = "\"onboarding\".stp_chm_getCPbalanceBydistributor";
            var parameters = new DynamicParameters();
            parameters.Add("p_distributorid", distributororgid, DbType.Int16, ParameterDirection.Input);
            parameters.Add("p_retailerorgid", retailerorgid, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<ChannelPartnerDetailDto>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (result != null)
                {
                    return result.ToList<ChannelPartnerDetailDto>();
                }
            }
            return slist;
        }

        public async Task<IEnumerable<OrgDetailsDto>> GetOrgDetail(CancellationToken cancellationToken = default)
        {
            var query = "Select tcc.orgname ,tcc.orgid  from onboarding.tbl_chm_channelpartners tcc where orgtype =2 and status in (2,4); ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<OrgDetailsDto>(query);
                return comissionDtls.ToList<OrgDetailsDto>();
            }
        }

        public async Task<IEnumerable<AccountDetailsByOrgID>> GetAccountDetailsByOrgID(int Offsetrows, int Fetchrows, int OrgtypeId, string? Accdescription, CancellationToken cancellationToken = default)
        {
            //var query = "select taj2.jvdetailsid, taj.jvno ,jvname ,description ,jvdate ,taj.status ,orgTypeId, tot.org_type ,taj2.accid accountId,taa.accdescription accountType,\r\ntaj2.orgId,orgName,taj2.amount, taj2.debitcredit JVType,taj2.status jvdtlsStatus, taj2.narration ,taj2.creationdate \r\nfrom accounts.tbl_acc_jv taj , accounts.tbl_acc_jvdetails taj2 ,onboarding.tbl_org_type tot ,accounts.tbl_acc_accounts taa ,\r\nonboarding.tbl_chm_channelpartners tcc where\r\ntaj.jvno =taj2.jvno and \r\ntaj2.orgtype = tot.org_type_id and \r\ntaj2.accid =taa.accid and \r\ntaj2.orgid = tcc.orgid and\r\ntaj2.orgtype = taa.orgtypeid and taj2.status in (0,1,2,3) and taj.jvno = (CASE WHEN @jvno > 0 THEN @jvno ELSE taj.jvno END) ";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var comissionDtls = await dbConnection.QueryAsync<AccountDetailsByOrgID>(query, new { Offsetrows });
            //    return comissionDtls.ToList<AccountDetailsByOrgID>();
            //}
            List<AccountDetailsByOrgID> slist = new List<AccountDetailsByOrgID>();
            try
            {
                var procedureName = "\"accounts\".stp_acc_getaccountdetails";
                var parameters = new DynamicParameters();
                parameters.Add("p_offsetrows", Offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", Fetchrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_orgtype", OrgtypeId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_accdescription", Accdescription == null ? "" : Accdescription, DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<AccountDetailsByOrgID>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<AccountDetailsByOrgID>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<AccountResponseModel> AddAccount(AddAccount addAccDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"accounts\".stp_acc_addaccounts";
                var parameters = new DynamicParameters();
                parameters.Add("p_orgtypeid", addAccDto.OrgtypeId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_accdescription", addAccDto.Accdescription, DbType.String, ParameterDirection.Input);
                parameters.Add("p_status", addAccDto.status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_creator", addAccDto.creator, DbType.Int16, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<AccountResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
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
            return new AccountResponseModel();
        }

        public async Task<IEnumerable<LedgerDetailsById>> GetAllLedgerDetailsById(int transactionid, string code, CancellationToken cancellationToken = default)
        {
            List<LedgerDetailsById> slist = new List<LedgerDetailsById>();
            try
            {
                var procedureName = "\"accounts\".stp_txn_getalledgerdetailsbyid";
                var parameters = new DynamicParameters();
                parameters.Add("p_transactionid", transactionid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_code", code == null ? "" : code, DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<LedgerDetailsById>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<LedgerDetailsById>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<SevenPayBankList>> GetSevenPayBankList(long orgid, CancellationToken cancellationToken = default)
        {
            var query = "select bankid ,bankaccnumber ,remarks ,status  from common.tbl_mst_sevenpaybank where orgid=@orgid; ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<SevenPayBankList>(query, new { orgid = orgid });
                return comissionDtls.ToList<SevenPayBankList>();
            }
        }

        public async Task<BankIFSCModel> GetBanksInfoIFSCCode(string ifscCode, CancellationToken cancellationToken = default)
        {
            var query = "SELECT ifsc_code, bankid, micr_code, branch_name, address, contact, district, state, ifsc_enabled, bankname, status FROM common.tbl_mst_ifsc where ifsc_code = @ifscCode; ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryFirstOrDefaultAsync<BankIFSCModel>(query, new { ifscCode = ifscCode });
                return result;
            }
        }

        public async Task<RetailInventoryResponseModel> InitiateFundTransfer(FundTransferRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "accounts.stp_acc_distributorbalancetransfer";
                var parameters = new DynamicParameters();
                parameters.Add("p_orgid", request.orgid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_paymentmode", request.paymentmode, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_amount", request.amount, DbType.VarNumeric, ParameterDirection.Input);
                parameters.Add("p_depositdate", Convert.ChangeType(request.depositdate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                
                parameters.Add("p_status", request.status, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_remark", request.remark, DbType.String, ParameterDirection.Input);
                parameters.Add("p_creator", request.creator, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_transferbyorgid", request.transferbyorgid, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<RetailInventoryResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
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
            return new RetailInventoryResponseModel();
        }
        public async Task<PaymentInResponseModel> UPIUpdatePayment(UPIPostTransactionRequestModel upiPostTransactionRequestModel, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModel();
            var result = new PaymentInResponseModel();



            try
            {
                if (upiPostTransactionRequestModel.MethodType == "payIn")
                {
                    var procedureName = "\"payments\".stp_acc_updatepaymenttopupupi";
                    var parameters = new DynamicParameters();

                    parameters.Add("p_paymentid", upiPostTransactionRequestModel.p_paymentid, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("p_depostat", upiPostTransactionRequestModel.p_depostat, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("p_userid", upiPostTransactionRequestModel.p_userid, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("p_remarks", upiPostTransactionRequestModel.p_remarks, DbType.String, ParameterDirection.Input);
                    parameters.Add("p_instrumentid", upiPostTransactionRequestModel.p_instrumentid, DbType.String, ParameterDirection.Input);


                    using (var connection = _context.CreateConnection())
                    {
                        connection.Open();
                        result = await connection.QueryFirstOrDefaultAsync<PaymentInResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                        return result;
                    }
                }
                else if (upiPostTransactionRequestModel.MethodType == "payOut")
                {
                    var procedureName = "\"accounts\".stp_payout_updatebankutrnumber";
                    var parameters = new DynamicParameters();

                    parameters.Add("p_paymentid", upiPostTransactionRequestModel.p_paymentid, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("p_txntype", upiPostTransactionRequestModel.p_txntype, DbType.String, ParameterDirection.Input);
                    parameters.Add("p_status", upiPostTransactionRequestModel.p_status, DbType.Int16, ParameterDirection.Input);
                    parameters.Add("p_utrnumber", upiPostTransactionRequestModel.p_utrnumber, DbType.String, ParameterDirection.Input);
                    parameters.Add("p_createdby", upiPostTransactionRequestModel.p_createdby, DbType.Int16, ParameterDirection.Input);
                    parameters.Add("p_creatoripaddress", upiPostTransactionRequestModel.p_creatoripaddress, DbType.String, ParameterDirection.Input);


                    using (var connection = _context.CreateConnection())
                    {
                        connection.Open();
                        result = await connection.QueryFirstOrDefaultAsync<PaymentInResponseModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                        return result;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new PaymentInResponseModel();
            }
        }

        public async Task<IEnumerable<ChannelPartnerDto>> GetChannelPartnerList(int distributorid, CancellationToken cancellationToken = default)
        {
            var query = "select orgid, orgname|| '(' || orgcode || ')' as retailorcode, status from onboarding.tbl_chm_channelpartners tcc where parentorgid = @distributorid; ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<ChannelPartnerDto>(query, new { distributorid = distributorid });
                return result.ToList<ChannelPartnerDto>();
            }

            
        }
        //Add Ledger Account 
        public async Task<AddLedgerAccount> AddLedgerAccount(AddLedgerAccount addLedgerAccount, CancellationToken cancellationToken = default)
        {
            try
            {  
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();

                    string maxAccIdQuery = "SELECT MAX(accid) FROM accounts.tbl_acc_accounts WHERE orgtypeid = @orgtypeid";
                    int maxAccId = await dbConnection.ExecuteScalarAsync<int>(maxAccIdQuery, new { orgtypeid = addLedgerAccount.orgtypeid });

                    // Increment the maxAccId by one to get the new accid
                    addLedgerAccount.accid = maxAccId + 1;

                    string query = @"INSERT INTO accounts.tbl_acc_accounts (orgtypeid,accdescription,accname, accid, status,  creator, creationdate)
	                       VALUES ( @orgtypeid, @accdescription, @accname, @accid, @status, @creator, @creationdate)";
                    var paramas = new { orgtypeid = addLedgerAccount.orgtypeid, accid = addLedgerAccount.accid, accdescription = addLedgerAccount.accdescription, accname = addLedgerAccount.accname, status = 2, creator = 1, creationdate = DateTime.Now };

                    var result = await dbConnection.ExecuteAsync(query, paramas);
                    return addLedgerAccount;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //check duplicate account name
        public async Task<bool> checkDuplicateAddLedgerAccount(AddLedgerAccount addLedgerAccount)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                string name = addLedgerAccount.accname;
                string query = "SELECT EXISTS(SELECT 1 FROM accounts.tbl_acc_accounts WHERE accname = @Name)";
                bool Duplicate = await dbConnection.QueryFirstOrDefaultAsync<bool>(query, new { Name = name });
                return Duplicate;
            }
        }
        //Get Ledger Account 
        public async Task<Tuple<IEnumerable<GetLedgerAccount>, int>> GetAddLedgerAccount(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            try
            {
                pageSize = pageSize < 0 ? 0 : pageSize;
                pageNumber = pageNumber < 0 ? throw new ArgumentException("Page number must be positive.") : pageNumber;
                int? limit = pageSize > 0 ? pageSize : null;
                int? offset = (pageNumber - 1) * pageSize; 
                orderByColumn = orderByColumn ?? "creationdate";
                orderBy = orderBy ?? "desc";
                
                var query = @$"SELECT COUNT(1) 
                       FROM accounts.tbl_acc_accounts a JOIN onboarding.tbl_org_type o ON a.orgtypeid = o.org_type_id WHERE a.status = 2;
                        SELECT a.orgtypeid, a.accid, a.accname, a.accdescription, a.status, o.org_type, a.creator as creator, a.status as status
                            FROM accounts.tbl_acc_accounts a
                              JOIN onboarding.tbl_org_type o ON a.orgtypeid = o.org_type_id
                                WHERE status = 2
                                  order by {orderByColumn} {orderBy}";
                if (limit.HasValue)
                    query += $" LIMIT {limit}";

                if (offset.HasValue)
                    query += $" OFFSET {offset}";

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    using (var multi = await dbConnection.QueryMultipleAsync(query))
                    {
                        var count = multi.Read<int>().Single();
                        var users = multi.Read<GetLedgerAccount>().ToList();
                        return new Tuple<IEnumerable<GetLedgerAccount>, int>(users.ToList<GetLedgerAccount>(), count);

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<DynamicSearchJV>> GetDynamicSearchJV(DynamicSearchRequest entity, CancellationToken cancellationToken = default)
        {
            List<DynamicSearchJV> slist = new List<DynamicSearchJV>();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("SELECT\r\n  COUNT(jvno) OVER() AS totalcount,\r\n  jvno,\r\n  jvname,  description, jvdate , status, creator, corpid,jvstatus ,orgid,  \r\n  creationdate \r\nFROM accounts.Tbl_ACC_JV ");
                queryBuilder.Append($" WHERE\r\n  LOWER(CAST(jvno AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n OR  LOWER(CAST(jvname AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n  OR LOWER(CAST(description AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n   OR LOWER(CAST(jvdate AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n   OR LOWER(CAST(status AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n   OR LOWER(CAST(jvstatus AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\n   OR LOWER(CAST(orgid AS text)) LIKE LOWER('%'||'{entity.p_searchoption}'||'%')\r\nORDER BY jvno DESC");
                if (entity.p_offsetrows > 0 && entity.p_fetchrows > 0)
                {
                    int? limit = entity.p_fetchrows;
                    int? offset = (entity.p_offsetrows - 1) * entity.p_fetchrows;
                    queryBuilder.Append($" LIMIT {limit} OFFSET {offset}   ");
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var TxnHistory = await dbConnection.QueryAsync<DynamicSearchJV>(queryBuilder.ToString());
                    return (IEnumerable<DynamicSearchJV>)TxnHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return slist;
        }

        public async Task<IEnumerable<PayoutReport>> GetPayoutReport(string? orgcode, int status, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            //StringBuilder queryBuilder = new StringBuilder();
            //queryBuilder.Append("select Count(1) over() as totalrecord, pa.paymentid,tcc.orgid,tcc.orgcode,tcc.orgname,tot.org_type,pa.amount,\r\n pa.paymentmode,pa.instrumentnumber,pa.status,pa.creationdate, tmb.bankname DepositingBank,\r\n pa.remark Narration, pa.bankpayinslip SlipNarration,\r\n pa.modificationdate ActionDateTime, pa.vpa UPI,\r\n matm.ifsccode IFSCCode,matm.accountno AccountNumber, matm.transactioncharge TransactionCharge,\r\n matm.utrno bankreferencenumber,\r\n pa.servicecharge,\r\n pab.accountnumber BeneficiaryAccNo , pab.accountholdername  BeneficiaryName\r\n ,pa.remark ");
            //queryBuilder.Append(" from payments.tbl_acc_payments pa \r\n inner join onboarding.tbl_chm_channelpartners tcc on pa.orgid  = tcc.orgid\r\n inner join onboarding.tbl_org_type tot on tcc.orgtype  = tot.org_type_id\r\n inner join common.tbl_mst_bank tmb on pa.bankid =tmb.bankid\r\n  inner join \"AEPSMATM\".tbl_txn_aepsmatm_banktransfer matm on matm.paymentid  = pa.paymentid \r\n inner join onboarding.tbl_org_payoutbanks pab on pab.orgid = pa.orgid  \r\n where  pa.paymentmode in (5,6,7) ");
            //if (status > 0)
            //{
            //    queryBuilder.Append($" AND pa.status = {status} ");
            //}
            //if (orgcode != null)
            //{
            //    if (orgcode != "0")
            //    {
            //        queryBuilder.Append($" AND tcc.orgcode = '{orgcode}' ");
            //    }
            //}
            //if (fromDate != DateTime.MinValue)
            //{
            //    queryBuilder.Append($" AND pa.paymentdate  >= '{fromDate:yyyy-MM-dd}'::date ");
            //}
            //if (toDate != DateTime.MinValue)
            //{
            //    queryBuilder.Append($" AND pa.paymentdate  <= '{toDate:yyyy-MM-dd}'::date ");
            //}
            //queryBuilder.Append($" ORDER BY  pa.paymentid desc  ");
            //if (p_fetchrows > 0 && p_offsetrows != null)
            //{
            //    int? limit = p_fetchrows;
            //    int? offset = (p_offsetrows - 1) * p_fetchrows;
            //    queryBuilder.Append($"  LIMIT {limit} OFFSET {offset}   ");
            //}
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var TxnHistory = await dbConnection.QueryAsync<PayoutReport>(queryBuilder.ToString());
            //    return (IEnumerable<PayoutReport>)TxnHistory.ToList();
            //}

            List<PayoutReport> slist = new List<PayoutReport>();
            try
            {
                var procedureName = "\"accounts\".getpayouthistoryreport";
                var parameters = new DynamicParameters();
                parameters.Add("p_offsetrows", p_offsetrows == null ? 0 : p_offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", p_fetchrows == null ? 0 : p_fetchrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_orgcode", orgcode ?? "", DbType.String, ParameterDirection.Input);
                parameters.Add("p_fromtxndate", Convert.ChangeType(fromDate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_totxndate", Convert.ChangeType(toDate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_status", status, DbType.Int16, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<PayoutReport>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null)
                    {
                        return result.ToList<PayoutReport>();
                    }
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
