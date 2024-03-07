using Contracts.Bbps;
using Contracts.Common;
using Contracts.Report;
using Contracts.Security;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PublicAPI.Utility;
using Services.Abstractions;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using static Dapper.SqlMapper;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BbpsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;

        

        public BbpsController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }
        //HttpGet: api/<GetBillerInfo>
        [HttpGet]
        [ActionName("GetBillerInfo")]
        public async Task<ActionResult> GetBillerInfo(string billercode, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.GetBillerInfoAsync(billercode, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<insertbillerDetails>
        [HttpPost]
        [ActionName("insertbillerDetails")]
        public async Task<ActionResult> insertbillerDetails(BbpsBillerListDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.insertbillerAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetbillerDetails>
        [HttpPost]
        [ActionName("GetbillerDetails")]
        public async Task<ActionResult> GetbillerDetails(BbpsBillerSearchOptionsDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.GetbillerAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<InsertAgentDetails>
        [HttpPost]
        [ActionName("InsertAgentDetails")]
        public async Task<ActionResult> InsertAgentDetails(InsertBbpsAgentDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.InsertAgentAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetAgentDetails>
        [HttpPost]
        [ActionName("GetAgentDetails")]
        public async Task<ActionResult> GetAgentDetails(GetAgentRequestDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.GetAgentAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpGet: api/<GetBillerInputParams>
        [HttpGet]
        [ActionName("GetBillerInputParams")]
        public async Task<ActionResult> GetBillerInputParams(int bbpsbillerid, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.GetBillerInputParams(bbpsbillerid, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<UpdateBBPSBiller>
        [HttpPost]
        [ActionName("UpdateBBPSBillerStatus")]
        public async Task<ActionResult> UpdateBBPSBillerStatus(UpdateBBPSBillerModelDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.UpdateBBPSBillerStatus(entity, cancellationToken);
            return Ok(reportsModel);
        }

        [HttpPut]
        [ActionName("UpdateBBPSBiller")]
        public async Task<ActionResult> UpdateBBPSBiller(BbpsBillerListDto entity, CancellationToken cancellationToken)
        {
            string updatedBillerId = entity.p_billerid;

            //Call the insert biller method, which is common for both insert and update process.
            var reportsModel = await _serviceManager.bbpsService.insertbillerAsync(entity, cancellationToken);

            if(reportsModel.ResponseCode == "0")
            {
                
                BbpsBillerSearchOptionsDto objBbpsBillerSearchOptionsDto = new BbpsBillerSearchOptionsDto() { p_fetchrows = 10, p_offsetrows = 1, p_searchoptions = updatedBillerId, p_billercategoryid = 0 };
                var reportsModelUpdated = await _serviceManager.bbpsService.GetbillerAsync(objBbpsBillerSearchOptionsDto, cancellationToken);

                return Ok(reportsModelUpdated);
            }
            else
            {
                return Ok(reportsModel);
            }
            
        }

        [HttpGet]
        [ActionName("GetBillerCategoryList")]
        public async Task<ActionResult> GetBillerCategoryList(CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.bbpsService.GetBillerCategoryList(cancellationToken);
            return Ok(reportsModel);
        }

        [HttpGet]
        [ActionName("GetBbpsService")]
        public async Task<ActionResult> GetBbpsServiceResult(CancellationToken cancellationToken)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            int serviceId = Convert.ToInt32(configurationBuilder.GetSection("ServiceConfig")["ServiceId"]);

            var reportsModel = await _serviceManager.bbpsService.GetBbpsServiceResult(serviceId,cancellationToken);
            return Ok(reportsModel);
        }
    }
}
