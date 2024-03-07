using Contracts.UserManager;
using Domain.Entities.UserManagement;

namespace Domain.RepositoryInterfaces
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<UsersData> GetAuthenticateUsersAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<Users>, int>> GetAllUsersAsync(int pageSize, int pageNumber, string orderByColumn, string orderBy, CancellationToken cancellationToken = default);
        Task<Users> GetUserByLoginId(string loginId, CancellationToken cancellationToken = default);
        Task<UserDtlsWithRole> GetUserByUserId(long userid, CancellationToken cancellationToken = default);
        Task<Users> GetAuthenticateUserAsync(string loginId, string loginPassword, CancellationToken cancellationToken = default);
        Task<int> ActivateOrDeActivateUser(int userId, int status, CancellationToken cancellationToken = default);

        Task<Users> GetUserByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default);
        Task<UserEdit> UpdateUserAsync(UserEdit entity, CancellationToken cancellationToken = default);
        Task<int> UpdatePassword(string loginId, string password, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserDetailsQuery>> GetAllUserAsync( string? loginid, string? emailid, string? mobilenumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserDetailsQuery>> GetUserBySearchFiltter( string? loginid, string? emailid, string? mobilenumber, string? firstname,int? roleid, int? status, int? p_offsetrows, int? p_fetchrows, CancellationToken cancellationToken = default);
        Task<IEnumerable<ActiveUserDetailsByRoleId>> GetAllActiveUser( int roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserDetailsQuery>> GetDynamicSearchUser(DynamicSearchUserModel entity, CancellationToken cancellationToken = default);
        Task<ResetMobileAndEmailModel> UpdateUserMobileAndEmailAsync(ResetMobileAndEmailModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserByMobileAndType>> GetUserByMobileAndUsertype(string mobileNumber,int usertypeid, CancellationToken cancellationToken = default);
        Task<Users> GetAuthenticateUserAsync(string loginId, string loginPassword, string ipAddress, CancellationToken cancellationToken = default);
        Task<UserOnBoardingModel> GetOnBoardingStatusByOrgId(long orgId, CancellationToken cancellationToken = default);

        Task<GetTokenDataByUserid> GetTokenDataByUserid(long userid, string ipaddress, CancellationToken cancellationToken = default);
        Task<UpdateUsersToken> UpdateUserToken(UpdateUsersToken entity, CancellationToken cancellationToken = default);
        Task<OpperetionStausModel> InsertToken(InsertTokenDataModel entity, CancellationToken cancellationToken = default);
       Task<int> DeactivePageStaus(string? pageCode, CancellationToken cancellationToken = default);
       Task<int> EditPageDetail(EditPageDetails editPageDetails, string? pageCode, CancellationToken cancellationToken = default);
    }
}
