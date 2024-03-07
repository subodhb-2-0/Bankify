using Contracts.Common;
using Contracts.PageManagement;
using Contracts.Role;
using Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICustomLogger _customlogger;
        private readonly BaseEntityHelper _baseEntityHelper;
        public RolesController(IServiceManager serviceManager, ICustomLogger customlogger)
        {
            _serviceManager = serviceManager;
            _customlogger = customlogger;
            _baseEntityHelper = new BaseEntityHelper();
        }

        // GET: api/<RolesController>
        [HttpGet]
        public async Task<ResponseModelDto> GetAllAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var roleResponse = await _serviceManager.RoleService.GetAllPagesAsync(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
            return roleResponse;
        }

        // GET api/<RolesController>/5

        /// <summary>
        /// This method is required to get all the roles from database.
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ResponseModelDto> Get(int id, CancellationToken cancellationToken)
        {
            var role = await _serviceManager.RoleService.GetByIdAsync(id, cancellationToken);

            //code added by swapnal for object logging
            _customlogger.LogObject(role);

            return role;
        }

        /// <summary>
        /// This method is required to get all page list 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetAllPages")]
        public async Task<ResponseModelDto> GetAllPages(CancellationToken cancellationToken)
        {
            var role = await _serviceManager.RoleService.GetAllPages(cancellationToken);

            return role;
        }


        /// <summary>
        /// Get All page list which are assigned to role or not assigned to role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetPageAccess/{roleId}/{pageSourceId}")]
        public async Task<ResponseModelDto> GetPageAccess(int roleId, int pageSourceId, CancellationToken cancellationToken)
        {
            var role = await _serviceManager.RoleService.GetPageAccess(roleId, pageSourceId, cancellationToken);

            return role;
        }


        /// <summary>
        /// Get list of pages which are assigned to role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetRolePages/{roleId}")]
        public async Task<ResponseModelDto> GetRolePages(int roleId, CancellationToken cancellationToken)
        {
            var role = await _serviceManager.RoleService.GetRolePages(roleId, cancellationToken);

            return role;
        }

        /// <summary>
        /// This method is required to add role and page access in to database.
        /// </summary>
        /// <param name="rolePageDto">Role and permissions</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Created role</returns>
        [HttpPost("CreateRoleAndRolePermissions")]
        public async Task<ResponseModelDto> CreateRoleAndRolePermissions(RolePageDto rolePageDto, CancellationToken cancellationToken)
        {
            BaseEntityDto baseEntityDto = _baseEntityHelper.GetBaseEntity(HttpContext);
            rolePageDto.Creator = baseEntityDto.CreatedBy;
            var response = await _serviceManager.RoleService.CreateRoleAndPageAccess(rolePageDto, cancellationToken);

            return response;
        }
        /// <summary>
        /// Update Role  Models 
        /// </summary>
        [HttpPut("AssignRole")]
        public async Task<ActionResult> AssignRole(AssignRoleDto roleUpdateDto, CancellationToken cancellationToken)
        {
            var roleUpdateModel = await _serviceManager.RoleService.EditAndAssignRole(roleUpdateDto, cancellationToken);
            return Ok(roleUpdateModel);
        }
        /// <summary>
        /// This method is required to update role and page access in to database.
        /// </summary>
        /// <param name="rolePageDto">Role and page</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Created role</returns>
        [HttpPost("UpdateRoleAndPage")]
        public async Task<ResponseModelDto> UpdateRoleAndPage(EditRolePageDto rolePageDto, CancellationToken cancellationToken)
        {
            BaseEntityDto baseEntityDto = _baseEntityHelper.GetBaseEntity(HttpContext);
            rolePageDto.Creator = baseEntityDto.CreatedBy;
            rolePageDto.Modifier = baseEntityDto.UpdatedBy;
            var response = await _serviceManager.RoleService.UpdateRoleAndPageAccess(rolePageDto, cancellationToken);
            return response;
        }

        [HttpGet("ViewRolePages")]
        public async Task<ActionResult> ViewRolePages(int roleid, CancellationToken cancellationToken)
        {
           var userResponseModel = await _serviceManager.RoleService.ViewRolePages(roleid, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Update Role  Models 
        /// </summary>
        [HttpPut("ActiveDeactiveRole")]
        public async Task<ActionResult> ActiveDeactiveRole(ActiveDeactiveRoleDto roleUpdateDto, CancellationToken cancellationToken)
        {
            var roleUpdateModel = await _serviceManager.RoleService.EditRoleStatus(roleUpdateDto, cancellationToken);
            return Ok(roleUpdateModel);
        }
        [HttpGet("GetListofRole")]
        public async Task<ActionResult> GetListofRole(CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.RoleService.GetListofRole(cancellationToken);
            return Ok(userResponseModel);
        }

        [HttpGet("GetRoleByRoleId")]
        public async Task<ActionResult> GetRoleByRoleId(int roleId, CancellationToken cancellationToken)
        {
            ResponseModelDto roles = await _serviceManager.RoleService.GetRoleByRoleId(roleId, cancellationToken);
            return Ok(roles);
        }

        [HttpGet("GetPageDetailsByRoleId")]
        public async Task<ResponseModelDto> GetPageDetailsByRoleId(int roleId, CancellationToken cancellationToken)
        {
            var role = await _serviceManager.RoleService.GetPageDetailsByRoleId(roleId, cancellationToken);
            return role;
        }
        /// <summary>
        /// Get Page Details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPageDetails")]
        public async Task<ResponseModelDto> GetAllPageDetails(int? roleId, string? searchValue, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken)
        {
            var roleResponse = await _serviceManager.RoleService.GetAllPageDetails(roleId, searchValue, pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
            return roleResponse;
        }
        /// <summary>
        /// Add Page Details
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddPageDetails")]
        public async Task<ResponseModelDto> CreatePageDetails(AddPageDetailDto addPageDetailDto, CancellationToken cancellationToken)
        {
            BaseEntityDto baseEntityDto = _baseEntityHelper.GetBaseEntity(HttpContext);
            addPageDetailDto.Creator = baseEntityDto.CreatedBy;
            var response = await _serviceManager.RoleService.CreatePageDetails(addPageDetailDto, cancellationToken);

            return response;
        }

        [HttpGet("GetAllParentPage")]
        public async Task<ResponseModelDto> GetAllParentPage(CancellationToken cancellationToken)
        {
            var roleResponse = await _serviceManager.RoleService.GetAllParentPage(cancellationToken);
            return roleResponse;
        }
    }
}
