using Contracts;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Middleware;
using PublicAPI.Model;
using PublicAPI.Utility;
using Services.Abstractions;
using System.Net;
using Mapster;
using System.Security.Claims;
using Contracts.UserManager;
using Contracts.Account;
using Contracts.Common;
using System.Security.Cryptography;
using System.Text;
using Contracts.CpOnBoard;
using Newtonsoft.Json;
using Contracts.WorkingCapital;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public AccountController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet]
        public async Task<ActionResult> Get(string loginId, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.GetUserByLoginIdAsync(loginId, cancellationToken);

            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet]
        public async Task<ActionResult> GetUserByUserId(long userid, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.GetUserByUserIdAsync(userid, cancellationToken);

            return Ok(userResponseModel);
        }
        /// <summary>
        /// Check if mobile number is already in use
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> IsMobileNumberAlreadyInUse(string mobileNumber, int roleId, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.IsMobileNumberAlreadyInUse(mobileNumber, roleId, cancellationToken);

            return Ok(userResponseModel);
        }



        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet(Name = "GetAllUser")]
        public async Task<ActionResult> GetAllAsync(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.GetAllUsersAsync(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);

            return Ok(userResponseModel);
        }

        /// <summary>
        /// Get All User Query
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet(Name = "CheckDuplicateUser")]
        public async Task<ActionResult> CheckDuplicateUser(string? loginid, string? emailid, string? mobilenumber, CancellationToken cancellationToken = default)
        {
            var userResponseModel = await _serviceManager.UsersService.GetUserAsyncAsync(loginid, emailid, mobilenumber, cancellationToken);

            return Ok(userResponseModel);
        }


        /// <summary>
        /// get token
        /// </summary>
        /// <param name="userLogins"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>response model</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GetToken(UserLogins userLogins, CancellationToken cancellationToken)
        {
            var Token = new UserTokens();
            byte[] key = Encoding.ASCII.GetBytes("PLAIN_7PAYAGENTS");
            byte[] IV = Encoding.ASCII.GetBytes("8119745113154120");//new byte[16] { 8, 1, 1, 9, 7, 4, 5, 1, 1, 3, 1, 5, 4, 1, 2, 0 };


            string DecPw = DecryptString(userLogins.Password, key, IV);
            if (!string.IsNullOrEmpty(DecPw))
            {
                userLogins.Password = ComputeHash(DecPw);
            }

            var userResponseModel = await _serviceManager.UsersService.GetAuthenticateUserAsync(userLogins.UserName, userLogins.Password, cancellationToken);
            if (userResponseModel.Data != null)
            {
                Token = JwtHelpers.GenTokenkey(new UserTokens()
                {
                    EmailId = userResponseModel.Data.EmailId,
                    GuidId = Guid.NewGuid(),
                    UserName = userResponseModel.Data.LoginId,
                    Id = userResponseModel.Data.UserId,
                    OrgId = userResponseModel.Data.OrgId,
                    Status = userResponseModel.Data.Status,
                    RoleId = userResponseModel.Data.RoleId,
                    FirstName = userResponseModel.Data.FirstName,
                    MiddleName = userResponseModel.Data.MiddleName,
                    LastName = userResponseModel.Data.LastName,
                    MobileNumber = userResponseModel.Data.MobileNumber,
                    InfiniteCreditLimit = userResponseModel.Data.InfiniteCreditLimit,

                    UserId = userResponseModel.Data.UserId,
                }, jwtSettings, _serviceManager.RoleService, userResponseModel.Data.RoleId);

                userResponseModel.Data = Token;
            }
            return Ok(userResponseModel);
        }
        /// <summary>
        /// get token
        /// </summary>
        /// <param name="userLogins"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>response model</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> GetLoginToken(UserLogins userLogins, CancellationToken cancellationToken)
        {

            var Token = new UserTokens();

            var ipAddress = _ipAddressHelper.GetClientIpAddress(Request.HttpContext);
            byte[] key = Encoding.ASCII.GetBytes("PLAIN_7PAYAGENTS");
            byte[] IV = Encoding.ASCII.GetBytes("8119745113154120");//new byte[16] { 8, 1, 1, 9, 7, 4, 5, 1, 1, 3, 1, 5, 4, 1, 2, 0 };


            string DecPw = DecryptString(userLogins.Password, key, IV);
            if (!string.IsNullOrEmpty(DecPw))
            {
                userLogins.Password = ComputeHash(DecPw);
            }
            var userResponseModel = await _serviceManager.UsersService.GetAuthenticateUsersAsync(userLogins.UserName, userLogins.Password, ipAddress, cancellationToken);

            if (userResponseModel.Data != null)
            {
                Token = JwtHelpers.GenLoginTokenkey(new UserTokens()
                {
                    EmailId = userResponseModel.Data.EmailId,
                    GuidId = Guid.NewGuid(),
                    UserName = userResponseModel.Data.UserName,
                    MobileNumber = userResponseModel.Data.MobileNumber,
                    Id = userResponseModel.Data.UserId,
                }, jwtSettings, userResponseModel);

                userResponseModel.Data = Token;
            }
            return Ok(userResponseModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> GetDistToken(UserLogins userLogins, CancellationToken cancellationToken)
        {
            var Token = new UserTokens();
            var ipAddress = _ipAddressHelper.GetClientIpAddress(Request.HttpContext);
            var userResponseModel = await _serviceManager.UsersService.GetAuthenticateUserAsync(userLogins.UserName, userLogins.Password, ipAddress, cancellationToken);
            if (userResponseModel.Data != null)
            {
                Token = JwtHelpers.GenTokenkey(new UserTokens()
                {
                    EmailId = userResponseModel.Data.EmailId,
                    GuidId = Guid.NewGuid(),
                    UserName = userResponseModel.Data.UserName,
                    MobileNumber = userResponseModel.Data.MobileNumber,
                    Id = userResponseModel.Data.UserId,
                    OrgId = userResponseModel.Data.OrgId,
                }, jwtSettings, userResponseModel);
                
                userResponseModel.Data = Token;
                var TokenResponse = userLogins.Adapt<UpdateUsersTokenDto>();
                TokenResponse.ipaddress = ipAddress;
                TokenResponse.userid = userResponseModel.Data.UserId;
                var TokenInsert = userLogins.Adapt<InsertTokenDto>();
                TokenInsert.p_userid = userResponseModel.Data.UserId;
                TokenInsert.p_ipaddress = ipAddress;
                TokenInsert.p_gettoken = Token.Token;
                var tokenindb = await _serviceManager.UsersService.GetTokenDataByUseridAsync(userResponseModel.Data.UserId, ipAddress);
                if (tokenindb != null)
                {
                    await _serviceManager.UsersService.UpdateUserTokenAsync(TokenResponse, cancellationToken);
                }
                await _serviceManager.UsersService.InsertTokenAsync(TokenInsert, cancellationToken);
            }
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserModel user, CancellationToken cancellationToken)
        {
            string ipAddress = _ipAddressHelper.GetClientIpAddress(Request.HttpContext);
            UsersDto userDto = user.Adapt<UsersDto>();
            userDto.IPAddress = ipAddress;
            userDto.CreatedBy = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            userDto.CreatedOn = DateTime.Now;
            userDto.CreationDate = DateTime.Now;
            userDto.Creator = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var userResponseModel = await _serviceManager.UsersService.AddAsync(userDto, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(UpdateUsersDto user, CancellationToken cancellationToken)
        {
            string ipAddress = _ipAddressHelper.GetClientIpAddress(Request.HttpContext);


            UpdateUsersDto updateUsersDto = user.Adapt<UpdateUsersDto>();
            updateUsersDto.IPAddress = ipAddress;
            updateUsersDto.CreatedBy = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            updateUsersDto.UpdatedBy = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var userResponseModel = await _serviceManager.UsersService.UpdateUserAsync(updateUsersDto, cancellationToken);
            return Ok(userResponseModel);
        }


        /// <summary>
        /// Deactivate user
        /// </summary>
        /// <returns>result of deactivate user </returns>
        [HttpDelete]
        public async Task<ActionResult> ActivateOrDeActivateUser(int userId, int status, CancellationToken cancellationToken)
        {
            var responseModelDto = await _serviceManager.UsersService.ActivateOrDeActivateUser(userId, status, cancellationToken);
            return Ok(responseModelDto);

        }



        /// <summary>
        /// Forgot password generate otp
        /// </summary>
        /// <returns>Status of otp </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GenerateOTPToForgotPassword(string loginid, CancellationToken cancellationToken)
        {
            var responseModelDto = await _serviceManager.UsersService.GenerateOTPToForgotPassword(loginid, cancellationToken);
            return Ok(responseModelDto);

        }

        /// <summary>
        /// Reset password 
        /// </summary>
        /// <returns>Status of reset password</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string loginid, string otp, string password, CancellationToken cancellationToken)
        {
            var responseModelDto = await _serviceManager.UsersService.ResetPassword(loginid, otp, password, cancellationToken);
            return Ok(responseModelDto);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> DistResetPassword(string loginid, string otp, string password, CancellationToken cancellationToken)
        {
            var responseModelDto = await _serviceManager.UsersService.DistResetPassword(loginid, otp, password, cancellationToken);
            return Ok(responseModelDto);

        }

        /// <summary>
        /// Reset password 
        /// </summary>
        /// <returns>Status of reset password</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AdminResetPassword(string loginid, CancellationToken cancellationToken)
        {
            var responseModelDto = await _serviceManager.UsersService.ResetPassword(loginid, cancellationToken);
            return Ok(responseModelDto);

        }
        /// <summary>
        /// Resend password 
        /// </summary>
        /// <returns> resend password</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResendPassword(string loginid, CancellationToken cancellationToken)
        {
            var responseModelDto = await _serviceManager.UsersService.ResetPassword(loginid, cancellationToken);
            return Ok(responseModelDto);

        }
        [HttpGet(Name = "ViewLedgerAccount")]
        public async Task<ActionResult> ViewLedgerAccount(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.ViewLedgerAccount(cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetListofOrgType")]
        public async Task<ActionResult> GetListofOrgType(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListofOrgType(cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPost]
        public async Task<ActionResult> CreateLedgerAccount(CreateLedgerAccountDto ledgerAccountDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.CreateLedgerAccount(ledgerAccountDto, cancellationToken);
            return Ok(serviceCreateModel);
        }

        //Add Ledger Account
        [HttpPost]
        public async Task<ActionResult> AddLedgerAccount(AddLedgerAccDto addLedgerAccDto, CancellationToken cancellationToken)
        {
            var serviceAddLedgerAccount = await _serviceManager.AccountService.AddLedgerAccount(addLedgerAccDto, cancellationToken);


            // Create a response containing both results
            var response = new
            {
                AddLedgerAccountResult = serviceAddLedgerAccount
            };

            return Ok(response);
        }

        //// Get Add Ledger Account
        [HttpGet]
        public async Task<ActionResult> GetAddLedgerAccount(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var serviceGetLedgerAccount = await _serviceManager.AccountService.GetAddLedgerAccount(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
            return Ok(serviceGetLedgerAccount);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateLedgerAccount(UpdateLedgerAccountDto updateLedgerAccountDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.UpdateLedgerAccount(updateLedgerAccountDto, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpGet(Name = "GetLedgerAccount")]
        public async Task<ActionResult> GetLedgerAccount(int? Id, string? Value, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetLedgerAccount(Id, Value, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet(Name = "GetListofAccType")]
        public async Task<ActionResult> GetListofAccType(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListofAccType(cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddJV(AddJVDto addJVDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.AddJV(addJVDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> AddJVDetails(JVDetailsDto addJVDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.AddJVDetails(addJVDto, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateJVDetailsId(UpdateJVDetailDto updateJVDetailDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.UpdateJVDetailsId(updateJVDetailDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> RemoveJV(UpdateJVDetailDto updateJVDetailDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.RemoveJV(updateJVDetailDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> ApproveRejectJV(ApproveRejectJVDto updateJVDetailDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.ApproveRejectJV(updateJVDetailDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost]
        public async Task<ActionResult> ApproveAndRejectJV(ApproveRejectJVDto addJVDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.ApproveAndRejectJV(addJVDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "GetListofWCRequest")]
        public async Task<ActionResult> GetListofWCRequest(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListofWCRequest(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "ViewBankClaimDeposits")]
        public async Task<ActionResult> ViewBankClaimDeposits(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.ViewBankClaimDeposits(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "ViewBankClaimDepositsWithFiltter")]
        public async Task<ActionResult> ViewBankClaimDepositsWithFiltter(int paymentmode, int status, string? orgcode, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            var userResponseModel = await _serviceManager.AccountService.ViewBankClaimDepositsWithFiltterasync(paymentmode, status, orgcode, fromDate, toDate, p_offsetrows, p_fetchrows, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPost]
        public async Task<ActionResult> ApproveRejectWC(ApproveRejectWCDto approveRejectWCDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.ApproveRejectWC(approveRejectWCDto, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpGet(Name = "GetListofJVDetailsByJVNo")]
        public async Task<ActionResult> GetListofJVDetailsByJVNo(int? jvno, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListofJVDetailsByJVNo(jvno, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetListofJVDetails")]
        public async Task<ActionResult> GetListofJVDetails(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListofJVDetails( pageSize,  pageNumber,  orderByColumn, orderBy, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet(Name = "GetListAccByOrgeTypeId")]
        public async Task<ActionResult> GetListAccByOrgeTypeId(int OrgTypeId,CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListAccByOrgeTypeId(OrgTypeId, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateJVByJVDetailID(UpdateJVDetailDto updateJVDetailDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.UpdateJVByJVDetailID(updateJVDetailDto, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpGet(Name = "GetListofJV")]
        public async Task<ActionResult> GetListofJV(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.AccountService.GetListofJV(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpGet("GetAllActiveUser/{roleId}")]
        public async Task<ResponseModelDto> GetAllActiveUser(int roleId, CancellationToken cancellationToken)
        {
            var role = await _serviceManager.UsersService.GetAllActiveUserAsync(roleId, cancellationToken);

            return role;
        }
        [HttpGet(Name = "GetUserBySearchFiltter")]
        public async Task<ActionResult> GetUserBySearchFiltter(string? loginid, string? emailid, string? mobilenumber, string? firstname, int? roleid, int? status, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.GetUserBySearchFiltterAsync(loginid, emailid, mobilenumber, firstname, roleid, status, p_offsetrows, p_fetchrows, cancellationToken);
            return Ok(userResponseModel);
        }
        [HttpPost(Name = "GetDynamicSearchUser")]
        public async Task<ActionResult> GetDynamicSearchUser(DynamicSearchUserDto entity, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.GetDynamicSearchUserAsync(entity, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetAccountDetailsByOrgID(int Offsetrows, int Fetchrows, int OrgtypeId, string? Accdescription, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.AccountService.GetAccountDetailsByOrgID(Offsetrows,Fetchrows,OrgtypeId, Accdescription, cancellationToken);
            return Ok(channelPartnerList);
        }
        [HttpPost]
        public async Task<ActionResult> AddAccount(AddAccountDto addaccDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.AccountService.AddAccount(addaccDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllLedgerDetailsById(int transactionid, string code, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.AccountService.GetAllLedgerDetailsById(transactionid, code, cancellationToken);
            return Ok(channelPartnerList);
        }
        [HttpGet]
        public async Task<ActionResult> GetSevenPayBankList(long orgid, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.AccountService.GetSevenPayBankList(orgid, cancellationToken);
            return Ok(channelPartnerList);
        }
        [HttpGet]
        public async Task<ActionResult> GetBanksInfoIFSCCode(string ifscCode, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.AccountService.GetBanksInfoIFSCCode(ifscCode, cancellationToken);
            return Ok(channelPartnerList);
        }
        [HttpPost(Name = "UpdateUserMobileAndEmailAsync")]
        public async Task<ActionResult> UpdateUserMobileAndEmailAsync(ResetMobileAndEmailModelDto entity, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.UsersService.UpdateUserMobileAndEmail(entity, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPost(Name = "InitiatePayment")]
        public async Task<ActionResult> InitiatePayment(PaymentInModelDto paymentInModelDto, CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string baseURL = configurationBuilder.GetSection("APIURL")["TransAPIURL"];
            string userid = configurationBuilder.GetSection("TransToken")["UserId"];
            string pass = configurationBuilder.GetSection("TransToken")["Pass"];
            string urlWorkingCapital = "api/Payment/InitiatePayment";
            string apiResponse = "";
            string strToken = GetMethodResponse(baseURL + "api/Account/GetToken/Login", "{\"userName\":\"" + userid + "\",\"password\":\"" + pass + "\"}", "POST", "");
            if (!strToken.Contains("Error"))
            {
                CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                strToken = cpOnBoardToken.data.token;
                if (!string.IsNullOrEmpty(strToken))
                {
                    WCInitiatePaymentDto wcInitiatePaymentDto = new WCInitiatePaymentDto
                    {
                        p_orgid = paymentInModelDto.p_orgid,
                        p_paymentmode = paymentInModelDto.p_paymentmode,
                        p_amount = paymentInModelDto.p_amount,
                        p_bankid = paymentInModelDto.p_bankid,
                        p_pgid = paymentInModelDto.p_pgid,
                        p_bankaccount = paymentInModelDto.p_bankaccount,
                        p_status = paymentInModelDto.p_status,
                        p_remark = paymentInModelDto.p_remark,
                        p_issueingbankid = paymentInModelDto.p_issueingbankid,
                        p_creator = paymentInModelDto.p_creator,
                        p_bankpayinslip = paymentInModelDto.p_bankpayinslip,
                        p_instrumentnumber = paymentInModelDto.p_instrumentnumber,
                        p_issuingifsccode = paymentInModelDto.p_issuingifsccode,
                        p_vpa = paymentInModelDto.p_vpa,
                        p_fileFormat = paymentInModelDto.p_fileFormat,
                        p_depositDate = paymentInModelDto.p_depositDate,
                        p_depositTime = paymentInModelDto.p_depositTime
                    };
                    string strData = JsonConvert.SerializeObject(wcInitiatePaymentDto);
                    apiResponse = GetMethodResponse(baseURL + urlWorkingCapital, strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "UPIUpdatePayment")]
        public async Task<ActionResult> UPIUpdatePayment(PaymentUpdateInModelDto paymentInModelDto, CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string baseURL = configurationBuilder.GetSection("APIURL")["TransAPIURL"];
            string userid = configurationBuilder.GetSection("TransToken")["UserId"];
            string pass = configurationBuilder.GetSection("TransToken")["Pass"];
            string apiResponse = "";
            string strToken = GetMethodResponse(baseURL + "api/Account/GetToken/Login", "{\"userName\":\"" + userid + "\",\"password\":\"" + pass + "\"}", "POST", "");
            if (!strToken.Contains("Error"))
            {
                CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                strToken = cpOnBoardToken.data.token;
                if (!string.IsNullOrEmpty(strToken))
                {
                    string strData = JsonConvert.SerializeObject(paymentInModelDto);
                    apiResponse = GetMethodResponse(baseURL + "api/Payment/UPIUpdatePayment", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "GetBanksInfo")]
        public async Task<ActionResult> GetBanksInfo(string searchString, CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string baseURL = configurationBuilder.GetSection("APIURL")["TransAPIURL"];
            string userid = configurationBuilder.GetSection("TransToken")["UserId"];
            string pass = configurationBuilder.GetSection("TransToken")["Pass"];
            string apiResponse = "";
            string strToken = GetMethodResponse(baseURL + "api/Account/GetToken/Login", "{\"userName\":\"" + userid + "\",\"password\":\"" + pass + "\"}", "POST", "");
            if (!strToken.Contains("Error"))
            {
                CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                strToken = cpOnBoardToken.data.token;
                if (!string.IsNullOrEmpty(strToken))
                {
                    //string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/DMTTransaction/GetBanksInfo?searchString=" + searchString, "", "GET", strToken);
                }
            }
            return Ok(apiResponse);
        }
        [HttpGet]
        public async Task<ActionResult> GetUserByMobileAndUsertypeAsync(string mobileNumber, int usertypeid, CancellationToken cancellationToken)
        {
            var channelPartnerList = await _serviceManager.UsersService.GetUserByMobileAndUsertypeAsync( mobileNumber,  usertypeid, cancellationToken);
            return Ok(channelPartnerList);
        }


        [HttpPost(Name = "ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string baseURL = configurationBuilder.GetSection("APIURL")["TransAPIURL"];
            string userid = configurationBuilder.GetSection("TransToken")["UserId"];
            string pass = configurationBuilder.GetSection("TransToken")["Pass"];
            string apiResponse = "";
            string strToken = GetMethodResponse(baseURL + "api/Account/GetToken/Login", "{\"userName\":\"" + userid + "\",\"password\":\"" + pass + "\"}", "POST", "");
            if (!strToken.Contains("Error"))
            {
                CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                strToken = cpOnBoardToken.data.token;
                if (!string.IsNullOrEmpty(strToken))
                {
                    string strData = JsonConvert.SerializeObject(changePasswordDto);
                    apiResponse = GetMethodResponse(baseURL + "api/Account/ChangePassword/ChangePassword", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchJV(DynamicSearchJVRequestDto entity, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.AccountService.GetDynamicSearchJV(entity, cancellationToken);

            return Ok(serviceResponseModel);
        }

        [HttpGet(Name = "GetPayoutReport")]
        public async Task<ActionResult> GetPayoutReport(string? orgcode, int status, DateTime fromDate, DateTime toDate, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            var userResponseModel = await _serviceManager.AccountService.GetPayoutReportAsync(orgcode, status, fromDate, toDate, p_offsetrows, p_fetchrows, cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpPut]
        public async Task<ActionResult> DeactivePageStaus(string? pageCode, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.UsersService.DeactivePageStaus(pageCode, cancellationToken);
            return Ok(serviceResponseModel);
        }

        [HttpPut]
        public async Task<ActionResult> EditPageDetail(EditPageDetailsDto editPageDetailsDto, string? pageCode, CancellationToken cancellationToken)
        {
            var serviceResponseModel = await _serviceManager.UsersService.EditPageDetail(editPageDetailsDto, pageCode, cancellationToken);
            return Ok(serviceResponseModel);
        }
        private string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            //encryptor.KeySize = 256;
            //encryptor.BlockSize = 128;
            encryptor.Padding = PaddingMode.PKCS7;
            // Set key and IV
            encryptor.Key = key;
            encryptor.IV = iv;
            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();
            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            // Will contain decrypted plaintext
            string plainText = String.Empty;
            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                // Complete the decryption process
                cryptoStream.FlushFinalBlock();
                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();
                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }
            // Return the decrypted data as a string
            return plainText;
        }

        private string EncryptStringToBytes_Aes(string plainText)
        {
            //SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = Encoding.ASCII.GetBytes("PLAIN_7PAYAGENTS");
            //byte[] IV = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }; 8119745113154120
            byte[] IV = Encoding.ASCII.GetBytes("8119745113154120");//new byte[16] { 8,1,1,9,7,4,5,1,1,3,1,5,4,1,2,0 };

            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        private byte[] ComputeHash(byte[] password)
        {
            byte[] temp = null;
            using (SHA512 sha = new SHA512Managed())
            {
                temp = sha.ComputeHash(password);
            }
            return temp;
        }

        private string ComputeHash(string input)
        {
            string hashString = "";
            try
            {
                string saltedKey = input.Trim();//.ToLower(System.Globalization.CultureInfo.InvariantCulture);
                hashString = BitConverter.ToString(ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedKey)));
            }
            catch (Exception exception)
            {
            }
            return hashString;
        }

        //private string GetMethodResponse(string URL, string data, string Type, string token)
        //{
        //    String responseString = "";
        //    try
        //    {
        //        WebRequest oRequest = System.Net.WebRequest.Create(URL);
        //        oRequest.Method = Type;
        //        oRequest.Headers.Add("accept", "*/*");
        //        oRequest.Headers.Add("Content-Type", "application/json");
        //        if (token != "")
        //        {
        //            oRequest.Headers.Add("Authorization", "Bearer " + token);
        //            oRequest.Headers.Add("PartnerId", "r65nGXCcKeV5fdX3NJEzTUer3QH5reJo");
        //        }
        //        if (data != "")
        //        {
        //            byte[] dataStream = Encoding.UTF8.GetBytes(data);
        //            oRequest.ContentLength = dataStream.Length;
        //            Stream newStream = oRequest.GetRequestStream();
        //            newStream.Write(dataStream, 0, dataStream.Length);
        //            newStream.Close();
        //        }
        //        WebResponse oReponse = oRequest.GetResponse();
        //        using (Stream stream = oReponse.GetResponseStream())
        //        {
        //            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        //            responseString = reader.ReadToEnd();
        //            reader.Close();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseString = "Error";
        //    }
        //    return responseString;
        //}
        private string GetMethodResponse(string URL, string data, string Type, string token)
        {
            String responseString = "";
            try
            {
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);
                oRequest.Method = Type;
                oRequest.Headers.Add("accept", "*/*");
                oRequest.Headers.Add("Content-Type", "application/json");
               
                oRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36";
                string cookieStr = "_ga=GA1.2.157330444.1617373251; app_session=thki1lkcba6o4st5fht40q3hm0; __atssc=google;16; AMP_TOKEN=$NOT_FOUND; _gid=GA1.2.706258968.1638088493; __atuvc=0|44,2|45,19|46,3|47,4|48; __atuvs=61a33f2dc5319cf1003";
                oRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                oRequest.Headers.Add("Cookie", cookieStr);
                if (token != "")
                {
                    oRequest.Headers.Add("Authorization", "Bearer " + token);
                    oRequest.Headers.Add("PartnerId", "r65nGXCcKeV5fdX3NJEzTUer3QH5reJo");
                }
                if (data != "")
                {
                    byte[] dataStream = Encoding.UTF8.GetBytes(data);
                    oRequest.ContentLength = dataStream.Length;
                    Stream newStream = oRequest.GetRequestStream();
                    newStream.Write(dataStream, 0, dataStream.Length);
                    newStream.Close();
                }
                WebResponse oReponse = oRequest.GetResponse();
                using (Stream stream = oReponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    reader.Close();
                   

                }
                oReponse.Close();
            }
            catch (Exception ex)
            {
                responseString = ex.Message;
            }
            return responseString;
        }


    }
}
