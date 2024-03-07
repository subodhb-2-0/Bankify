using Contracts.Bbps;
using Dapper;
using Domain.Entities.Bbps;
using Domain.Entities.Reports;
using Domain.Entities.UserManagement;
using Domain.RepositoryInterfaces;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace Persistence
{
    public class BbpsRepository : IBbpsRepository
    {
        private readonly DapperContext _context;
        public BbpsRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<OperationResult> insertbillerDetails(BbpsBillerListModel entity, CancellationToken cancellationToken = default)
        {
            
                List<XmlParamInfo> xmlData = new List<XmlParamInfo>();
                string xmlParamInfo;
                
                foreach (var sourceItem in entity.p_billerinputparams)
                {
                    var targetItem = new XmlParamInfo
                    {
                        dataType = sourceItem.dataType,
                        isOptional = sourceItem.isOptional,
                        paramName = sourceItem.paramName,
                        maxLength = sourceItem.maxLength,
                        minLength = sourceItem.minLength
                    };
                    xmlData.Add(targetItem);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<XmlParamInfo>));
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, xmlData);
                    xmlParamInfo = writer.ToString();
                }

                //XmlDocument document = new XmlDocument();
                //document.LoadXml(xmlParamInfo);

                var procedureName = "\"BBPS\".stp_bbps_insertbiller";
                var parameters = new DynamicParameters();
                parameters.Add("p_billerid", entity.p_billerid, DbType.String, ParameterDirection.Input);
                parameters.Add("p_serviceproviderid", entity.p_serviceproviderid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_billercategoryid", entity.p_billercategoryid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_billername", entity.p_billername, DbType.String, ParameterDirection.Input);
                parameters.Add("p_adhocpayment", entity.p_adhocpayment, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_coverage", entity.p_coverage, DbType.String, ParameterDirection.Input);
                parameters.Add("p_fetchrequirement", entity.p_fetchrequirement, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_paymentexactness", entity.p_paymentexactness, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_supportbillvalidation", entity.p_supportbillvalidation, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_supportpendingstatus", entity.p_supportpendingstatus, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_supportdeemed", entity.p_supportdeemed, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_billertimeout", entity.p_billertimeout, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_billeramountoptions", entity.p_billeramountoptions, DbType.String, ParameterDirection.Input);
                parameters.Add("p_billerpaymentmode", entity.p_billerpaymentmode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_billerdesc", entity.p_billerdesc, DbType.String, ParameterDirection.Input);
                parameters.Add("p_status", entity.p_status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_createdby", entity.p_createdby, DbType.Int32, ParameterDirection.Input);
                parameters.Add("paraminfo_xml", xmlParamInfo, DbType.Xml, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = connection.QueryFirstAsync<OperationResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return await reports;
                }
             
        }
        public async Task<IEnumerable<GetBillerDetailsModel>> GetbillerDetails(BbpsBillerSearchOptionsModel entity, CancellationToken cancellationToken = default)
        {

            try 
            {
                var procedureName = "\"BBPS\".stp_bbps_getbillerdata";
                var parameters = new DynamicParameters();
                parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_searchoptions", entity.p_searchoptions, DbType.String, ParameterDirection.Input);
                parameters.Add("p_billercategoryid", entity.p_billercategoryid, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = connection.QueryAsync<GetBillerDetailsModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return await reports;
                }
            }
            catch(Exception ex) 
            { 
                string message = ex.Message;
                return new List<GetBillerDetailsModel> { };
            }
            
                
        }
        public async Task<OperationResult> insertAgentDetails(InsertBbpsAgentModel entity, CancellationToken cancellationToken = default)
        {
            var procedureName = "\"BBPS\".stp_bbps_insert_agendetails";
            var parameters = new DynamicParameters();
            parameters.Add("p_orgid", entity.p_orgid, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_agentid", entity.p_agentid, DbType.String, ParameterDirection.Input);
            parameters.Add("p_agentname", entity.p_agentname, DbType.String, ParameterDirection.Input);
            parameters.Add("p_geocode", entity.p_geocode, DbType.String, ParameterDirection.Input);
            parameters.Add("p_mobilenumber", entity.p_mobilenumber, DbType.String, ParameterDirection.Input);
            parameters.Add("p_pincode", entity.p_pincode, DbType.String, ParameterDirection.Input);
            parameters.Add("p_city", entity.p_city, DbType.String, ParameterDirection.Input);
            parameters.Add("p_state", entity.p_state, DbType.String, ParameterDirection.Input);
            parameters.Add("p_createdby", entity.p_createdby, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_modifiedby", entity.p_modifiedby, DbType.Int32, ParameterDirection.Input);
            parameters.Add("p_agentcode", entity.p_agentcode, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var reports = connection.QueryFirstAsync<OperationResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return await reports;
            }
        }
        public async Task<IEnumerable<GetAgentDetailsModel>> GetAgentDetails(GetAgentRequestModel entity, CancellationToken cancellationToken = default)
        {   
                var procedureName = "\"BBPS\".stp_bbps_getagendetailsbyagentcode";
                var parameters = new DynamicParameters();
                parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_agentcode", entity.p_agentcode, DbType.String, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = connection.QueryAsync<GetAgentDetailsModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return await reports;
                }
        }
        public async Task<IEnumerable<Domain.Entities.Bbps.BillerInputParams>> getBillerInputParams(int bbpsbillerid, CancellationToken cancellationToken = default)
        {
            var query = "SELECT paramname,datatype,isoptional,minlength,maxlength,billerparaminfoid \r\nFROM \"BBPS\".tbl_bbps_biller_param_info where bbpsbillerid = @bbpsbillerid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var BBPS = await dbConnection.QueryAsync<Domain.Entities.Bbps.BillerInputParams>(query, new { bbpsbillerid });
                return (IEnumerable<Domain.Entities.Bbps.BillerInputParams>)BBPS;
            }
        }
        public async Task<UpdateBBPSBillerModel> UpdateBillerStatusAsync(UpdateBBPSBillerModel entity, CancellationToken cancellationToken = default)
        {
            var query = "update \"BBPS\".tbl_bbps_biller set status=@status,modifieddate=NOW() where billerid=@billerid";

            var paramas = new { status = entity.status, billerid = entity.billerid };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public async Task<IEnumerable<BillerCategoryResult>> GetBillerCategoryList(CancellationToken cancellationToken = default)
        {
            var query = "select billercategoryid,billercategoryname \r\nFROM \"BBPS\".tbl_bbps_billercategory";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var BBPS = await dbConnection.QueryAsync<BillerCategoryResult>(query);
                return BBPS.ToList<BillerCategoryResult>();
            }
        }

        public async Task<IEnumerable<BbpsServiceResult>> GetBbpsServiceResult(int serviceId, CancellationToken cancellationToken = default)
        {
            var query = "select tms.serviceid,tms.servicename  from servicemanagement.tbl_mst_service tms where tms.serviceid = @serviceId";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var serviceResult = await dbConnection.QueryAsync<BbpsServiceResult>(query, new { serviceId });
                return serviceResult.ToList<BbpsServiceResult>();
            }
        }
    }
}
