using Contracts.Acquisition;
using Contracts.Onboarding;
using Dapper;
using Domain.Entities.Acquisition;
using Domain.Entities.Location;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using static Dapper.SqlMapper;

namespace Persistence
{
    public class AcquisitionRepository : IAcquisitionRepository
    {
        private readonly DapperContext _context;
        public AcquisitionRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Acquisition>> GetAcquisitionDetails(int? Id, string? Value, CancellationToken cancellationToken = default)
        {
            //var query = "select tcc.orgid ,tcc.orgcode ,tcc.orgname ,tot.org_type  ,tuu.mobilenumber,tcc.status  from  onboarding.tbl_chm_channelpartners tcc,\r\nuser_management.tbl_um_users tuu , onboarding.tbl_org_type tot \r\nwhere tcc.orgid =tuu.orgid and tcc.orgtype =tot.org_type_id and tuu.reportsto is null order by tcc.orgid ";
            var query = "select tcc.orgid ,tcc.orgcode ,tcc.orgname ,tot.org_type  ,tuu.mobilenumber,tcc.status  from  onboarding.tbl_chm_channelpartners tcc,\r\nuser_management.tbl_um_users tuu , onboarding.tbl_org_type tot \r\nwhere tcc.orgid =tuu.orgid and tcc.orgtype =tot.org_type_id and tuu.reportsto is null and tcc.orgid = (CASE WHEN @Id > 0 THEN @Id ELSE tcc.orgid END) and tuu.mobilenumber = (CASE WHEN @Value <> '' THEN @Value ELSE tuu.mobilenumber END) order by tcc.orgid ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<Acquisition>(query, new { Id, Value });
                return comissionDtls.ToList<Acquisition>();
            }
        }
        //public async Task<AddAcquisitionDto> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default)
        //{
        //    try
        //    {
        //        //string password = GenerateRandomOTP();
        //        using (IDbConnection dbConnection = _context.CreateConnection())
        //        {
        //            StringBuilder commandBuilder = new StringBuilder();
        //            commandBuilder.AppendLine($"DO $$");
        //            commandBuilder.AppendLine($"DECLARE insertedOrgId integer;");
        //            commandBuilder.AppendLine($"BEGIN ");
        //            commandBuilder.AppendLine($"INSERT INTO onboarding.tbl_chm_channelpartners (orgcode, orgname, orgtype, parentorgid, status, creator,creationdate, productid, secdeposit,fieldstaffid, secdepositchequeno, secdepositbankid, secdepositaccount, paidby, payment_option,onboardstage, kyc_status) VALUES('{addAcquisitionDto.orgcode}','{addAcquisitionDto.orgname}',{addAcquisitionDto.orgtype},{addAcquisitionDto.parentOrgId},0,{addAcquisitionDto.creator},NOW(),{addAcquisitionDto.productId},{addAcquisitionDto.secdeposit},{addAcquisitionDto.fieldStaffId},'{addAcquisitionDto.secdepositchequeno}',{addAcquisitionDto.secDepositBankId},'{addAcquisitionDto.secdepositaccount}','{addAcquisitionDto.paidBy}','{addAcquisitionDto.payment_option}',0,1) RETURNING OrgId into insertedOrgId;");
        //            commandBuilder.AppendLine($"INSERT INTO onboarding.tbl_org_cpdetails (orgid,status, busi_sub_district,creator,creationDate) VALUES (insertedOrgId,1,'{addAcquisitionDto.busi_sub_district}',{addAcquisitionDto.creator},NOW());");
        //            commandBuilder.AppendLine($"INSERT INTO onboarding.\"tbl_org_KYCdetails\" (orgid,kyc_status,creator,creationdate,onboardstage) VALUES (insertedOrgId,1,{addAcquisitionDto.creator},NOW(),0);");
        //            commandBuilder.AppendLine($"INSERT INTO user_management.tbl_um_users (firstname, middlename, lastname, loginid, password,orgid,emailid ,mobilenumber,needspasschange,roleid,failedcount, status, creator, creationdate,modificationdate,lastpwdchangedate,ispasscodeexists, usertypeid) VALUES('{addAcquisitionDto.fName}','{addAcquisitionDto.mName}','{addAcquisitionDto.lName}','{addAcquisitionDto.orgcode}','{addAcquisitionDto.password}',insertedOrgId,'{addAcquisitionDto.emailid}','{addAcquisitionDto.mobileNumber}', 1,5 ,0,1,{addAcquisitionDto.creator},NOW(),NOW(),NOW(),0,2);");
        //            for (int i = 1; i <= 7; i++)
        //            {
        //                commandBuilder.AppendLine($"INSERT INTO onboarding.tbl_org_onboard_status (orgid,onboardstate,status,statusdate) VALUES (insertedOrgId,{i},1,NOW());");
        //            }
        //            commandBuilder.AppendLine($" END$$");
        //            dbConnection.Open();
        //            var result = await dbConnection.ExecuteAsync(commandBuilder.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return addAcquisitionDto;
        //}
        public async Task<AddAcquisitionResponse> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"onboarding\".stp_org_createcp";
                var parameters = new DynamicParameters();


