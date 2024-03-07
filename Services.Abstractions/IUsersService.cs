using Contracts;
using Contracts.Common;
using Contracts.UserManager;

namespace Services.Abstractions
{
    public interface IUsersService : IGenericService<UsersDto>
    {
        Task<ResponseModelDto> GetAuthenticateUsersAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllUsersAsync(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAuthenticateUserAsync(string loginId, string loginPassword, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetUserByLoginIdAsync(string loginId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetUserByUserIdAsync(long userid, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> IsMobileNumberAlreadyInUse(string mobileNumber, int roleId, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> ChangePassword(string newPassword, string oldPassword, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditUserDtlsAsync(UserEditDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GenerateOTPToForgotPassword(string loginId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ResetPassword(string loginId, string otp, string password, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> DistResetPassword(string loginId, string otp, string password, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ActivateOrDeActivateUser(int userId, int status, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> UpdateUserAsync(UpdateUsersDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllActiveUserAsync(int roleId, CancellationToken cancellationToken = default);
        //code added by swapnal fro admin reset password
        Task<ResponseModelDto> ResetPassword(string loginId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetUserAsyncAsync( string? loginid, string? emailid, string? mobilenumber, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetUserBySearchFiltterAsync( string? loginid, string? emailid, string? mobilenumber, string? firstname, int? roleid, int? status, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetDynamicSearchUserAsync(DynamicSearchUserDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateUserMobileAndEmail(ResetMobileAndEmailModelDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetUserByMobileAndUsertypeAsync(string mobileNumber, int usertypeid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAuthenticateUserAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetTokenDataByUseridAsync(long userid, string ipaddress, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateUserTokenAsync(UpdateUsersTokenDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> InsertTokenAsync(InsertTokenDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> DeactivePageStaus(string? pageCode, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditPageDetail(EditPageDetailsDto editPageDetailsDto, string? pageCode, CancellationToken cancellationToken = default);

    }
}