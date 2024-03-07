using Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class MasterDataController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        
        public MasterDataController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }



        // GET: api/<MasterDataController>
        [HttpGet]
        public async Task<ResponseModelDto> GetAllDepartments(CancellationToken cancellationToken)
        {
            var departmentListResponse = await _serviceManager.MasterDataService.GetDepartmentListAsync(cancellationToken);
            return departmentListResponse;
        }

        // GET: api/<MasterDataController>
        [HttpGet]
        public async Task<ResponseModelDto> GetAllDesignations(CancellationToken cancellationToken)
        {
            var designationListResponse = await _serviceManager.MasterDataService.GetDesignationListAsync(cancellationToken);
            return designationListResponse;
        }
    }
}
