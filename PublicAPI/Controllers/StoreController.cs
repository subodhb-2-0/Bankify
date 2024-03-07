using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


        [HttpGet(Name = "GetMerchandise")]
        public async Task<ActionResult> GetMerchandiseAsync(CancellationToken cancellationToken)
        {
            var response = await _storeService.GetMerchandiseAsync(cancellationToken);
            return Ok(response);
        }

        [HttpGet(Name = "GetOrderAddress")]
        public async Task<ActionResult> GetOrderAddress(int orgId, CancellationToken cancellationToken)
        {
            var response = await _storeService.GetOrderAddressAsync(orgId, cancellationToken);
            return Ok(response);
        }

        [HttpPost(Name = "PlaceOrder")]
        public async Task<ActionResult> PlaceOrder(PlaceOrderDto request, CancellationToken cancellationToken)
        {
            var response = await _storeService.PlaceOrderAsync(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost(Name = "GetOrderHistoryList")]
        public async Task<ActionResult> GetOrderHistoryList(OrderHistoryListDto request, CancellationToken cancellationToken)
        {
            var response = await _storeService.GetOrderHistoryListAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
