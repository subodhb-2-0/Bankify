using Contracts;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ComissionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        public ComissionController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
        }
        /// <summary>
        /// Get All Comission Type details
        /// </summary>
        /// <returns>All Comission type details</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllComissionType( CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ComissionService.GetAllCommisionTypeAsync( cancellationToken);

            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get Comission Type details
        /// </summary>
        /// <returns>Comission type details</returns>
        [HttpGet]
        public async Task<ActionResult> GetComissionType(string commType, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ComissionService.GetCommisionTypeAsync(commType, cancellationToken);

            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get Base parameter by comm Type details
        /// </summary>
        /// <returns>parameter details</returns>
        [HttpGet]
        public async Task<ActionResult> GetBaseparameterByCommType(string Value2, CancellationToken cancellationToken)
        {
            var commResponseModel = await _serviceManager.ComissionService.GetbaseParamByCommTypeAsync(Value2, cancellationToken);

            return Ok(commResponseModel);
        }
        /// <summary>
        /// Get Comission Receivable details
        /// </summary>
        /// <returns>Comission Receivable details</returns>
        [HttpGet]
        public async Task<ActionResult> GetReceivableDtls(int commReceivableId, int userid, CancellationToken cancellationToken)
        {
            var commResponseModel = await _serviceManager.ComissionService.GetCommReceivablesDtlsAsync(commReceivableId, userid, cancellationToken);

            return Ok(commResponseModel);
        }
        /// <summary>
        /// Get Comission sharing model details
        /// </summary>
        /// <returns>Comission sharing model details</returns>
        [HttpGet]
        public async Task<ActionResult> GetComissionSharingmodelDtls(int commSharingId, int userid, CancellationToken cancellationToken)
        {
            var commResponseModel = await _serviceManager.ComissionService.GetCommSharingModelDtlsAsync(commSharingId, userid, cancellationToken);

            return Ok(commResponseModel);
        }
        /// <summary>
        /// Get Comission sharing details
        /// </summary>
        /// <returns>Comission sharing details</returns>
        [HttpGet]
        public async Task<ActionResult> GetComissionSharing(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ComissionService.GetCommisionSharingAsync(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Get Comission receive details
        /// </summary>
        /// <returns>Comission receive details</returns>
        [HttpGet]
        public async Task<ActionResult> GetComissionreceive(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            var comissionResponseModel = await _serviceManager.ComissionService.GetComissionReceivableAsync(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
            return Ok(comissionResponseModel);
        }
        /// <summary>
        /// Get Comission receive Id
        /// </summary>
        /// <returns>Comission receive Id</returns>
        [HttpGet]
        public async Task<ActionResult> GetComissionreceiveId(string crname, CancellationToken cancellationToken)
        {
            var comissionResponseModel = await _serviceManager.ComissionService.GetRecentIdOfComissionReciveAsync(crname,cancellationToken);

            return Ok(comissionResponseModel);
        }
        // <summary>
        /// Get Comission sharing Id
        /// </summary>
        /// <returns>Comission sharing Id</returns>
        [HttpGet]
        public async Task<ActionResult> GetComissionsharingId(string csmname, CancellationToken cancellationToken)
        {
            var comissionResponseModel = await _serviceManager.ComissionService.GetRecentIdOfComissionSharingAsync(csmname, cancellationToken);

            return Ok(comissionResponseModel);
        }
        /// <summary>
        /// Get Comission receive status details
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetComissionreceiveStatus(int CRID, CancellationToken cancellationToken)
        {
            var comissionResponseModel = await _serviceManager.ComissionService.GetCommReceivablesStatus(CRID,cancellationToken);

            return Ok(comissionResponseModel);
        }
        /// <summary>
        /// Add comission Receiveable
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddcomissionReceiveable(CreateComissionReceivableDto ComissionReceivable, CancellationToken cancellationToken)
        {
            var userResponseModel = await _serviceManager.ComissionService.CreateComissionReceivableAsync(ComissionReceivable, cancellationToken);
            return Ok(userResponseModel);
        }
        /// <summary>
        /// Add comission Receiveable Dtls
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddcomissionReceiveableDtls(CreateCommReceivablesDto ComissionReceivableDtls, CancellationToken cancellationToken)
        {
            var commTypeResponseModel = await _serviceManager.ComissionService.CreateComissionReceivableDetlsAsync(ComissionReceivableDtls, cancellationToken);
            return Ok(commTypeResponseModel);
        }
        /// <summary>
        /// Add comission Sharing Models
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddcomissionSharingModels(CreateCommSharingModelDto CommSharingModelDto, CancellationToken cancellationToken)
        {
            var commSharingResponseModel = await _serviceManager.ComissionService.CreateComissionSharingModelAsync(CommSharingModelDto, cancellationToken);
            return Ok(commSharingResponseModel);
        }
        /// <summary>
        /// Add comission Sharing Models Dtls
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddcomissionSharingModelsDtls(CreateCommSharingModelDtlsDto CommSharingModelDto, CancellationToken cancellationToken)
        {
            var commSharingResponseModel = await _serviceManager.ComissionService.CreateComissionSharingModelDtlsAsync(CommSharingModelDto, cancellationToken);
            return Ok(commSharingResponseModel);
        }
        /// <summary>
        /// Update comission recive staus Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditcomissionRecivestausModels(ComissionReciveableStatusDto comissionUpdateDto, CancellationToken cancellationToken)
        {
            var commUpdateModel = await _serviceManager.ComissionService.EditReceivablesStatusAsync(comissionUpdateDto, cancellationToken);
            return Ok(commUpdateModel);
        }
        /// <summary>
        /// Update comission recive details Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditcomissionReciveDtlsModels(UpdateCommReceivablesDtlsDto comissionUpdateDto, CancellationToken cancellationToken)
        {
            var commUpdateModel = await _serviceManager.ComissionService.EditReceivablesDtlsAsync(comissionUpdateDto, cancellationToken);
            return Ok(commUpdateModel);
        }
        /// <summary>
        /// Update comission sharing Models
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditcomissionsharingModels(UpdateCommSharingModelDto comissionUpdateDto, CancellationToken cancellationToken)
        {
            var commUpdateModel = await _serviceManager.ComissionService.EditCommSharingModelAsync(comissionUpdateDto, cancellationToken);
            return Ok(commUpdateModel);
        }
        /// <summary>
        /// Update comission sharing Models Dtls
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> EditcomissionsharingModelsDtls(UpdateCommSharingModelDtlsDto comissionUpdateDto, CancellationToken cancellationToken)
        {
            var commUpdateModel = await _serviceManager.ComissionService.EditCommSharingModelDtlsAsync(comissionUpdateDto, cancellationToken);
            return Ok(commUpdateModel);
        }
        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchComissionReceiveable(DynamicSearchComissionReceiveableDto request, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ComissionService.GetDynamicSearchComissionReceiveable(request, cancellationToken);
            return Ok(serviceCreateModel);
        }

        [HttpPost]
        public async Task<ActionResult> GetDynamicSearchSharingModels(DynamicSearchComissionReceiveableDto request, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ComissionService.GetDynamicSearchSharingModels(request, cancellationToken);
            return Ok(serviceCreateModel);
        }
    }
}
