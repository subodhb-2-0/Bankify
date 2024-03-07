using Contracts;
using Contracts.Acquisition;
using Contracts.CpOnBoard;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicAPI.Utility;
using Services.Abstractions;
using System.Net;
using System.Net.Mime;
using System.Text;
using XAct.Messages;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CpOnBoardController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public CpOnBoardController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }
        /// <summary>
        /// Get All Service Catagory
        /// </summary>
        /// <returns>Service Catagory</returns>
        [HttpGet(Name = "CheckOnboardStatus")]
        public async Task<ActionResult> CheckOnboardStatus(int orgid, CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string baseURL = configurationBuilder.GetSection("APIURL")["TransAPIURL"];
            string userid = configurationBuilder.GetSection("TransToken")["UserId"];
            string pass = configurationBuilder.GetSection("TransToken")["Pass"];
            string apiResponse = "";
            string strToken = GetMethodResponse(baseURL+"api/Account/GetToken/Login", "{\"userName\":\""+ userid + "\",\"password\":\""+pass+"\"}", "POST", "");
            if (!strToken.Contains("Error"))
            {
                CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                strToken = cpOnBoardToken.data.token;
                if (!string.IsNullOrEmpty(strToken))
                {
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/CheckOnboardStatus?orgid=" + orgid, "", "GET", strToken);
                }
            }
            return Ok(apiResponse);
        }
        [HttpGet(Name = "CheckAllOnboardStatusByOrgId")]
        public async Task<ActionResult> CheckAllOnboardStatusByOrgId(int orgid, CancellationToken cancellationToken)
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
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/CheckAllOnboardStatusByOrgId?orgid=" + orgid, "", "GET", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "SendOtpToMobile")]
        public async Task<ActionResult> SendOtpToMobile(string mobilenumber,int userid, CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string baseURL = configurationBuilder.GetSection("APIURL")["TransAPIURL"];
            string uid = configurationBuilder.GetSection("TransToken")["UserId"];
            string pass = configurationBuilder.GetSection("TransToken")["Pass"];
            string apiResponse = "";
            string strToken = GetMethodResponse(baseURL + "api/Account/GetToken/Login", "{\"userName\":\"" + uid + "\",\"password\":\"" + pass + "\"}", "POST", "");
            if (!strToken.Contains("Error"))
            {
                CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                strToken = cpOnBoardToken.data.token;
                if (!string.IsNullOrEmpty(strToken))
                {
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/SendOtpToMobile?mobilenumber=" + mobilenumber + "&userid="+ userid, "", "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }
        [HttpPost(Name = "SendOtpToMail")]
        public async Task<ActionResult> SendOtpToMail(EmailOtpVerificationDto emailOtpVerificationDto, CancellationToken cancellationToken)
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
                    string strData  = JsonConvert.SerializeObject(emailOtpVerificationDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/SendOtpToMail", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "VerifyMobileOtp")]
        public async Task<ActionResult> VerifyMobileOtp(EditCPOnBoardDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifyMobileOtp", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "VerifyOtpByMail")]
        public async Task<ActionResult> VerifyOtpByMail(EditCpOnBoardByMailDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifyOtpByMail", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "VerifyPanCard")]
        public async Task<ActionResult> VerifyPanCard(EditCpDetailsDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifyPanCard", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "SendOtpToAdharCard")]
        public async Task<ActionResult> SendOtpToAdharCard(string adharcard, CancellationToken cancellationToken)
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
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/SendOtpToAdharCard?adharcard=" + adharcard , "", "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "VerifyAdharCard")]
        public async Task<ActionResult> VerifyAdharCard(EditCpDetailsDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifyAdharCard", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "CheckBankImpsStatus")]
        public async Task<ActionResult> CheckBankImpsStatus(string ifsc_code, CancellationToken cancellationToken)
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
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/CheckBankImpsStatus?ifsc_code=" + ifsc_code, "", "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "VerifyBank")]
        public async Task<ActionResult> VerifyBank(EditCpDetailsDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifyBank", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }

        [HttpPost(Name = "VerifyBankOffline")]
        public async Task<ActionResult> VerifyBankOffline(BankOfflineVerificationDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifyBankOffline", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }
        [HttpPost(Name = "initiateBusinessInfo")]
        public async Task<ActionResult> initiateBusinessInfo(InitiatBusinessinfoDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/initiateBusinessInfo", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }
        [HttpPost(Name = "VerifySelfiImage")]
        public async Task<ActionResult> VerifySelfiImage(UploadSelfiModelDto editCPOnBoardDto, CancellationToken cancellationToken)
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
                    string strData = JsonConvert.SerializeObject(editCPOnBoardDto);
                    apiResponse = GetMethodResponse(baseURL + "api/CpOnBoard/VerifySelfiImage", strData, "POST", strToken);
                }
            }
            return Ok(apiResponse);
        }
        private string GetMethodResponse(string URL, string data, string Type, string token)
        {
            String responseString = "";
            try
            {
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(URL);
                oRequest.Method = Type;
                oRequest.Headers.Add("accept", "*/*");
                oRequest.Headers.Add("Content-Type", "application/json");

                //oRequest.UseDefaultCredentials = true;
                //oRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //oRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
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
            }
            catch (Exception ex)
            {
                responseString = "Error";
            }
            return responseString;
        }
    }

}