                parameters.Add("p_orgname", addAcquisitionDto.orgname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_orgtype", addAcquisitionDto.orgtype, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_parentorgid", addAcquisitionDto.parentOrgId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_status", 0, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_creator", 0, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_productid", addAcquisitionDto.productId, DbType.Int32, ParameterDirection.Input);
                //parameters.Add("p_secdeposit", addAcquisitionDto.secdeposit, DbType.VarNumeric, ParameterDirection.Input);
                parameters.Add("p_secdeposit", 0.00, DbType.VarNumeric, ParameterDirection.Input);
                parameters.Add("p_fieldstaffid", addAcquisitionDto.fieldStaffId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_secdepositchequeno", addAcquisitionDto.secdepositchequeno, DbType.String, ParameterDirection.Input);
                parameters.Add("p_secdepositbankid", addAcquisitionDto.secDepositBankId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_secdepositaccount", addAcquisitionDto.secdepositaccount, DbType.String, ParameterDirection.Input);
                parameters.Add("p_paidby", addAcquisitionDto.paidBy, DbType.String, ParameterDirection.Input);
                parameters.Add("p_payment_option", addAcquisitionDto.payment_option, DbType.String, ParameterDirection.Input);
                parameters.Add("p_onboardstage", 0, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_kyc_status", 1, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_busi_sub_district", addAcquisitionDto.busi_sub_district, DbType.String, ParameterDirection.Input);
                parameters.Add("p_firstname", addAcquisitionDto.fName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_middlename", addAcquisitionDto.mName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_lastname", addAcquisitionDto.lName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_loginid", addAcquisitionDto.orgcode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_password", addAcquisitionDto.password, DbType.String, ParameterDirection.Input);
                parameters.Add("p_emailid", addAcquisitionDto.emailid, DbType.String, ParameterDirection.Input);
                parameters.Add("p_mobilenumber", addAcquisitionDto.mobileNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("p_needspasschange", 0, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_roleid", 5, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_failedcount", 0, DbType.Int32, ParameterDirection.Input);
                //parameters.Add("p_lastpwdchangedate", DateTime.Now.ToShortDateString(), DbType.DateTime, ParameterDirection.Input);
                parameters.Add("p_ispasscodeexists", 0, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_usertypeid", 2, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_channelid", addAcquisitionDto.channels, DbType.Int32, ParameterDirection.Input);

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

        public async Task<Tuple<IEnumerable<VerifyCPApplication>, int>> VerifyCPApplication(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default)
        {
            //var query = "select distinct tcc.orgid, tcc.orgcode, tcc.orgname, tcc.orgtype, tcc.parentorgid, tcc.firsttrxdate, tcc.lasttrxdate, tcc.status, tcc.creator, tcc.productid, tcc.enrollmentfeecharged, \r\ntcc.secdeposit, tcc.latitude, tcc.longitude, tcc.pgsstatus, tcc.preferredlanguage, tcc.classoftrade, tcc.activationdate, tcc.fieldstaffid, tcc.secdepositchequeno, tcc.secdepositbankid, tcc.secdepositaccount, tcc.paidby, tcc.payment_option, \r\ntcc.onboardstage\r\nfrom onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid \r\nleft join user_management.tbl_um_users tuu on tuu.orgid = toc.orgid";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var comissionDtls = await dbConnection.QueryAsync<VerifyCPApplication>(query);
            //    return comissionDtls.ToList<VerifyCPApplication>();
            //}

            int limit = pageSize;
            int offset = (pageNumber - 1) * pageSize;
            var query = string.Format("Select Count(1) from onboarding.tbl_chm_channelpartners tcc inner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid inner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid;" +
                 " select tcc.orgid, tcc.orgcode, tcc.orgname, tcc.orgtype, tcc.parentorgid, tcc.firsttrxdate, tcc.lasttrxdate, tcc.status, tcc.creator, tcc.productid, tcc.enrollmentfeecharged, \r\ntcc.secdeposit, tcc.latitude, tcc.longitude, tcc.pgsstatus, tcc.preferredlanguage, tcc.classoftrade, tcc.activationdate, tcc.fieldstaffid, tcc.secdepositchequeno, tcc.secdepositbankid, tcc.secdepositaccount, tcc.paidby, tcc.payment_option,toc.busi_sub_district, \r\ntcc.onboardstage , tcc.creationdate , tcc.modificationdate \r\nfrom onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid " +
                 "order by tcc.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<VerifyCPApplication>().ToList();
                    return new Tuple<IEnumerable<VerifyCPApplication>, int>(users.ToList<VerifyCPApplication>(), count);
                }
            }

        }
        public async Task<IEnumerable<ListOrgTypeDto>> GetListOrgType(CancellationToken cancellationToken = default)
        {
            var query = "select org_type_id ,org_type  from  onboarding.tbl_org_type";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListOrgTypeDto>(query);
                return comissionDtls.ToList<ListOrgTypeDto>();
            }
        }
        public async Task<IEnumerable<ListofFeildStaffDto>> GetListofFeildStaff(CancellationToken cancellationToken = default)
        {
            var query = "select userId,firstName, lastname,tur.rolename  from user_management.tbl_um_users tuu , \r\nuser_management.tbl_um_role tur where tuu.roleid =tur.roleid  and tur.roleid =28 \r\nand tuu.status=2 and tur.status =2;";
            //var query = "select userId,firstName, lastname,tur.rolename  from user_management.tbl_um_users tuu , \r\nuser_management.tbl_um_role tur where tuu.roleid =tur.roleid  and tur.roleid =28;";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListofFeildStaffDto>(query);
                return comissionDtls.ToList<ListofFeildStaffDto>();
            }
        }
        public async Task<IEnumerable<ParentOrgIdDto>> GetParentOrgId(int? OrgType, CancellationToken cancellationToken = default)
        {
            var query = "select orgId,orgName from onboarding.tbl_chm_channelpartners";
            if (OrgType == 2)
            {
                query = "select orgId,orgName from onboarding.tbl_chm_channelpartners where status = 2 and orgType in (6,3,1)";
            }
            else if (OrgType == 3)
            {
                query = "select orgId,orgName from onboarding.tbl_chm_channelpartners where status = 2 and orgType in (6,1)";
            }
            else if (OrgType == 6)
            {
                query = "select orgId,orgName from onboarding.tbl_chm_channelpartners where status = 2 and orgType = 1";
            }
            else
            {
                query = "select orgId,orgName from onboarding.tbl_chm_channelpartners ";
            }
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ParentOrgIdDto>(query, new { OrgType });
                return comissionDtls.ToList<ParentOrgIdDto>();
            }
        }
        //public async Task<AddAcquisitionDto> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default)
        //{
        //    try
        //    {
        //        //orgname,orgtype,fieldStaffId,parentOrgId,fName,lName,mName,mobileNumber,productId,secdeposit,paidBy,paymentmode,secDepositBankId,SecDepositBankAC
        //        //string query1 = @"INSERT INTO onboarding.tbl_chm_channelpartners (orgcode, orgname, orgtype, parentorgid, status, creator, creationdate, productid, secdeposit,
        //        //             fieldstaffid, secdepositchequeno, secdepositbankid, secdepositaccount, paidby, payment_option, onboardstage, kyc_status)
        //        //             VALUES('1000',@orgname,@orgtype,@parentorgid,0, @creator,NOW(), @productid,@secdeposit,@fieldstaffid ,@secdepositchequeno,@secdepositbankid, @secdepositaccount, @paidby, @payment_option, 0, 1) RETURNING orgid;";
        //        //var paramas1 = new { orgname = addAcquisitionDto.orgname, orgtype = addAcquisitionDto.orgtype, parentOrgId = addAcquisitionDto.parentOrgId, creator = addAcquisitionDto.creator, productid = addAcquisitionDto.productId, 
        //        //    secdeposit = addAcquisitionDto.secdeposit, fieldStaffId = addAcquisitionDto.fieldStaffId, secdepositchequeno = addAcquisitionDto.secdepositchequeno, secDepositBankId = addAcquisitionDto.secDepositBankId, secdepositaccount = addAcquisitionDto.secdepositaccount, paidby = addAcquisitionDto.paidBy, payment_option = addAcquisitionDto.payment_option };

        //        //using (IDbConnection dbConnection = _context.CreateConnection())
        //        //{
        //        //    //System.Data.IDbTransaction dbTransaction = dbConnection.BeginTransaction();
        //        //    dbConnection.Open();
        //        //    //var result = await dbConnection.ExecuteAsync(query, paramas, dbTransaction);
        //        //    var result1 = await dbConnection.ExecuteAsync(query1, paramas1);
        //        //    addAcquisitionDto.orgid = result1;
        //        //    //var result2 = await dbConnection.ExecuteAsync(query2, paramas2);
        //        //    //dbTransaction.Commit();
        //        //}
        //        //string query2 = @"INSERT INTO onboarding.tbl_org_cpdetails (orgid,status, creator,creationDate)
        //        //               VALUES ( @orgid, @creator,1,NOW())";
        //        //var paramas2 = new { orgid = addAcquisitionDto.orgid, creator = addAcquisitionDto.creator};
        //        //using (IDbConnection dbConnection = _context.CreateConnection())
        //        //{
        //        //    dbConnection.Open();
        //        //    var result2 = await dbConnection.ExecuteAsync(query2, paramas2);
        //        //}

        //string query3 = @"INSERT INTO onboarding.tbl_org_KYCdetails (orgid, kyc_status, creator, creationdate, onboardstage)
        //                  VALUES(@orgid,1,@creator,NOW(),0)";
        //        //var paramas3 = new { orgid = addAcquisitionDto.orgid, creator = addAcquisitionDto.creator };
        //        //using (IDbConnection dbConnection = _context.CreateConnection())
        //        //{
        //        //    dbConnection.Open();
        //        //    var result3 = await dbConnection.ExecuteAsync(query3, paramas3);
        //        //}

