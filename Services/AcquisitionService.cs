using Contracts.Common;
using Contracts.Error;
using Contracts.Response;
using Contracts.Acquisition;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System.Text;
using Domain.Entities.Acquisition;
using XSystem.Security.Cryptography;
using NotificationsLibrary.SMS;
using Contracts.Bbps;
using Contracts.Constants;

namespace Services
{
    public class AcquisitionService : IAcquisitionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;
        public AcquisitionService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public async Task<ResponseModelDto> GetAcquisitionDetails(int? Id, string? Value, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetAcquisitionDetails(Id, Value, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<AcquisitionDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Comission Type {Id}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> AddAcquisitonDetails(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var _AcquisitonModel = addAcquisitionDto.Adapt<AddAcquisitionDto>();
                int length = 8;
                string Randompassword = GetRandomPassword(length);
                var planpassword = Randompassword;
                _AcquisitonModel.password = Randompassword;
                string IsPWDHashEnable = "YES";
                if (IsPWDHashEnable.ToUpper() == "YES")
                {
                    _AcquisitonModel.password = ComputeHash(Randompassword);
                }
                //_AcquisitonModel.password = Randompassword;
                var response = await _repositoryManager.acquisitionRepository.AddAcquisitonDetails(_AcquisitonModel, cancellationToken);
                string s1 = response.p_operationmessage.ToString();
                string[] s2 = s1.Split("|");
                responseModel.ResponseCode = "0";
                //responseModel.Response = "User successfully created. The password is sent to the registered mobile number.";
                responseModel.Response = s2[0].ToString();

                //code to sent the password to mobile number.
                YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                //dynamic _object = new
                //{
                //    UserName = addAcquisitionDto.orgcode,
                //    Password = Randompassword
                //};
                //sMSProvider.SendMessage(addAcquisitionDto.mobileNumber, "POS_User_login", _object);
                dynamic _object = new
                {
                    UserName = s2[2].ToString(),
                    Password = planpassword
                };
                //sMSProvider.SendMessage(s2[1].ToString(), "Registration01", _object);
                sMSProvider.SendMessage(s2[1].ToString(), "POS_User_login", _object);

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Add Acquisiton Details");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

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
        public async Task<ResponseModelDto> VerifyCPApplication(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.VerifyCPApplication(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);

                //var allUsers = await _repositoryManager.acquisitionRepository.VerifyCPApplication(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                //var userDtos = allUsers.Adapt<IEnumerable<VerifyCPApplication>>();
                //responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                //responseModel.Response = "Success";
                //responseModel.Data = userDtos;

                var userDtos = allUsers.Item1.Adapt<IEnumerable<VerifyCPApplicationDto>>();
                VerifyCPApplicationResponse userDetailResponse = new VerifyCPApplicationResponse() { verifyCPApplicationDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetListOrgType(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetListOrgType(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListOrgType>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while ListOrgType");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> GetListofFeildStaff(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetListofFeildStaff(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListofFeildStaff>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while ListofFeildStaff");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetParentOrgId(int? OrgType, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetParentOrgId(OrgType, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ParentOrgId>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetParentOrgId {OrgType}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> VerifyChannelPartner(int? Id, string? Value, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.VerifyChannelPartner(Id, Value, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<VerifyChannelPartnerDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while VerifyChannelPartner {Id}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetCpDetailsById(int? orgId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto response = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetCPDetailsById(orgId, cancellationToken);
                var userDtos = allUsers.Adapt<CPDetailsforApproval>();
                response.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                response.Response = "Success";
                response.Data = userDtos;
            }
            catch(Exception ex)
            {
                _logger.LogInfo($"Error while get CPDetails for Approval {orgId}");
                _logger.LogException(ex);
                response.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                response.Response = ex.Message;
                response.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                    {
                                         ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                         Error = Resource.ContactToAdmin }
                                    };
            }
            return response;
        }

        public async Task<ResponseModelDto> GetCPDetailsforApproval(int? OrgId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetCPDetailsforApproval(OrgId, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<CPDetailsforApproval>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while get CPDetails for Approval {OrgId}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> ConfirmRejectCP(ConfirmRejectCPInfoDto addAcquisitionDto, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.acquisitionRepository.ConfirmRejectCP(addAcquisitionDto, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
                else
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = CPAcquisition.OrgIdNotFound,
                    Error = Resource.UserNotFound } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Confirm Reject CP");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> UpdateCPStatus(int orgid, int status, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                if (!(status == 2 || status == 4))
                {
                    responseModelDto.Response = "Error";
                    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.InValidStatus).ToString(),
                    Error = Resource.InValidStatus } };
                }
                else
                {
                    var result = await _repositoryManager.acquisitionRepository.UpdateCPStatus(orgid, status, cancellationToken);
                    if (result > 0)
                    {
                        responseModelDto.ResponseCode = "0";
                        responseModelDto.Response = status == 2 ? "CP has been activated" : "CP has been deactivated";
                    }
                    else
                    {
                        responseModelDto.Response = "Error";
                        responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = "Error";
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModelDto;
        }

        public async Task<ResponseModelDto> ViewActiveCP(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            //ResponseModelDto responseModel = new ResponseModelDto();
            //try
            //{
            //    var allUsers = await _repositoryManager.acquisitionRepository.ViewActiveCP(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
            //    var userDtos = allUsers.Adapt<IEnumerable<ViewActiveCPDto>>();
            //    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
            //    responseModel.Response = "Success";
            //    responseModel.Data = userDtos;
            //}

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.ViewActiveCP(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<ViewActiveCPDto>>();
                ViewActiveCPResponse userDetailResponse = new ViewActiveCPResponse() { viewActiveCPDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> GetListofChannels(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetListofChannels(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListofChannelsDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get List of Channels");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetProductsforChannel(int? channelId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.acquisitionRepository.GetProductsforChannel(channelId, cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ListProductsforChannelDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Get Products for Channel");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> GetCpdetailsByNameAsync(int? orgType, string? orgName, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _getResponse = await _repositoryManager.acquisitionRepository.GetAllCPDetailsByNameAsync(orgType, orgName, cancellationToken);
                if (_getResponse == null)
                {
                    responseModel.ResponseCode = CPAcquisition.ORGNOTFOUND;
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = CPAcquisition.ORGNOTFOUND,
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _getResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching Cp details {orgType}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        public async Task<ResponseModelDto> ApproveRejectCP(ApproveRejectCPInfoDto addAcquisitionDto, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.acquisitionRepository.ApproveRejectCP(addAcquisitionDto, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
                else
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = CPAcquisition.OrgIdNotFound,
                    Error = Resource.UserNotFound } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Approve Reject CP");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        Task<ResponseModelDto> IGenericService<ResponseModelDto>.AddAsync(ResponseModelDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        Task<ResponseModelDto> IGenericService<ResponseModelDto>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.UpdateAsync(ResponseModelDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ResponseModelDto IGenericService<ResponseModelDto>.Validate(ResponseModelDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> CreateCPByDistributor(AddAcquisitionDto addAcquisitionDto, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var _AcquisitonModel = addAcquisitionDto.Adapt<AddAcquisitionDto>();
                int length = 8;
                string Randompassword = GetRandomPassword(length);
                var planpassword = Randompassword;
                _AcquisitonModel.password = Randompassword;
                string IsPWDHashEnable = "YES";
                if (IsPWDHashEnable.ToUpper() == "YES")
                {
                    _AcquisitonModel.password = ComputeHash(Randompassword);
                }
                //_AcquisitonModel.password = Randompassword;
                var response = await _repositoryManager.acquisitionRepository.CreateCPByDistributor(_AcquisitonModel, cancellationToken);
                if (response.p_operationstatus == 2)
                {
                    string s1 = response.p_operationmessage.ToString();
                    string[] s2 = s1.Split("|");
                    responseModel.ResponseCode = "0";
                    responseModel.Response = s2[0].ToString();
                    //code to sent the password to mobile number.
                    YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                    dynamic _object = new
                    {
                        UserName = s2[2].ToString(),
                        Password = planpassword
                    };
                    //sMSProvider.SendMessage(s2[1].ToString(), "Registration01", _object);
                    sMSProvider.SendMessage(s2[1].ToString(), "1007830712270568387", _object);
                }
                else
                {
                    responseModel.ResponseCode = response.p_operationstatus.ToString();
                    responseModel.Response = response.p_operationmessage.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create CP By Distributor Details");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }
        private string GetRandomPassword(int length)
        {
            const string chars = "0123456789";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }
            return sb.ToString();
        }

        private string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            XSystem.Security.Cryptography.SHA512Managed hashString = new XSystem.Security.Cryptography.SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
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
        public async Task<ResponseModelDto> GetCityAreaByPinCode(int PinCode, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var citiareas = await _repositoryManager.acquisitionRepository.GetCityAreaByPinCode(PinCode, cancellationToken);

                if (citiareas is null)
                {
                    responseModel.ResponseCode = LocationError.NoLocationFound;
                    responseModel.Response = Resource.NoLocationFound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = LocationError.NoLocationFound,
                    Error = Resource.NoLocationFound } };
                    return responseModel;
                }

                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = citiareas;

            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetCpafByOrgid(long orgid, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var alldata = await _repositoryManager.acquisitionRepository.GetCpafByOrgid(orgid, cancellationToken);
                var userDtos = alldata.Adapt<IEnumerable<CpafNumberDto>>();
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> ApproveCPByActivateDate(int orgid, int status, string activationdate, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.acquisitionRepository.ApproveCPByActivateDate(orgid, status, activationdate, cancellationToken);
                if (result > 0)
                {
                    responseModelDto.ResponseCode = "0";
                    //responseModelDto.Response = status == 3 ? "CP has been activated" : "CP has been deactivated";
                    responseModelDto.Response = "Success";
                }
                else
                {
                    responseModelDto.Response = "Error";
                    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = "Error";
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModelDto;
        }
        
        public async Task<ResponseModelDto> GetDynamicSearchCPAsync(DynamicSearchModelCPDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var requestParam = entity.Adapt<DynamicSearchModelCP>();
                var _getuserResponse = await _repositoryManager.acquisitionRepository.GetDynamicSearchCP(requestParam, cancellationToken);

                if (_getuserResponse == null)
                {
                    responseModel.ResponseCode = UserManagementError.UserNotFound;
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = UserManagementError.UserNotFound,
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = _getuserResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the user");
                _logger.LogException(ex);
                responseModel.ResponseCode = CommonErrorCode.ContactToAdmin;
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = CommonErrorCode.ContactToAdmin,
                    Error = Resource.ContactToAdmin } };
            }
            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
    }
}
