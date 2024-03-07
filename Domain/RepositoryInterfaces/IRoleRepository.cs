using Domain.Entities.PageManagement;
using Domain.Entities.RoleManagement;

namespace Domain.RepositoryInterfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IEnumerable<RoleAccess>> GetAllPagesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<RoleAccess>> GetPageAccess(int roleId, int pageSourceId , CancellationToken cancellationToken = default);

        Task<IEnumerable<RoleAccess>> GetRolePages(int roleId, CancellationToken cancellationToken = default);

        Task<Role> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);

        Task<Role> CreateAndAssignPageAccess(Role entity, List<RolePage> rolePages, CancellationToken cancellationToken = default);
        Task<RolepageeditByRoleId> EditRoleAndPageAccess(RolepageeditByRoleId entity, List<RolePageEdit> rolePages, CancellationToken cancellationToken = default);
        Task<AssignRole> AssignRoleAsync(AssignRole entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<ViewRolePages>> ViewRolePages(int roleid, CancellationToken cancellationToken = default);
        Task<ActiveDeactiveRole> ActiveDeactiveRoleAsync(ActiveDeactiveRole entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<ListofRole>> GetListofRole(CancellationToken cancellationToken = default);

        Task<IEnumerable<RoleAccess>> GetRoleByRoleId(int roleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PageDetailsByRole>> GetPageDetailsByRoleId(int roleId, CancellationToken cancellationToken = default);

        Task<Tuple<IEnumerable<Role>, int>> GetAllPagesAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<PageDetails>, int>> GetAllPageDetails(int? roleId, string? serachValue, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<bool> CheckDuplicatePage(AddPageDetail entity);
        Task<AddPageDetail> CreatePageDetails(AddPageDetail addPageDetail, CancellationToken cancellationToken);
        Task<IEnumerable<ParentPageDetails>> GetAllParentPage(CancellationToken cancellationToken);
    }
}
