using Contracts.AEPS;
using Dapper;
using Domain.Entities.Onboarding;
using Domain.RepositoryInterfaces;
using System.Data;
using System.Text;
using static Dapper.SqlMapper;

namespace Persistence.Onboarding
{
    public class CpBcOnboardingRepository : ICpBcOnboardingRepository
    {
        private readonly DapperContext _context;
        public CpBcOnboardingRepository(DapperContext context)
        {
            _context = context;
        }   
        public async Task<List<CpBcOnboarding>> GetAEPSOnBoardingDetails(int? pageSize, int? pageNumber, CancellationToken cancellationToken)
        {
            List<CpBcOnboarding> response = new();
            try
            {
                StringBuilder stringBuilder = new();
                stringBuilder.AppendLine(" SELECT DISTINCT cpbconboarding.refparam2, cpbconboarding.refparam1, cpbconboarding.onboarding_status, cpBcOnboarding.supplier_csp_id, cpBcOnboarding.orgcode, chanelPtnr.orgname\r\nFROM onboarding.tbl_cp_bc_onboarding AS cpBcOnboarding\r\nLEFT JOIN onboarding.tbl_chm_channelpartners AS chanelPtnr\r\nON cpBcOnboarding.orgcode = chanelPtnr.orgcode\r\nWHERE cpBcOnboarding.orgcode <> ''");
                
                if (pageSize != null && pageSize != 0)
                    stringBuilder.AppendFormat("limit {0} offset {1}", pageSize, pageSize * ((pageNumber ?? 1) - 1));
                string query = stringBuilder.ToString();

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var rawData = await dbConnection.QueryAsync<CpBcOnboarding>(query);
                    response = rawData.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<int> AddPaytmOnboardingAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                string procedureName = "onboarding.stp_insert_paytmonboarding";
                DynamicParameters parameters = new();
                parameters.Add("p_supplierid", request.SupplierId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_supplier_csp_id", request.SupplierCspId ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_onboarding_status", request.OnboardingStatus, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_createdby", request.CreatedBy, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_createdate", request.CreatedDate, DbType.Date, ParameterDirection.Input);
                parameters.Add("p_orgid", request.OrgId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_serviceid", request.ServiceId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_refparam1", request.RefParam1 ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_refparam2", request.RefParam2 ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_refparam3", request.RefParam3 ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_orgcode", request.OrgCode ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_orgname", request.OrgName ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_terminal_id", request.TerminalId ?? string.Empty, DbType.String, ParameterDirection.Input);

                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckPaySprintOnboardingAsync(PaytmOnboardingRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                string query = string.Format($"SELECT CASE WHEN EXISTS ( SELECT 1 FROM onboarding.tbl_cp_bc_onboarding tcbo WHERE tcbo.supplierid = {request.SupplierId} AND tcbo.orgid = {request.OrgId})\r\nTHEN true ELSE false END AS Result;");
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    bool result = await connection.QueryFirstOrDefaultAsync<bool>(query);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdatePaytmOnboardingAsync(int updatedStatus, PaySprintOnboardingDetails request)
        {
            try
            {
                string procedureName = "onboarding.stp_paytm_onboarding_status_update";
                DynamicParameters parameters = new();
                parameters.Add("p_supplierid", request.supplierid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_orgid", request.orgid, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_onboarding_status", updatedStatus, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_refparam1", request.ref_param1 ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_refparam3", request.ref_param3 ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_remarks", request.Remarks ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_modifieddate", DateTime.Now, DbType.Date, ParameterDirection.Input);
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PaySprintOnboardingDetails>> GetPaySprintOnboardingDetailsAsync(PaySprintOnboardingDetailsDto request, CancellationToken cancellationToken)
        {
            try
            {
                string procedureName = "onboarding.stp_get_paysprint_onboarding_report";
                DynamicParameters parameters = new();
                parameters.Add("p_fromdate", request.StartDate, DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", request.EndtDate, DbType.Date, ParameterDirection.Input);
                parameters.Add("p_orgcode", request.OrgCode ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_offsetvalue", request.PageNumber, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", request.PageSize, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_supplierid", request.SupplierId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_bankname",request.Bank ?? string.Empty, DbType.String, ParameterDirection.Input);
                parameters.Add("p_searchoption", request.SearchBy, DbType.String, ParameterDirection.Input);
                parameters.Add("p_status", request.Status, DbType.Int16, ParameterDirection.Input);

                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<PaySprintOnboardingDetails>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<string> GetStateCodeByPincode(string pincode)
        {
            try
            {
                string query = string.Format($"SELECT tms.statecode \r\nFROM common.tbl_mst_cityarea tmc \r\nLEFT JOIN common.tbl_mst_state tms ON tmc.stateid = tms.stateid \r\nWHERE tmc.pincode = {pincode}\r\nLIMIT 1;");
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    string result = await connection.QueryFirstOrDefaultAsync<string>(query);
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        } 
    }
}
