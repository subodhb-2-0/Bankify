using Contracts;
using Contracts.Common;
using Contracts.PageManagement;
using Contracts.Role;

namespace Services.Abstractions
{
    public interface IRoleService : IGenericService<RoleDto>
    {
        Task<ResponseModelDto> GetAllPages(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetPageAccess(int roleId, int pageSourceId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetRolePages(int roleId, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> CreateRoleAndPageAccess(RolePageDto entity, CancellationToken cancellationToken);
        Task<ResponseModelDto> UpdateRoleAndPageAccess(EditRolePageDto entity, CancellationToken cancellationToken);
        Task<ResponseModelDto> EditAndAssignRole(AssignRoleDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ViewRolePages(int roleid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> EditRoleStatus(ActiveDeactiveRoleDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListofRole(CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetRoleByRoleId(int roleId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetPageDetailsByRoleId(int roleId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllPagesAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllPageDetails(int? roleId, string? searchValue, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CreatePageDetails(AddPageDetailDto addPageDetailDto, CancellationToken cancellationToken);
        Task<ResponseModelDto> GetAllParentPage(CancellationToken cancellationToken = default);

    }
}
