using Contracts.Report;
using Contracts.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ReportController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly JwtSettings jwtSettings;
        private readonly IPAddressHelper _ipAddressHelper;
        public ReportController(IServiceManager serviceManager, JwtSettings jwtSettings)
        {
            _serviceManager = serviceManager;
            this.jwtSettings = jwtSettings;
            _ipAddressHelper = new IPAddressHelper();
        }
        /// <summary>
        /// Get All Service Catagory
        /// </summary>
        /// <returns>Service Catagory</returns>
        [HttpPost(Name = "viewTxnsOfCPs")]
        public async Task<ActionResult> ViewTxnsOfCPs(TransactionRequestDto requestDto, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.ViewTxnsOfCPs(requestDto, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "viewStates")]
        public async Task<ActionResult> getListofState(CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListofState( cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "getListofChannel")]
        public async Task<ActionResult> getListofChannel(CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListofChannel(cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "getListofPaymentMode")]
        public async Task<ActionResult> getListofPaymentMode(int? orgId, int? orgType, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListofPaymentMode(orgId, orgType, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "getListofServices")]
        public async Task<ActionResult> getListofServices(CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListofServices(cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "getListofSuppliers")]
        public async Task<ActionResult> getListofSuppliers(int serviceId, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListofSuppliers(serviceId, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpGet(Name = "getListofServiceProviders")]
        public async Task<ActionResult> getListofServiceProviders(int serviceId, int supplierId, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListofServiceProviders(serviceId, supplierId, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost(Name = "getCPLedger")]
        public async Task<ActionResult> getCPLedger(LedgerRequestDto request, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getCPLedger(request, cancellationToken);
            return Ok(serviceCreateModel);
        }
        [HttpPost(Name = "getListOfTranfers")]
        public async Task<ActionResult> getListOfTranfers(BalanceTransferRequestDto request, CancellationToken cancellationToken)
        {
            var serviceCreateModel = await _serviceManager.ReportService.getListOfTranfers(request, cancellationToken);
            return Ok(serviceCreateModel);
        }
        // code added by biswa
        //HttpPost: api/<GetTxnDetails>
        [HttpPost]
        [ActionName("GetTxnDetails")]
        public async Task<ActionResult> GetTxnDetails(gettxndetailsreportsDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GettxnReports(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<Getretailerledger>
        [HttpPost]
        [ActionName("Getretailerledger")]
        public async Task<ActionResult> Getretailerledger(GetretailerledgerDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.Gettxnledger(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<Getretailerledger>
        [HttpPost]
        [ActionName("Getwithdrawalledger")]
        public async Task<ActionResult> Getwithdrawalledger(cashwithdrawalledgerRequestDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.Getwithdrawalledger(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetAllpaymentdetails>
        [HttpPost]
        [ActionName("GetAllpaymentdetails")]
        public async Task<ActionResult> GetAllpaymentdetails(GetallpaymentdetailsDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetAllpaymentdetailsAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetAllpaymentdetails>
        [HttpPost]
        [ActionName("GetAllpaymentHistory")]
        public async Task<ActionResult> GetAllpaymentHistory(GetPaymentHistoryDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetAllpaymentsHistoryAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetTxnCancelOrSsucsessdetails>
        [HttpPost]
        [ActionName("GetTxnCancelOrSsucsessdetails")]
        public async Task<ActionResult> GetTxnCancelOrSsucsessdetails(GetcancelTxnDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetTxnCancelOrSsucsessDetailsAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetRetalierSalsAndCancelation>
        [HttpPost]
        [ActionName("GetRetalierSalsAndCancelation")]
        public async Task<ActionResult> GetRetalierSalsAndCancelation(RetalierSalsAndCancelationDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetRetalierSalsAndCancelationAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetDistributerSalsSalsAndCancelation>
        [HttpPost]
        [ActionName("GetDistributerSalsAndCancelation")]
        public async Task<ActionResult> GetDistributerSalsAndCancelation(DistributerSalsAndCancelationDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetDistributerSalsAndCancelationAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetchannelserviceSalsSalsAndCancelation>
        [HttpPost]
        [ActionName("GetchannelserviceSalsAndCancelation")]
        public async Task<ActionResult> GetchannelserviceSalsAndCancelation(ChannelParametersRequestDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetchannelserviceSalsAndCancelationAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }
        //HttpPost: api/<GetTransactionDetailsByTxnId>
        [HttpPost]
        [ActionName("GetTransactionDetailsByTxnId")]
        public async Task<ActionResult> GetTransactionDetailsByTxnId(long p_txnid, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetTransactionDetailsByTxnIdAsync(p_txnid, cancellationToken);
            return Ok(reportsModel);
        }

        [HttpPost]
        [ActionName("GetRetailerTxnReport")]
        public async Task<ActionResult> GetRetailerTxnReport(RetailerTxnReportDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetRetailerTxnReportAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }

        [HttpPost]
        [ActionName("GetPurchaseOrderDetails")]
        public async Task<ActionResult> GetPurchaseOrderDetails(PurchaseOrderReportDto purchaseOrderReportDto, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetPurchaseOrderDetailsAsync(purchaseOrderReportDto, cancellationToken);
            return Ok(reportsModel);
        }

        [HttpPost]
        [ActionName("GetCpOrderReportDetails")]
        public async Task<ActionResult> GetCpOrderReportDetails(OrderReportRequestDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetOrderReportCpAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }

        [HttpPost]
        [ActionName("GetInventoryReportDetails")]
        public async Task<ActionResult> GetInventoryReportDetails(InventoryReportRequestDto entity, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetInventoryReportAsync(entity, cancellationToken);
            return Ok(reportsModel);
        }

        [HttpPost]
        [ActionName("GetInventoryIdDetails")]
        public async Task<ActionResult> GetInventoryIdDetails(int p_inventoryid, CancellationToken cancellationToken)
        {
            var reportsModel = await _serviceManager.ReportService.GetInventoryIdDetailsAsync(p_inventoryid, cancellationToken);
            return Ok(reportsModel);
        }
    }

}
