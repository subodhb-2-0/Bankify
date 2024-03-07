using Contracts;
using Contracts.Common;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.Error;
using Contracts.Response;
using Contracts.UserManager;
using Domain.Entities.OTPManager;
using Domain.Entities.UserManagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using NotificationsLibrary.SMS;
using Services.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UsersService : IUsersService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;
        private readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public UsersService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        async Task<ResponseModelDto> IGenericService<UsersDto>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseModelDto> GetUserByLoginIdAsync(string loginId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "0";
            try
            {

                var user = await _repositoryManager.usersRepository.GetUserByLoginId(loginId, cancellationToken);

                if (user == null)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound
                }};
                }
                else
                {
                    var userDto = user.Adapt<UserDetailDto>();
                    responseModel.ResponseCode = "0";
                    responseModel.Response = nameof(ResponseCode.Success);
                    responseModel.Data = userDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while login user {loginId}");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> GetUserByUserIdAsync(long userid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "0";
            try
            {

                var user = await _repositoryManager.usersRepository.GetUserByUserId(userid, cancellationToken);

                if (user == null)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound
                }};
                }
                else
                {
                    var userDto = user.Adapt<UserDtlsWithRoleDto>();
                    responseModel.ResponseCode = "0";
                    responseModel.Response = nameof(ResponseCode.Success);
                    responseModel.Data = userDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while login user {userid}");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> GetAuthenticateUserAsync(string loginId, string loginPassword, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {


                var user = await _repositoryManager.usersRepository.GetUserByLoginId(loginId, cancellationToken);

                if (user == null || user.Password != loginPassword)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.InvalidUserNamePassword).ToString(),
                    Error = Resource.InvalidUserNamePassword
                }};
                }
                else if (user.Status == 1)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  =   ((int)ErrorCodeEnum.UserManagementErrorEnum.UserIsInActive).ToString(),
                    Error = Resource.UserIsInActive
                }};
                }
                else if (user.Status == 3)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserIsDeleted).ToString(),
                    Error = Resource.UserIsDeleted
                }};
                }
                else if (user.FailedCount > 5)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserIsIsLocked).ToString(),
                    Error = Resource.UserIsIsLocked
                }};
                }
                else if (user.Status == 2 && user.NeedsPassChnage == 1)
                {
                    var userDto = user.Adapt<UsersDto>();
                    responseModel.ResponseCode = UserManagementResponse.NeedResetPassword;
                    responseModel.Response = Resource.NeedResetPassword;
                    responseModel.Data = userDto;
                }
                else if (user.Status == 2)
                {
                    var userDto = user.Adapt<UsersDto>();
                    responseModel.ResponseCode = "0";
                    responseModel.Response = nameof(ResponseCode.Success);
                    responseModel.Data = userDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while login user {loginId}");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> GetAuthenticateUsersAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _loginResponse = await _repositoryManager.usersRepository.GetAuthenticateUsersAsync(loginId, loginPassword, ipAddress, cancellationToken);
                if (_loginResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.InvalidUserNamePassword).ToString();
                    responseModel.Response = Resource.InvalidUserNamePassword;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.InvalidUserNamePassword).ToString(),
                    Error = Resource.InvalidUserNamePassword } };

                    //logging
                    _logger.LogInfo($"Error while login user {loginId}");
                    _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                    return await Task.FromResult<ResponseModelDto>(responseModel);
                }
                else if (_loginResponse.NeedsPassChange == 0 || string.IsNullOrEmpty(_loginResponse.NeedsPassChange.ToString()))
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.FirstTimeLoginUserNeedToChangeThePassword).ToString();
                    responseModel.Response = Resource.FirstTimeLoginUserNeedToChangeThePassword;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.FirstTimeLoginUserNeedToChangeThePassword).ToString(),
                    Error = Resource.FirstTimeLoginUserNeedToChangeThePassword } };

                    //logging
                    _logger.LogInfo($"Error while login user {loginId}");
                    _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                    return await Task.FromResult<ResponseModelDto>(responseModel);
                }
                else if (_loginResponse.LastPwdChangeDate.AddDays(60) <= DateTime.Now)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNeedToChangeThePassword).ToString();
                    responseModel.Response = Resource.UserNeedToChangeThePassword;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNeedToChangeThePassword).ToString(),
                    Error = Resource.UserNeedToChangeThePassword } };

                    //logging
                    _logger.LogInfo($"Error while login user {loginId}");
                    _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                    return await Task.FromResult<ResponseModelDto>(responseModel);
                }

                //username and password are matching and done with all the validations
                responseModel.ResponseCode = "0";
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = _loginResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while login user {loginId}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetAllUsersAsync(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allUsers = await _repositoryManager.usersRepository.GetAllUsersAsync(pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = allUsers.Item1.Adapt<IEnumerable<UserDetailDto>>();

                UserDetailResponse userDetailResponse = new UserDetailResponse() { UserDetails = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };


                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }
        async Task<ResponseModelDto> IGenericService<UsersDto>.GetAllAsync(CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var users = await _repositoryManager.usersRepository.GetAllAsync(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = users.Adapt<IEnumerable<UserDetailDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }
        public async Task<ResponseModelDto> AddAsync(UsersDto entity, CancellationToken cancellationToken)
        {
            //Add generate password logic here
            entity.Password = entity.LoginId;           
            var responseModel = Validate(entity);
            if (responseModel != null) return await Task.FromResult(responseModel);
            responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                int length = 8;
                string Randompassword = GetRandomPassword(length);
                entity.Password = Randompassword;
                var planpassword = Randompassword;
                string IsPWDHashEnable = "YES";
                if (IsPWDHashEnable.ToUpper() == "YES")
                {
                    entity.Password = ComputeHash(Randompassword);
                }
                var existingUser = await _repositoryManager.usersRepository.GetAllUserAsync(entity.LoginId, entity.EmailId, entity.MobileNumber, cancellationToken);
                //code added by swapnal to generate the random password
                //entity.Password = GenerateRandomPassword();
                if (existingUser is not null && existingUser.Count() > 0)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserAlreadyExist).ToString(),
                    Error = Resource.UserAlreadyExist } };
                }
                else
                {
                    var userEntity = entity.Adapt<Users>();
                    await _repositoryManager.usersRepository.AddAsync(userEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "User successfully created. The password is sent to the registered mobile number.";

                    //code to sent the password to mobile number.
                    YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                    dynamic _object = new
                    {
                        UserName = entity.LoginId,
                        Password = planpassword
                    };

                    sMSProvider.SendMessage(entity.MobileNumber, "POS_User_login", _object);
                }
            }
            catch (Exception ex)
            {
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        async Task<ResponseModelDto> IGenericService<UsersDto>.UpdateAsync(UsersDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
          
            try
            {
                responseModel.ResponseCode = "-1";

                var existingUser = await _repositoryManager.usersRepository.GetUserByLoginId(entity.LoginId, cancellationToken);

                if (existingUser is null)
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
                else
                {
                    var userEntity = entity.Adapt<Users>();
                    await _repositoryManager.usersRepository.UpdateAsync(userEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = nameof(ResponseCode.Success);

                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        async Task<ResponseModelDto> IGenericService<UsersDto>.DeleteAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> ActivateOrDeActivateUser(int userId, int status, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                if (!(status == 2 || status == 3))
                {
                    responseModelDto.Response = nameof(ResponseCode.Error);
                    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.InValidStatus).ToString(),
                    Error = Resource.InValidStatus } };
                }
                else
                {

                    var result = await _repositoryManager.usersRepository.ActivateOrDeActivateUser(userId, status, cancellationToken);


                    if (result > 0)
                    {
                        responseModelDto.ResponseCode = "0";
                        responseModelDto.Response = status == 3 ? "User has been deactivated" : "User has been activated";
                    }
                    else
                    {
                        responseModelDto.Response = nameof(ResponseCode.Error);
                        responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };


                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = nameof(ResponseCode.Error);
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModelDto;
        }

        public ResponseModelDto Validate(UsersDto entity)
        {
            ResponseModelDto responseModel = null;


            var context = new ValidationContext(entity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                responseModel = new ResponseModelDto() { Errors = new List<ErrorModelDto>() };
                foreach (var result in results)
                {

                    var _error = result.ToString().Split('|');

                    responseModel.ResponseCode = _error[0];
                    responseModel.Response = _error[1];
                    responseModel.Errors.Add(new ErrorModelDto() { Error = _error[1], ErrorCode = _error[1] });
                }

            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditUserDtlsAsync(UserEditDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var userEntity = entity.Adapt<UserEdit>();
                await _repositoryManager.usersRepository.UpdateUserAsync(userEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = nameof(ResponseCode.Success);

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update user Dtls");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> IsMobileNumberAlreadyInUse(string mobileNumber, int roleId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "1";
            try
            {
                var user = await _repositoryManager.usersRepository.GetUserByMobileNumber(mobileNumber, cancellationToken);

                if (user == null)
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = nameof(ResponseCode.Success);
                }
                else
                {
                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.MobileNumberExist).ToString(),
                    Error = Resource.UserNotFound
                }};
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching user by mobile number  {mobileNumber}");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public Task<ResponseModelDto> ChangePassword(string newPassword, string oldPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GenerateOTPToForgotPassword(string loginId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "1";
            try
            {

                var user = await _repositoryManager.usersRepository.GetUserByLoginId(loginId, cancellationToken);

                if (user == null)
                {

                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound}};
                }
                else
                {
                    //get active existing otp
                    OTPDetails oTPDetails = await _repositoryManager.oTPRepository.Get(loginId, (int)(int)ModuleEnum.ForgotPassword);

                    string _otp = GenerateOTP().ToString();

                    //code commented by sapnal to generate new OTP everytime
                    //if (oTPDetails is not null)
                    //{
                    //    _otp = oTPDetails.OTP;
                    //}
                    //else
                    //{

                        oTPDetails = new OTPDetails()
                        {
                            Creator = user.UserId,
                            ExpiredTimeInSecond = 300,
                            LoginId = loginId,
                            OTP = _otp,
                            ModuleId = (int)ModuleEnum.ForgotPassword

                        };
                        await _repositoryManager.oTPRepository.AddAsync(oTPDetails, cancellationToken);
                    //}
                    //end code commented by sapnal to generate new OTP everytime


                    YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                    dynamic _object = new
                    {
                        OTP = _otp.ToString()
                    };


                    sMSProvider.SendMessage(user.MobileNumber, "Logiin", _object);

                    responseModel.ResponseCode = "0";
                    responseModel.Data = user.MobileNumber;
                    responseModel.Response = Resource.ForgotPasswordOTP;
                }


            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching user by loginid : {loginId}");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> ResetPassword(string loginId, string otp, string password, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "1";
            try
            {

                var user = await _repositoryManager.usersRepository.GetUserByLoginId(loginId, cancellationToken);

                if (user == null)
                {

                    responseModel.Response = nameof(ResponseCode.Error);
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound}};
                }
                else
                {
                    //get active existing otp
                    OTPDetails oTPDetails = await _repositoryManager.oTPRepository.Get(loginId, (int)(int)ModuleEnum.ForgotPassword);

                    if (oTPDetails is null)
                    {
                        responseModel.Response = nameof(ResponseCode.Error);
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                            ErrorCode  = OTPErrorCode.OTPExpire, // ((int)ErrorCodeEnum.OTPErrorCodeEnum.OTPExpire).ToString(),
                            Error = Resource.OTPExpire}};
                    }
                    else if (oTPDetails.OTP != otp)
                    {
                        responseModel.Response = nameof(ResponseCode.Error);
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                            ErrorCode  = OTPErrorCode.InValidOTP,
                            Error = Resource.InValidOTP}};
                    }
                    else
                    {
                        byte[] key = Encoding.ASCII.GetBytes("PLAIN_7PAYAGENTS");
                        byte[] IV = Encoding.ASCII.GetBytes("8119745113154120");
                        string DecPw = DecryptString(password, key, IV);
                        if (!string.IsNullOrEmpty(DecPw))
                        {
                            password = ComputeHash(DecPw);
                        }
                        await _repositoryManager.usersRepository.UpdatePassword(loginId, password, cancellationToken);
                        responseModel.ResponseCode = "0";
                        responseModel.Response = Resource.ResetPasswordSuccessfully;

                        YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                        dynamic _object = new
                        {
                            ContactNumber = user.MobileNumber
                        };


                        sMSProvider.SendMessage(user.MobileNumber, "Password_reset", _object);

                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while validating opt for login : {loginId}");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> DistResetPassword(string loginId, string otp, string password, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "1";
            try
            {
                var user = await _repositoryManager.usersRepository.GetUserByLoginId(loginId, cancellationToken);
                if (user == null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound}};
                }
                else
                {
                    //get active existing otp
                    OTPDetails oTPDetails = await _repositoryManager.oTPRepository.Get(loginId, (int)(int)ModuleEnum.ForgotPassword);
                    if (oTPDetails is null)
                    {
                        responseModel.Response = "Error";
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                            ErrorCode  = OTPErrorCode.OTPExpire,
                            Error = Resource.OTPExpire}};
                    }
                    else if (oTPDetails.OTP != otp)
                    {
                        responseModel.Response = "Error";
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                            ErrorCode  = OTPErrorCode.InValidOTP,
                            Error = Resource.InValidOTP}};
                    }
                    else
                    {
                        //byte[] key = Encoding.ASCII.GetBytes("PLAIN_7PAYAGENTS");
                        //byte[] IV = Encoding.ASCII.GetBytes("8119745113154120");
                        //string DecPw = DecryptString(password, key, IV);
                        string DecPw = password;
                        if (!string.IsNullOrEmpty(DecPw))
                        {
                            password = ComputeHash(DecPw);
                        }
                        await _repositoryManager.usersRepository.UpdatePassword(loginId, password, cancellationToken);
                        responseModel.ResponseCode = "0";
                        responseModel.Response = Resource.ResetPasswordSuccessfully;

                        YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                        dynamic _object = new
                        {
                            ContactNumber = user.MobileNumber
                        };
                        sMSProvider.SendMessage(user.MobileNumber, "Password_reset", _object);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while validating opt for login : {loginId}");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> ResetPassword(string loginId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "1";
            try
            {

                var user = await _repositoryManager.usersRepository.GetUserByLoginId(loginId, cancellationToken);

                if (user == null)
                {

                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound}};
                }
                else
                {
                    //var _password = GenerateRandomPassword();
                    int length = 8;
                    string _password = GetRandomPassword(length);
                    string planpssword = _password;
                    if (!string.IsNullOrEmpty(_password))
                    {
                        _password = ComputeHash(_password);
                    }

                    await _repositoryManager.usersRepository.UpdatePassword(loginId, _password, cancellationToken);
                    responseModel.ResponseCode = "0";
                    responseModel.Response = Resource.ResetPasswordSuccessfully;

                    YieldPlusSMSProvider sMSProvider = new YieldPlusSMSProvider();
                    //dynamic _object = new
                    //{
                    //    ContactNumber = planpssword
                    //};
                    //sMSProvider.SendMessage(user.MobileNumber, "Password_reset", _object);
                    dynamic _object = new
                    {
                        UserName = user.LoginId,
                        Password = planpssword
                    };
                    //sMSProvider.SendMessage(s2[1].ToString(), "Registration01", _object);
                    sMSProvider.SendMessage(user.MobileNumber, "POS_User_login", _object);
                }


            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while validating opt for login : {loginId}");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> UpdateUserAsync(UpdateUsersDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();

            try
            {
                responseModel.ResponseCode = "-1";

                var existingUser = await _repositoryManager.usersRepository.GetUserByLoginId(entity.LoginId, cancellationToken);

                if (existingUser is null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
                else
                {
                    var userEntity = entity.Adapt<Users>();
                    await _repositoryManager.usersRepository.UpdateAsync(userEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";

                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> GetUserAsyncAsync( string? loginid, string? emailid, string? mobilenumber, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _getuserResponse = await _repositoryManager.usersRepository.GetAllUserAsync( loginid, emailid, mobilenumber, cancellationToken);
                if (_getuserResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString();
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _getuserResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the user {loginid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetUserBySearchFiltterAsync(string? loginid, string? emailid, string? mobilenumber, string? firstname, int? roleid, int? status, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _getuserResponse = await _repositoryManager.usersRepository.GetUserBySearchFiltter(loginid, emailid, mobilenumber, firstname, roleid, status, p_offsetrows, p_fetchrows, cancellationToken);
                
                if (_getuserResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString();
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _getuserResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the user {loginid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> GetDynamicSearchUserAsync(DynamicSearchUserDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var requestparam = entity.Adapt<DynamicSearchUserModel>();
                var _getuserResponse = await _repositoryManager.usersRepository.GetDynamicSearchUser(requestparam, cancellationToken);

                if (_getuserResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString();
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _getuserResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the user");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        private int GenerateOTP()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        private string GenerateRandomPassword()
        {
            int size = 8;
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        public async Task<ResponseModelDto> GetAllActiveUserAsync(int roleId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var rolePages = await _repositoryManager.usersRepository.GetAllActiveUser(roleId, cancellationToken);               
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = rolePages;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> GetUserByMobileAndUsertypeAsync(string mobileNumber, int usertypeid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "0";
            try
            {

                var user = await _repositoryManager.usersRepository.GetUserByMobileAndUsertype(mobileNumber, usertypeid, cancellationToken);

                if (user == null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound
                }};
                }
                else
                {
                    
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                    responseModel.Data = user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching user {mobileNumber}");
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
        private string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            //encryptor.KeySize = 256;
            //encryptor.BlockSize = 128;
            //encryptor.Padding = PaddingMode.Zeros;
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
        public async Task<ResponseModelDto> UpdateUserMobileAndEmail(ResetMobileAndEmailModelDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();

            try
            {
                responseModel.ResponseCode = "-1";

                var existingUser = await _repositoryManager.usersRepository.GetUserByLoginId(entity.loginid, cancellationToken);

                if (existingUser is null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
                else
                {
                    var userEntity = entity.Adapt<ResetMobileAndEmailModel>();
                    await _repositoryManager.usersRepository.UpdateUserMobileAndEmailAsync(userEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";

                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> GetAuthenticateUserAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            UserOnBoardingModel onboardingResponse = new UserOnBoardingModel();
            responseModel.ResponseCode = "-1";

            try
            {
                var encpassword = ComputeHash(loginPassword);
                var _loginResponse = await _repositoryManager.usersRepository.GetAuthenticateUserAsync(loginId, encpassword, ipAddress, cancellationToken);
                if (_loginResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.InvalidUserNamePassword).ToString();
                    responseModel.Response = Resource.InvalidUserNamePassword;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.InvalidUserNamePassword).ToString(),
                    Error = Resource.InvalidUserNamePassword } };

                    //logging
                    _logger.LogInfo($"Error while login user {loginId}");
                    _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                    return await Task.FromResult<ResponseModelDto>(responseModel);
                }
                else if (_loginResponse.NeedsPassChange == 0 || string.IsNullOrEmpty(_loginResponse.NeedsPassChange.ToString()))
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.FirstTimeLoginUserNeedToChangeThePassword).ToString();
                    responseModel.Response = Resource.FirstTimeLoginUserNeedToChangeThePassword;
                    responseModel.Data = _loginResponse;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.FirstTimeLoginUserNeedToChangeThePassword).ToString(),
                    Error = Resource.FirstTimeLoginUserNeedToChangeThePassword } };

                    //logging
                    _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                    return await Task.FromResult<ResponseModelDto>(responseModel);
                }
                else if (_loginResponse.LastPwdChangeDate.AddDays(60) <= DateTime.Now)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.UserNeedToChangeThePassword).ToString();
                    responseModel.Response = Resource.UserNeedToChangeThePassword;
                    responseModel.Data = _loginResponse;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.UserNeedToChangeThePassword).ToString(),
                    Error = Resource.UserNeedToChangeThePassword } };

                    //logging
                    _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                    return await Task.FromResult<ResponseModelDto>(responseModel);
                }

                //code added by swapnal to deal with on boarding status
                onboardingResponse = await _repositoryManager.usersRepository.GetOnBoardingStatusByOrgId(_loginResponse.OrgId, cancellationToken);
                if (onboardingResponse != null)
                {

                    if (onboardingResponse.Status == 0) //some onboarding details is pending
                    {
                        responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.SomeOnboardingDetailsArePending).ToString();
                        responseModel.Response = Resource.SomeOnboardingDetailsArePending;
                        responseModel.Data = _loginResponse;
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                        ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.SomeOnboardingDetailsArePending).ToString(),
                        Error = Resource.SomeOnboardingDetailsArePending } };

                        //logging
                        _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                        return await Task.FromResult<ResponseModelDto>(responseModel);
                    }
                    else if (onboardingResponse.Status == 1) //some onboarding KYC pending
                    {
                        responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.SomeOnboardingKYCsArePending).ToString();
                        responseModel.Response = Resource.SomeOnboardingKYCsArePending;
                        responseModel.Data = _loginResponse;
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                        ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.SomeOnboardingKYCsArePending).ToString(),
                        Error = Resource.SomeOnboardingKYCsArePending } };

                        //logging
                        _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                        return await Task.FromResult<ResponseModelDto>(responseModel);
                    }
                    else if (onboardingResponse.Status == 3) //some onboarding have failed
                    {
                        responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.SomeOnboardingDetailsHaveFailed).ToString();
                        responseModel.Response = Resource.FacingIssueInFatchingOnboarding;
                        responseModel.Data = _loginResponse;
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                        ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.FacingIssueInFatchingOnboarding).ToString(),
                        Error = Resource.FacingIssueInFatchingOnboarding } };

                        //logging
                        _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                        return await Task.FromResult<ResponseModelDto>(responseModel);
                    }
                    else if (onboardingResponse.Status == 4) //login is blocked
                    {
                        responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.UserBlockedPermanently).ToString();
                        responseModel.Response = Resource.UserBlockedPermanently;
                        responseModel.Data = _loginResponse;
                        responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                        ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.UserBlockedPermanently).ToString(),
                        Error = Resource.UserBlockedPermanently } };

                        //logging
                        _logger.LogInfo(responseModel.ResponseCode.ToString() + " " + responseModel.Response.ToString());

                        return await Task.FromResult<ResponseModelDto>(responseModel);
                    }
                }

                //end code added by swapnal to deal with on boarding status


                //username and password are matching and done with all the validations
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _loginResponse;

                _logger.LogInfo($"Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while login user {loginId}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        public async Task<ResponseModelDto> GetTokenDataByUseridAsync(long userid, string ipaddress, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";

            try
            {
                var _getResponse = await _repositoryManager.usersRepository.GetTokenDataByUserid(userid, ipaddress, cancellationToken);
                if (_getResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.UnableToFetchWalletBalance).ToString();
                    responseModel.Response = Resource.UnableToFetcBalance;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementTransErrorEnum.UnableToFetchWalletBalance).ToString(),
                    Error = Resource.UnableToFetcBalance } };
                    return responseModel;
                }
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = _getResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the token {userid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
        public async Task<ResponseModelDto> UpdateUserTokenAsync(UpdateUsersTokenDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var tokenEntity = entity.Adapt<UpdateUsersToken>();
                var users = await _repositoryManager.usersRepository.UpdateUserToken(tokenEntity, cancellationToken);
                if (users is null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.NoSubUserFound).ToString();
                    responseModel.Response = Resource.NoSubUserFound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.NoSubUserFound).ToString(),
                    Error = Resource.NoSubUserFound } };
                }
                else
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> InsertTokenAsync(InsertTokenDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";
                var tokenEntity = entity.Adapt<InsertTokenDataModel>();
                var users = await _repositoryManager.usersRepository.InsertToken(tokenEntity, cancellationToken);
                if (users is null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.NoSubUserFound).ToString();
                    responseModel.Response = Resource.NoSubUserFound;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementTransErrorEnum.NoSubUserFound).ToString(),
                    Error = Resource.NoSubUserFound } };
                }
                else
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> DeactivePageStaus(string? pageCode, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.usersRepository.DeactivePageStaus(pageCode, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Page status changed successfully.";
                }
                else
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> EditPageDetail(EditPageDetailsDto editPageDetailsDto, string? pageCode, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var edditDetails = editPageDetailsDto.Adapt<EditPageDetails>();
                var result = await _repositoryManager.usersRepository.EditPageDetail(edditDetails, pageCode, cancellationToken);
                if (result > 0)
                {
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Page details has been successfully edited.";
                }
                else
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
    }
}