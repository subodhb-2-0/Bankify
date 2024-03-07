using Contracts;
using Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public RoleController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public Task<ResponseModelDto> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = _serviceManager.RoleService.GetAllAsync(cancellationToken);
            //if (roles.co)
            //{

            //}
            return result;
        }

        [HttpGet]
        public Task<ResponseModelDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var roles = _serviceManager.RoleService.GetByIdAsync(id, cancellationToken);
            return roles;
        }

        [HttpPost]
        public Task<ResponseModelDto> Post([FromBody] RoleDto roleDto)
        {
            return _serviceManager.RoleService.AddAsync(roleDto);

        }

    }
}