        //        string query4 = @"INSERT INTO user_management.tbl_um_users (firstname, middlename, lastname, loginid, orgid, mobilenumber, needspasschange,roleid,failedcount, status, creator, creationdate,modificationdate,lastpwdchangedate,ispasscodeexists, usertypeid)
        //                        VALUES(@firstname,@middlename,@lastname,@loginid, @orgid, @mobilenumber, 1,5 ,0,1, @creator, NOW(),NOW(),NOW(),0, 2)";
        //        var paramas4 = new { firstname = addAcquisitionDto.fName, middlename = addAcquisitionDto.mName, lastname = addAcquisitionDto.lName, loginid = addAcquisitionDto.orgcode, orgid = addAcquisitionDto.orgid, mobilenumber = addAcquisitionDto.mobileNumber, creator = addAcquisitionDto.creator };
        //        using (IDbConnection dbConnection = _context.CreateConnection())
        //        {
        //            dbConnection.Open();
        //            var result4 = await dbConnection.ExecuteAsync(query4, paramas4);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Tran.Rollback();
        //    }
        //    return addAcquisitionDto;
        //}
        public async Task<IEnumerable<VerifyChannelPartner>> VerifyChannelPartner(int? Id, string? Value, CancellationToken cancellationToken = default)
        {
            //var query = "select tcc.orgid ,tcc.orgcode ,tcc.orgname ,tot.org_type  ,tuu.mobilenumber,tcc.status  from  onboarding.tbl_chm_channelpartners tcc,\r\nuser_management.tbl_um_users tuu , onboarding.tbl_org_type tot \r\nwhere tcc.orgid =tuu.orgid and tcc.orgtype =tot.org_type_id and tuu.reportsto is null and tcc.orgid = (CASE WHEN @Id > 0 THEN @Id ELSE tcc.orgid END) and tuu.mobilenumber = (CASE WHEN @Value <> '' THEN @Value ELSE tuu.mobilenumber END) order by tcc.orgid ";
            var query = "select tcc.orgid ,tcc.orgcode ,tcc.orgname ,tot.org_type ,tuu.mobilenumber from  onboarding.tbl_chm_channelpartners tcc,\r\nuser_management.tbl_um_users tuu , onboarding.tbl_org_type tot \r\nwhere tcc.orgid =tuu.orgid \r\nand tcc.orgtype =tot.org_type_id  \r\nand tcc.status=0 and tcc.onboardstage = 7 and tcc.orgid = (CASE WHEN @Id > 0 THEN @Id ELSE tcc.orgid END) and tuu.mobilenumber = (CASE WHEN @Value <> '' THEN @Value ELSE tuu.mobilenumber END) ";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<VerifyChannelPartner>(query, new { Id, Value });
                return comissionDtls.ToList<VerifyChannelPartner>();
            }
        }

        public async Task<CPDetailsforApprovalDto>GetCPDetailsById(int? OrgId, CancellationToken cancellationToken = default)
        {
            CPDetailsforApprovalDto cPDetailsforApprovalDto = new();
            try
            {
                var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string imgUrl = configurationBuilder.GetSection("ImagePath")["URL"];
                imgUrl = imgUrl + "Uploads/PaymentReceipt/";

                var chanquery = "select status from onboarding.tbl_chm_channelpartners where orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE orgid END)";
                var orgquery = "select tcc.orgcode \"orgCode\", tot.org_type_id ,tot.org_type  \"orgType\" ,tcc.orgname \"organizationName\" ,concat(tuu.firstname,' ',tuu.middlename,' ' , tuu.lastname)  \"ownerName\" ,\r\ntuu.lastname ,tuu.mobilenumber \"mobile\" ,\r\ntom.status \"mobileStatus\",\r\ntuu.emailid \"mailId\",\r\ntoms.status  \"mailStatus\", " +
                    "concat('" + imgUrl + "', tok.selfimage) \"selfImage\",\r\ntosi.status \"selfImageVerficationStatus\" \r\nfrom onboarding.tbl_chm_channelpartners tcc ,\r\nonboarding.tbl_org_type tot, \r\nonboarding.\"tbl_org_KYCdetails\" tok \r\n,user_management.tbl_um_users tuu\r\n,onboarding.tbl_org_onboard_status tom \r\n,onboarding.tbl_org_onboard_status toms \r\n,onboarding.tbl_org_onboard_status tosi \r\nwhere tcc.orgid =tok.orgid and tcc.orgid = tuu.orgid \r\nand tcc.orgid =tok.orgid and tok.orgid =tuu.orgid \r\nand tot.org_type_id =tcc.orgtype and tom.orgid  = tcc.orgid and tom.onboardstate = 1\r\nand toms.orgid  = tcc.orgid and toms.onboardstate = 2\r\nand tosi.orgid  = tcc.orgid and tosi.onboardstate = 7\r\nand tcc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE tcc.orgid END)";
                var kycquery = "select toc.dob ,toc.gender ,tok.pancard \"pan\",ton.status \"panStatu\",tok.pancardname \"nameInPAN\",tok.aadharcard \"aadhaar\",\r\ntok.aadharcardname \"nameInAadhaar\",tom.status \"aadhaarStatus\" from onboarding.tbl_org_cpdetails toc  ,\r\nonboarding.\"tbl_org_KYCdetails\" tok \r\n,onboarding.tbl_org_onboard_status tom \r\n,onboarding.tbl_org_onboard_status ton \r\nwhere toc.orgid = tok.orgid  \r\nand tom.orgid  = toc.orgid and tom.onboardstate = 4\r\nand ton.orgid  = toc.orgid and ton.onboardstate = 3\r\nand toc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";
                var permquery = "select toc.perm_house_no \"permHouseNo\",toc.perm_road ,toc.perm_dist \"permDist\" ,toc.perm_sub_dist \"permSubDst\",\r\ntoc.perm_pincode \"permPincode\" ,toc.perm_landmark \"permLandmark\"\r\nfrom onboarding.tbl_org_cpdetails toc  where toc.orgid= (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";
                var bankquery = " select tok.bankid,tmb.bankname \"bankName\",tok.ifsccode \"ifsc\" ,tok.accountnumber,tok.bankaccountname  \"accountHolderName\" , concat('" + imgUrl + "', tok.cancelledchequeimage) \"cancelledchequeimage\" , tos.status  \"status\" \r\n from onboarding.\"tbl_org_KYCdetails\" tok \r\n inner join  user_management.tbl_um_users tuu on tok.orgid =tuu.orgid\r\n left join common.tbl_mst_bank tmb on  tok.bankid =tmb.bankid  \r\n left join onboarding.tbl_org_onboard_status tos on tos.orgid  = tok.orgid  and tos.onboardstate  = 5\r\n where tok.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE tok.orgid END)";
                var buissquery = "select toc.busi_house_no \"businessHouseNo\",toc.busi_road \"businessRoad\" ,toc.busi_district \"businessDist\" ,toc.busi_sub_district \"businessSubDst\",toc.busi_pincode \"businessPincode\",toc.busi_landmark \"businessLandmark\" ,concat('" + imgUrl + "', kyc.shopimage)  \"businessShopImage\",\r\nconcat('" + imgUrl + "', kyc.aadharimage1)  \"aadharImage1\",\r\nconcat('" + imgUrl + "', kyc.aadharimage2)  \"aadharImage2\",\r\nconcat('" + imgUrl + "', kyc.aadharimage3)  \"aadharImage3\",\r\ntos.status,kyc.ref_param1 as Lat,kyc.ref_param2 as Long from onboarding.tbl_org_cpdetails toc  left join onboarding.tbl_org_onboard_status tos on tos.orgid  = toc.orgid  inner join onboarding.\"tbl_org_KYCdetails\" kyc on kyc.orgid  = toc.orgid and tos.onboardstate  = 6 where toc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var role = await dbConnection.QueryFirstAsync<StatusDto>(chanquery, new { OrgId });
                    var organizationIfoDto = await dbConnection.QueryAsync<OrganizationIfoDto>(orgquery, new { OrgId });
                    var kycInfoDto = await dbConnection.QueryAsync<KycInfoDto>(kycquery, new { OrgId });
                    var permanentAddressDto = await dbConnection.QueryAsync<PermanentAddressDto>(permquery, new { OrgId });
                    var bankingInfoDto = await dbConnection.QueryAsync<BankingInfoDto>(bankquery, new { OrgId });
                    var businessInfoDto = await dbConnection.QueryAsync<BusinessInfoDto>(buissquery, new { OrgId });

                    cPDetailsforApprovalDto = new()
                    {
                        getChannelPartnerDetails = new()
                        { 
                            status = new()
                            { 
                                status = role.status,
                                organizationIfo = organizationIfoDto?.FirstOrDefault() ?? new OrganizationIfoDto(),
                                kycInfo = kycInfoDto?.FirstOrDefault() ?? new KycInfoDto()
                            }
                        }
                    };
                    cPDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.permanentAddress = permanentAddressDto?.FirstOrDefault() ?? new PermanentAddressDto();
                    cPDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.bankingInfo = bankingInfoDto?.FirstOrDefault() ?? new BankingInfoDto();
                    cPDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.businessInfo = businessInfoDto?.FirstOrDefault() ?? new BusinessInfoDto();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return cPDetailsforApprovalDto;
        }

        public async Task<CPDetailsforApprovalDto> GetCPDetailsforApproval(int? OrgId, CancellationToken cancellationToken = default)
        {
            CPDetailsforApprovalDto cpDetailsforApprovalDto = new CPDetailsforApprovalDto();
            cpDetailsforApprovalDto.getChannelPartnerDetails = new GetChannelPartnerDetailsDto();
            cpDetailsforApprovalDto.getChannelPartnerDetails.status = new StatusDto();
            cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo = new KycInfoDto();
            cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.permanentAddress = new PermanentAddressDto();
            cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.bankingInfo = new BankingInfoDto();
            cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.businessInfo = new BusinessInfoDto();


            List<OrganizationIfoDto> organizationIfoDto = new List<OrganizationIfoDto>();
            List<KycInfoDto> kycInfoDto = new List<KycInfoDto>();
            List<PermanentAddressDto> permanentAddress = new List<PermanentAddressDto>();
            List<BankingInfoDto> bankinfo = new List<BankingInfoDto>();
            List<BusinessInfoDto> businfo = new List<BusinessInfoDto>();
            try
            {
                //var orgquery = "select tot.org_type_id ,tot.org_type  \"orgType\" ,tcc.orgname \"organizationName\" ,concat(tuu.firstname,' ' , tuu.lastname)  \"ownerName\" ,\r\ntuu.lastname ,tuu.mobilenumber \"mobile\" ,'1' \"mobileStatus\",tuu.emailid \"mailId\",'1' \"mailStatus\", tok.selfimage \"selfImage\",'1' \"selfImageVerficationStatus\" from onboarding.tbl_chm_channelpartners tcc ,\r\nonboarding.tbl_org_type tot, \r\nonboarding.\"tbl_org_KYCdetails\" tok \r\n,user_management.tbl_um_users tuu\r\nwhere tcc.orgid =tok.orgid and tcc.orgid = tuu.orgid \r\nand tcc.orgid =tok.orgid and " +
                //    "tok.orgid =tuu.orgid \r\nand tot.org_type_id =tcc.orgtype and tcc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE tcc.orgid END)";
                //var kycquery = "select toc.dob ,toc.gender ,tok.pancard \"pan\",'1' \"panStatu\",tok.pancardname \"nameInPAN\",tok.aadharcard \"aadhaar\",\r\ntok.aadharcardname \"nameInAadhaar\",'1' \"aadhaarStatus\" \r\nfrom onboarding.tbl_org_cpdetails toc  ,onboarding.\"tbl_org_KYCdetails\" tok \r\nwhere toc.orgid = tok.orgid and toc.orgid= (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";

                var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string imgUrl = configurationBuilder.GetSection("ImagePath")["URL"];
                imgUrl = imgUrl + "Uploads/PaymentReceipt/";

                var chanquery = "select status from onboarding.tbl_chm_channelpartners where orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE orgid END)";
                var orgquery = "select tot.org_type_id ,tot.org_type  \"orgType\" ,tcc.orgname \"organizationName\" ,concat(tuu.firstname,' ',tuu.middlename,' ' , tuu.lastname)  \"ownerName\" ,\r\ntuu.lastname ,tuu.mobilenumber \"mobile\" ,\r\ntom.status \"mobileStatus\",\r\ntuu.emailid \"mailId\",\r\ntoms.status  \"mailStatus\", " +
                    "concat('" + imgUrl + "', tok.selfimage) \"selfImage\",\r\ntosi.status \"selfImageVerficationStatus\" \r\nfrom onboarding.tbl_chm_channelpartners tcc ,\r\nonboarding.tbl_org_type tot, \r\nonboarding.\"tbl_org_KYCdetails\" tok \r\n,user_management.tbl_um_users tuu\r\n,onboarding.tbl_org_onboard_status tom \r\n,onboarding.tbl_org_onboard_status toms \r\n,onboarding.tbl_org_onboard_status tosi \r\nwhere tcc.orgid =tok.orgid and tcc.orgid = tuu.orgid \r\nand tcc.orgid =tok.orgid and tok.orgid =tuu.orgid \r\nand tot.org_type_id =tcc.orgtype and tom.orgid  = tcc.orgid and tom.onboardstate = 1\r\nand toms.orgid  = tcc.orgid and toms.onboardstate = 2\r\nand tosi.orgid  = tcc.orgid and tosi.onboardstate = 7\r\nand tcc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE tcc.orgid END)";
                var kycquery = "select toc.dob ,toc.gender ,tok.pancard \"pan\",ton.status \"panStatu\",tok.pancardname \"nameInPAN\",tok.aadharcard \"aadhaar\",\r\ntok.aadharcardname \"nameInAadhaar\",tom.status \"aadhaarStatus\" from onboarding.tbl_org_cpdetails toc  ,\r\nonboarding.\"tbl_org_KYCdetails\" tok \r\n,onboarding.tbl_org_onboard_status tom \r\n,onboarding.tbl_org_onboard_status ton \r\nwhere toc.orgid = tok.orgid  \r\nand tom.orgid  = toc.orgid and tom.onboardstate = 4\r\nand ton.orgid  = toc.orgid and ton.onboardstate = 3\r\nand toc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";
                var permquery = "select toc.perm_house_no \"permHouseNo\",toc.perm_road ,toc.perm_dist \"permDist\" ,toc.perm_sub_dist \"permSubDst\",\r\ntoc.perm_pincode \"permPincode\" ,toc.perm_landmark \"permLandmark\"\r\nfrom onboarding.tbl_org_cpdetails toc  where toc.orgid= (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";
                //var bankquery = " select tok.bankid,tmb.bankname \"bankName\",tok.ifsccode \"ifsc\" ,tok.accountnumber,tok.bankaccountname  \"accountHolderName\" ,\r\n tos.status  \"status\" \r\n from onboarding.\"tbl_org_KYCdetails\" tok \r\n inner join  user_management.tbl_um_users tuu on tok.orgid =tuu.orgid\r\n left join common.tbl_mst_bank tmb on  tok.bankid =tmb.bankid  \r\n left join onboarding.tbl_org_onboard_status tos on tos.orgid  = tok.orgid  and tos.onboardstate  = 5\r\n where tok.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE tok.orgid END)";
                var bankquery = " select tok.bankid,tmb.bankname \"bankName\",tok.ifsccode \"ifsc\" ,tok.accountnumber,tok.bankaccountname  \"accountHolderName\" , concat('" + imgUrl + "', tok.cancelledchequeimage) \"cancelledchequeimage\" , tos.status  \"status\" \r\n from onboarding.\"tbl_org_KYCdetails\" tok \r\n inner join  user_management.tbl_um_users tuu on tok.orgid =tuu.orgid\r\n left join common.tbl_mst_bank tmb on  tok.bankid =tmb.bankid  \r\n left join onboarding.tbl_org_onboard_status tos on tos.orgid  = tok.orgid  and tos.onboardstate  = 5\r\n where tok.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE tok.orgid END)";
                //var buissquery = "select toc.busi_house_no \"businessHouseNo\",toc.busi_road ,toc.busi_district \"businessDist\" ,toc.busi_sub_district \"businessSubDst\",\r\n toc.busi_pincode \"businessPincode\",toc.busi_landmark \"businessLandmark\" ,toc.busi_addr_proof \"businessShopImage\",\r\n tos.status from onboarding.tbl_org_cpdetails toc \r\n left join onboarding.tbl_org_onboard_status tos on tos.orgid  = toc.orgid  and tos.onboardstate  = 6\r\n where toc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";
                var buissquery = "select toc.busi_house_no \"businessHouseNo\",toc.busi_road \"businessRoad\" ,toc.busi_district \"businessDist\" ,toc.busi_sub_district \"businessSubDst\",toc.busi_pincode \"businessPincode\",toc.busi_landmark \"businessLandmark\" ,concat('" + imgUrl + "', kyc.shopimage)  \"businessShopImage\",\r\nconcat('" + imgUrl + "', kyc.aadharimage1)  \"aadharImage1\",\r\nconcat('" + imgUrl + "', kyc.aadharimage2)  \"aadharImage2\",\r\nconcat('" + imgUrl + "', kyc.aadharimage3)  \"aadharImage3\",\r\ntos.status,kyc.ref_param1 as Lat,kyc.ref_param2 as Long from onboarding.tbl_org_cpdetails toc  left join onboarding.tbl_org_onboard_status tos on tos.orgid  = toc.orgid  inner join onboarding.\"tbl_org_KYCdetails\" kyc on kyc.orgid  = toc.orgid and tos.onboardstate  = 6 where toc.orgid = (CASE WHEN @OrgId > 0 THEN @OrgId ELSE toc.orgid END)";

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var role = await dbConnection.QueryFirstAsync<StatusDto>(chanquery, new { OrgId });
                    cpDetailsforApprovalDto.getChannelPartnerDetails.status = role;
                    //return role;
                }

                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var qr1 = await dbConnection.QueryAsync<OrganizationIfoDto>(orgquery, new { OrgId });
                    organizationIfoDto = qr1.ToList<OrganizationIfoDto>();
                    cpDetailsforApprovalDto.getChannelPartnerDetails.status.organizationIfo = organizationIfoDto.FirstOrDefault();
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var qr2 = await dbConnection.QueryAsync<KycInfoDto>(kycquery, new { OrgId });
                    kycInfoDto = qr2.ToList<KycInfoDto>();
                    cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo = kycInfoDto.FirstOrDefault();
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var qr3 = await dbConnection.QueryAsync<PermanentAddressDto>(permquery, new { OrgId });
                    permanentAddress = qr3.ToList<PermanentAddressDto>();
                    cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.permanentAddress = permanentAddress.FirstOrDefault();
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var qr4 = await dbConnection.QueryAsync<BankingInfoDto>(bankquery, new { OrgId });
                    bankinfo = qr4.ToList<BankingInfoDto>();
                    cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.bankingInfo = bankinfo.FirstOrDefault();
                }
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var qr5 = await dbConnection.QueryAsync<BusinessInfoDto>(buissquery, new { OrgId });
                    businfo = qr5.ToList<BusinessInfoDto>();
                    cpDetailsforApprovalDto.getChannelPartnerDetails.status.kycInfo.businessInfo = businfo.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return cpDetailsforApprovalDto;
        }
        public async Task<int> ConfirmRejectCP(ConfirmRejectCPInfoDto crInfoDto, CancellationToken cancellationToken = default)
        {
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendLine($"DO $$");

            commandBuilder.AppendLine($"BEGIN ");
            commandBuilder.AppendLine($"update onboarding.tbl_chm_channelpartners set status= {crInfoDto.confirmRejectCP.statusId} where orgid={crInfoDto.confirmRejectCP.orgId};");
            //update onboarding.tbl_org_onboard_status set status = 2,statusdate = now() where tbl_org_onboard_status.onboardstate = 1 and orgid = 22;
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage1} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 1 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage2} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 2 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage3} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 3 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage4} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 4 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage5} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 5 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage6} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 6 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls.onboardStage7} , statusdate= NOW() where tbl_org_onboard_status.onboardstate = 7 and orgid={crInfoDto.confirmRejectCP.orgId};");
            commandBuilder.AppendLine($" END$$");

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(commandBuilder.ToString());
                result = 1;
                return result;
            }

            //using (IDbConnection dbConnection = _context.CreateConnection())
            //    {

            //        dbConnection.Open();
            //        var result = await dbConnection.ExecuteAsync(commandBuilder.ToString());
            //    }

            //return result;
        }

        public async Task<int> UpdateCPStatus(int orgid, int status, CancellationToken cancellationToken = default)
        {
            var query = "update onboarding.tbl_chm_channelpartners set status = @status , modificationdate = @modificationdate  where orgid = @orgid";

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, new { orgid, status, modificationdate = DateTime.Now });
                return result;
            }
        }

        public async Task<Tuple<IEnumerable<ViewActiveCP>, int>> ViewActiveCP(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            //var query = "select distinct tcc.orgid, tcc.orgcode, tcc.orgname, tcc.orgtype org_type,toy.org_type orgtypename , tcc.parentorgid, tcc.firsttrxdate, tcc.lasttrxdate, \r\ntcc.status, tcc.creator, tcc.productid, tcc.enrollmentfeecharged, \r\ntcc.secdeposit, tcc.latitude, tcc.longitude, tcc.pgsstatus, tcc.preferredlanguage, tcc.classoftrade, tcc.activationdate, \r\ntcc.fieldstaffid, tcc.secdepositchequeno, tcc.secdepositbankid, tcc.secdepositaccount, tcc.paidby, tcc.payment_option, \r\ntcc.onboardstage\r\nfrom onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid \r\nleft join user_management.tbl_um_users tuu on tuu.orgid = toc.orgid\r\nleft join onboarding.tbl_org_type toy on toy.org_type_id  = tcc.orgtype\r\nwhere tcc.status = 2";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var comissionDtls = await dbConnection.QueryAsync<ViewActiveCP>(query);
            //    return comissionDtls.ToList<ViewActiveCP>();
            //}
            pageNumber = pageNumber ?? 1;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Select Count(1)  from onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid \r\nleft join onboarding.tbl_org_type toy on toy.org_type_id  = tcc.orgtype\r\nwhere tcc.status in (2,4) and tcc.orgtype in (2,3,6); ");
            stringBuilder.AppendLine(" select distinct tcc.orgid, tcc.orgcode, tcc.orgname, tcc.orgtype org_type,toy.org_type orgtypename , tcc.parentorgid, tcc.firsttrxdate, tcc.lasttrxdate, \r\ntcc.status, tcc.creator, tcc.productid, tcc.enrollmentfeecharged, \r\ntcc.secdeposit, tcc.latitude, tcc.longitude, tcc.pgsstatus, tcc.preferredlanguage, tcc.classoftrade, tcc.activationdate, \r\ntcc.fieldstaffid, tcc.secdepositchequeno, tcc.secdepositbankid, tcc.secdepositaccount, tcc.paidby, tcc.payment_option, \r\ntcc.onboardstage\r\nfrom onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid \r\nleft join onboarding.tbl_org_type toy on toy.org_type_id  = tcc.orgtype\r\nwhere tcc.status in (2,4) and tcc.orgtype in (2,3,6) ");

            //orderByColumn
            stringBuilder.AppendFormat("order by tcc.{0} ", orderByColumn ?? " orgid ");

            //orderBy
            stringBuilder.AppendFormat(" {0} ", orderBy ?? " asc ");

            if (pageSize != null && pageSize != 0)
            {
                int? limit = pageSize;
                int? offset = (pageNumber - 1) * pageSize;
                stringBuilder.AppendFormat("limit {0} offset {1}", limit, offset);
            }
            var query = stringBuilder.ToString();
            //var query = string.Format("Select Count(1)  from onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid \r\nleft join onboarding.tbl_org_type toy on toy.org_type_id  = tcc.orgtype\r\nwhere tcc.status in (2,4) and tcc.orgtype in (2,3,6);" +
            //     " select distinct tcc.orgid, tcc.orgcode, tcc.orgname, tcc.orgtype org_type,toy.org_type orgtypename , tcc.parentorgid, tcc.firsttrxdate, tcc.lasttrxdate, \r\ntcc.status, tcc.creator, tcc.productid, tcc.enrollmentfeecharged, \r\ntcc.secdeposit, tcc.latitude, tcc.longitude, tcc.pgsstatus, tcc.preferredlanguage, tcc.classoftrade, tcc.activationdate, \r\ntcc.fieldstaffid, tcc.secdepositchequeno, tcc.secdepositbankid, tcc.secdepositaccount, tcc.paidby, tcc.payment_option, \r\ntcc.onboardstage\r\nfrom onboarding.tbl_chm_channelpartners tcc \r\ninner join onboarding.tbl_org_cpdetails toc on tcc.orgid = toc.orgid \r\ninner join onboarding.\"tbl_org_KYCdetails\" tok on tok.orgid = toc.orgid \r\nleft join onboarding.tbl_org_type toy on toy.org_type_id  = tcc.orgtype\r\nwhere tcc.status in (2,4) and tcc.orgtype in (2,3,6) " +
            //     "order by tcc.{0} {1} limit {2} offset {3}", orderByColumn, orderBy, limit, offset);
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().Single();
                    var users = multi.Read<ViewActiveCP>().ToList();
                    return new Tuple<IEnumerable<ViewActiveCP>, int>(users.ToList<ViewActiveCP>(), count);
                }
            }

        }
        public async Task<IEnumerable<ListofChannels>> GetListofChannels(CancellationToken cancellationToken = default)
        {
            var query = "select * from onboarding.tbl_chm_channel where status  = 2";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListofChannels>(query);
                return comissionDtls.ToList<ListofChannels>();
            }
        }
        public async Task<IEnumerable<ListProductsforChannel>> GetProductsforChannel(int? channelId, CancellationToken cancellationToken = default)
        {
            var query = "select * from servicemanagement.tbl_sm_product where status  = 2 and channelId = (CASE WHEN @channelId > 0 THEN @channelId ELSE channelId END)";
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var comissionDtls = await dbConnection.QueryAsync<ListProductsforChannel>(query, new { channelId });
                return comissionDtls.ToList<ListProductsforChannel>();
            }
        }
        public async Task<IEnumerable<getCPDetailsByName>> GetAllCPDetailsByNameAsync(int? orgType, string? orgName, CancellationToken cancellationToken = default)
        {

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("select orgid,orgcode,orgname,status  from onboarding.tbl_chm_channelpartners");
            queryBuilder.Append(" where 1=1 ");

            if (orgType != null)
            {
                queryBuilder.Append($" AND orgType = {orgType} ");
            }

            if (orgName != null)
            {
                queryBuilder.Append($" AND LOWER(orgName) like LOWER('%{orgName}%') ");
            }


            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var cpdetails = await dbConnection.QueryAsync<getCPDetailsByName>(queryBuilder.ToString());
                return (IEnumerable<getCPDetailsByName>)cpdetails;
            }
        }

        public async Task<int> ApproveRejectCP(ApproveRejectCPInfoDto crInfoDto, CancellationToken cancellationToken = default)
        {
            //var query = "update onboarding.tbl_chm_channelpartners set status = @status where orgid = @orgid";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var result = await dbConnection.ExecuteAsync(query, new { orgid, status });
            //    //return result;
            //}

            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendLine($"DO $$");
            commandBuilder.AppendLine($"BEGIN ");
            commandBuilder.AppendLine($"update onboarding.tbl_chm_channelpartners set status= {crInfoDto.confirmRejectCP.statusId} , modificationdate = NOW()  where orgid={crInfoDto.confirmRejectCP.orgId};");
            for (int i = 0; i < crInfoDto.confirmRejectCP.kycStatusDtls.Count; i++)
            {
                commandBuilder.AppendLine($"update onboarding.tbl_org_onboard_status set status= {crInfoDto.confirmRejectCP.kycStatusDtls[i].kycStatusId} , statusdate= NOW(), remarks = '{crInfoDto.confirmRejectCP.kycStatusDtls[i].remarks}' where tbl_org_onboard_status.onboardstate = {crInfoDto.confirmRejectCP.kycStatusDtls[i].onOnboardState}  and orgid={crInfoDto.confirmRejectCP.orgId};");
            }
            commandBuilder.AppendLine($" END$$");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(commandBuilder.ToString());
                result = 1;
                return result;
            }
        }

        public async Task<IEnumerable<CPAquisitionDetail>> GetCPAcquistionDetails(CPAquisitionDetailRequestDto request, CancellationToken cancellationToken = default)
        {
            StringBuilder queryBuilder = new();
            try
            {
                queryBuilder.Append("select count(tcc.orgid) over() as totalcount,  tcc.orgid ,tcc.orgcode ,tcc.orgname ,tot.org_type_id channelType,tsp.productname productName ,\r\ntuu.firstname, tuu.lastname,tuu.mobilenumber, tcc.activationdate , concat(toc.busi_house_no,' ' , toc.busi_road) \"businessAddress\",\r\ntoc.busi_pincode pincode,toc.busi_sub_district city, perm_dist State,tcc.status \r\nfrom onboarding.tbl_chm_channelpartners tcc\r\nleft join user_management.tbl_um_users tuu on tuu.orgid = tcc.orgid and tcc.orgcode = tuu.loginid\r\nleft join onboarding.tbl_org_cpdetails toc  on tcc.orgid = toc.orgid\r\nleft join onboarding.tbl_org_type tot on tcc.orgtype =tot.org_type_id\r\nleft join servicemanagement.tbl_sm_product tsp on tsp.productid  = tcc.productid where tuu.status not in (5)  ");
               
                //if(request.status != 10 ) queryBuilder.Append($" and tcc.status = {request.status}  ");
                if (request.orgId > 0) queryBuilder.Append($" AND tcc.parentorgid = {request.orgId} ");
                if(!string.IsNullOrEmpty(request.searchById)) queryBuilder.AppendLine($" AND tcc.orgcode like '%{request.searchById}%' ");
                if (!string.IsNullOrEmpty(request.productName)) queryBuilder.AppendLine($" AND tsp.productname like '%{request.productName}%' ");
                if (!string.IsNullOrEmpty(request.searchByName)) queryBuilder.AppendLine($" AND tcc.orgname like '%{request.searchByName}%' ");
                if (!string.IsNullOrEmpty(request.fromDate)) queryBuilder.AppendLine($" AND tcc.creationdate  >= '{request.fromDate:yyyy-MM-dd}'::date ");     
                if (!string.IsNullOrEmpty(request.toDate)) queryBuilder.AppendLine($" AND tcc.creationdate  <= '{request.toDate:yyyy-MM-dd}'::date ");

                queryBuilder.AppendLine(" ORDER BY   tcc.orgid asc ");
                if (request.pageSize > 0)
                {
                    int? limit = request.pageSize;
                    int? offset = (request.pageNumber - 1) * request.pageSize;
                    queryBuilder.AppendLine($" LIMIT {limit} OFFSET {offset}   ;");
                }
               
                string query = queryBuilder.ToString();
                using (IDbConnection dbConnection = _context.CreateConnection())
                {
                    dbConnection.Open();
                    var users = await dbConnection.QueryAsync<CPAquisitionDetail>(query);
                    return users;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        Task<Acquisition> IGenericRepository<Acquisition>.AddAsync(Acquisition entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        Task<Acquisition> IGenericRepository<Acquisition>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Acquisition>> IGenericRepository<Acquisition>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Acquisition> IGenericRepository<Acquisition>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Acquisition> IGenericRepository<Acquisition>.UpdateAsync(Acquisition entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private string GenerateRandomOTP()
        {
            string OTPLength = "6";
            string NewCharacters = "";
            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            string IDString = "";
            string temp = "";

            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewCharacters = IDString;
            }

            return NewCharacters;
        }

        public async Task<AddAcquisitionResponse> CreateCPByDistributor(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "\"onboarding\".stp_org_createcpbydistributor";
                var parameters = new DynamicParameters();

                 

                parameters.Add("p_orgname", addAcquisitionDto.orgname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_orgtype", addAcquisitionDto.orgtype, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_parentorgid", addAcquisitionDto.parentOrgId, DbType.Int32, ParameterDirection.Input);
                //parameters.Add("p_status", addAcquisitionDto.status, DbType.Int32, ParameterDirection.Input);//0
                parameters.Add("p_status", 0, DbType.Int32, ParameterDirection.Input);//0
                parameters.Add("p_creator", addAcquisitionDto.creator, DbType.Int32, ParameterDirection.Input);//0
                parameters.Add("p_productid", addAcquisitionDto.productId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_secdeposit", addAcquisitionDto.secdeposit, DbType.VarNumeric, ParameterDirection.Input);
                parameters.Add("p_fieldstaffid", addAcquisitionDto.fieldStaffId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_secdepositchequeno", addAcquisitionDto.secdepositchequeno, DbType.String, ParameterDirection.Input);
                parameters.Add("p_secdepositbankid", addAcquisitionDto.secDepositBankId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_secdepositaccount", addAcquisitionDto.secdepositaccount, DbType.String, ParameterDirection.Input);
                parameters.Add("p_paidby", addAcquisitionDto.paidBy, DbType.String, ParameterDirection.Input);
                parameters.Add("p_payment_option", addAcquisitionDto.payment_option, DbType.String, ParameterDirection.Input);
                //parameters.Add("p_onboardstage", addAcquisitionDto.onboardstage, DbType.Int32, ParameterDirection.Input);//0
                //parameters.Add("p_kyc_status", addAcquisitionDto.kycstatus , DbType.Int32, ParameterDirection.Input);//1
                parameters.Add("p_onboardstage", 0, DbType.Int32, ParameterDirection.Input);//0
                parameters.Add("p_kyc_status", 0, DbType.Int32, ParameterDirection.Input);//1
                parameters.Add("p_busi_sub_district", addAcquisitionDto.busi_sub_district, DbType.String, ParameterDirection.Input);
                parameters.Add("p_firstname", addAcquisitionDto.fName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_middlename", addAcquisitionDto.mName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_lastname", addAcquisitionDto.lName, DbType.String, ParameterDirection.Input);
                parameters.Add("p_loginid", addAcquisitionDto.orgcode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_password", addAcquisitionDto.password, DbType.String, ParameterDirection.Input);
                parameters.Add("p_emailid", addAcquisitionDto.emailid, DbType.String, ParameterDirection.Input);
                parameters.Add("p_mobilenumber", addAcquisitionDto.mobileNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("p_needspasschange", addAcquisitionDto.needspasschange, DbType.Int32, ParameterDirection.Input);//1
                //parameters.Add("p_needspasschange", 1, DbType.Int32, ParameterDirection.Input);//1
                parameters.Add("p_roleid", addAcquisitionDto.roleid, DbType.Int32, ParameterDirection.Input);//5
                parameters.Add("p_failedcount", addAcquisitionDto.failedcount, DbType.Int32, ParameterDirection.Input);//0
                parameters.Add("p_ispasscodeexists", addAcquisitionDto.ispasscodeexists, DbType.Int32, ParameterDirection.Input);//0
                //parameters.Add("p_failedcount", 0, DbType.Int32, ParameterDirection.Input);//0
                //parameters.Add("p_ispasscodeexists", 0, DbType.Int32, ParameterDirection.Input);//0
                parameters.Add("p_usertypeid", addAcquisitionDto.usertypeid, DbType.Int32, ParameterDirection.Input);//2
                parameters.Add("p_channelid", addAcquisitionDto.channels, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_cpafnumber", addAcquisitionDto.cpafnumber, DbType.Int32, ParameterDirection.Input);
                

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
        public async Task<IEnumerable<CityAreaModel>> GetCityAreaByPinCode(int PinCode, CancellationToken cancellationToken = default)
        {
            string query = @"select c.cityareaid, c.cityareaname, c.pincode, c.cityname, c.pincodeid,  tms.stateid, tms.statename, tmc2.countryid, tmc2.conuntryname as countryname, tmc2.countrycode from common.tbl_mst_cityarea c inner join common.tbl_mst_state tms on tms.stateid = c.stateid inner join common.tbl_mst_country tmc2 on tmc2.countrycode = coalesce(CAST(tms.countryid AS text), 'N/A') where c.pincode = @PinCode";

            var paramas = new
            {
                pincode = PinCode
            };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<CityAreaModel>(query, paramas);
                return result;
            }
        }

        public async Task<IEnumerable<CpafNumber>> GetCpafByOrgid(long orgid, CancellationToken cancellationToken = default)
        {
            string query = @"select cpafnnumber from ""Inventory"".tbl_sc_inventory_dtls tdp inner join ""Inventory"".tbl_sc_inventory inv on tdp.inventoryid =inv.""inventoryId"" where inv.orgid =@orgid and tdp.orgid =0;";
            var paramas = new
            {
                orgid = orgid
            };
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<CpafNumber>(query, paramas);
                return result;
            }
        }

        public async Task<int> ApproveCPByActivateDate(int orgid, int status, string activationdate, CancellationToken cancellationToken = default)
        {
            //var query = "update onboarding.tbl_chm_channelpartners set status = @status ,   where orgid = @orgid";
            //var query = "update onboarding.tbl_chm_channelpartners set status = @status ,activationdate = (CASE WHEN @status = '2' THEN @activationdate ELSE activationdate END)  where orgid = @orgid";
            //using (IDbConnection dbConnection = _context.CreateConnection())
            //{
            //    dbConnection.Open();
            //    var result = await dbConnection.ExecuteAsync(query, new { orgid, status, activationdate = activationdate });
            //    return result;
            //}

            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendLine($"DO $$");
            commandBuilder.AppendLine($"BEGIN ");
            commandBuilder.AppendLine($"update onboarding.tbl_chm_channelpartners set status= {status} where orgid={orgid};");
            if (status == 2)
            {
                commandBuilder.AppendLine($"update onboarding.tbl_chm_channelpartners set status= {status} ,activationdate = '{activationdate}' where orgid={orgid};");
                commandBuilder.AppendLine($"update user_management.tbl_um_users  set status= {status}  where orgid = {orgid} and usertypeid in (2,4,6);");
            }
            commandBuilder.AppendLine($" END$$");
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(commandBuilder.ToString());
                result = 1;
                return result;
            }


        }
        public async Task<IEnumerable<DynamicSearchCpApplicationModel>> GetDynamicSearchCP(DynamicSearchModelCP entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var procedureName = "onboarding.stp_chm_getverifycpapplication";
                var parameters = new DynamicParameters();
                parameters.Add("p_searchoption", entity.p_searchoption, DbType.String, ParameterDirection.Input);
                parameters.Add("p_offsetrows", entity.p_offsetrows, DbType.Int16, ParameterDirection.Input);
                parameters.Add("p_fetchrows", entity.p_fetchrows, DbType.Int16, ParameterDirection.Input);

                parameters.Add("p_fromdate", string.IsNullOrEmpty(entity.p_fromDate) ? null: Convert.ChangeType(entity.p_fromDate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);
                parameters.Add("p_todate", string.IsNullOrEmpty(entity.p_toDate) ? null: Convert.ChangeType(entity.p_toDate, typeof(DateTime)), DbType.Date, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    var reports = await connection.QueryAsync<DynamicSearchCpApplicationModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return reports;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<IEnumerable<CPAquisitionReportDetails>, int>> GetCPAquisitionReportDetails(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            pageNumber = pageNumber ?? 1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SELECT COUNT(1) FROM onboarding.tbl_chm_channelpartners tcc INNER JOIN user_management.tbl_um_users tuu ON tcc.orgid = tuu.orgid LEFT JOIN user_management.tbl_um_users parents ON parents.orgid = tcc.parentorgid LEFT JOIN onboarding.tbl_chm_channelpartners parent ON parent.orgid = tcc.parentorgid LEFT JOIN user_management.tbl_um_users superName ON superName.orgid = parent.parentorgid LEFT JOIN onboarding.tbl_chm_channelpartners super ON super.orgid = tcc.parentorgid LEFT JOIN user_management.tbl_um_users fieldstaff ON fieldstaff.orgid = tcc.fieldstaffid INNER JOIN onboarding.\"tbl_org_KYCdetails\" tok ON tcc.orgid = tok.orgid INNER JOIN onboarding.tbl_org_cpdetails toc ON tcc.orgid = toc.orgid LEFT JOIN common.tbl_mst_cityarea tmc ON CAST(toc.busi_pincode AS INTEGER) = tmc.pincode LEFT JOIN common.tbl_mst_state tms ON tmc.stateid = tms.stateid LEFT JOIN onboarding.tbl_org_onboard_status tos ON tcc.orgid = tos.orgid left JOIN servicemanagement.tbl_sm_product tsp on tcc.productid = tsp.productid WHERE DATE(tcc.activationdate) = CURRENT_DATE - INTERVAL '5 day' AND tcc.orgId = tuu.orgId AND tcc.orgId = tok.orgId AND tok.orgId = tuu.orgId ;");
            stringBuilder.AppendLine("SELECT DISTINCT tcc.creationdate as AcquisitionDate, tcc.fieldstaffid as EmpID, Concat( fieldstaff.firstname,' ', fieldstaff.middlename,' ', fieldstaff.lastname) as EmpName, tcc.orgid as CPID, tcc.orgcode as CPCode, Concat( tuu.firstname,' ', tuu.middlename,' ', tuu.lastname) as CPName, tcc.orgname as ShopName, tuu.mobilenumber as MobileNo, tuu.emailid as EmailId, parent.orgid as ParentId, parent.orgcode as ParentOrgCode, Concat( parents.firstname,' ', parents.middlename,' ', parents.lastname) as ParentName, super.parentorgid as SuperId, Concat( superName.firstname,' ', superName.middlename,' ', superName.lastname) as SuperName, tok.modificationdate as ActivationDate, toc.busi_pincode as PinCode, tms.statename as State, toc.perm_house_no as Address, tok.ref_param1 as Latitude, tok.ref_param2 as Longitude, tok.pancard as PAN, case \t WHEN tos.status = 0 THEN 'Not Loged In' WHEN tos.status = 1 THEN 'Pending' WHEN tos.status = 2 THEN 'Activated' WHEN tos.status = 3 THEN 'Rejected'  WHEN tos.status = 4 THEN 'Disabled'  ELSE '' END AS Status, tsp.productname as ProductName FROM onboarding.tbl_chm_channelpartners tcc INNER JOIN user_management.tbl_um_users tuu ON tcc.orgid = tuu.orgid LEFT JOIN user_management.tbl_um_users parents ON parents.orgid = tcc.parentorgid LEFT JOIN onboarding.tbl_chm_channelpartners parent ON parent.orgid = tcc.parentorgid LEFT JOIN user_management.tbl_um_users superName ON superName.orgid = parent.parentorgid LEFT JOIN onboarding.tbl_chm_channelpartners super ON super.orgid = tcc.parentorgid LEFT JOIN user_management.tbl_um_users fieldstaff ON fieldstaff.orgid = tcc.fieldstaffid INNER JOIN onboarding.\"tbl_org_KYCdetails\" tok ON tcc.orgid = tok.orgid INNER JOIN onboarding.tbl_org_cpdetails toc ON tcc.orgid = toc.orgid LEFT JOIN common.tbl_mst_cityarea tmc ON CAST(toc.busi_pincode AS INTEGER) = tmc.pincode LEFT JOIN common.tbl_mst_state tms ON tmc.stateid = tms.stateid LEFT JOIN onboarding.tbl_org_onboard_status tos ON tcc.orgid = tos.orgid left JOIN servicemanagement.tbl_sm_product tsp on tcc.productid = tsp.productid WHERE DATE(tcc.activationdate) = CURRENT_DATE - INTERVAL '5 day' AND tcc.orgId = tuu.orgId AND tcc.orgId = tok.orgId AND tok.orgId = tuu.orgId ");
            
            //orderByColumn
            stringBuilder.AppendFormat(" ORDER BY {0} ", orderByColumn ?? " tcc.creationdate ");

            //orderBy
            stringBuilder.AppendFormat(" {0} ", orderBy ?? " DESC ");

            //Limit and Offset
            if (pageSize != null && pageSize != 0)
            {
                int? limit = pageSize;
                int? offset = (pageNumber - 1) * pageSize;
                stringBuilder.AppendFormat(" limit {0} offset {1} ", limit, offset);
            }

            var query = stringBuilder.ToString();

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                using (var multi = await dbConnection.QueryMultipleAsync(query))
                {
                    var count = multi.Read<int>().SingleOrDefault();
                    var users = multi.Read<CPAquisitionReportDetails>().ToList();
                    return new Tuple<IEnumerable<CPAquisitionReportDetails>, int>(users.ToList<CPAquisitionReportDetails>(), count);
                }
            }
        }
    }
}
