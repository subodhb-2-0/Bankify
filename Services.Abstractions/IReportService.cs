using Contracts.Common;
using Contracts.Report;

namespace Services.Abstractions
{
    public interface IReportService
    {
        Task<ResponseModelDto> ViewTxnsOfCPs(TransactionRequestDto requestDto, CancellationToken cancellationToken);
        Task<ResponseModelDto> getListofState(CancellationToken cancellationToken);
        Task<ResponseModelDto> getListofChannel(CancellationToken cancellationToken);
        Task<ResponseModelDto> getListofPaymentMode(int? orgId, int? orgType, CancellationToken cancellationToken);
        Task<ResponseModelDto> getListofServices(CancellationToken cancellationToken);
        Task<ResponseModelDto> getListofSuppliers(int serviceId, CancellationToken cancellationToken);
        Task<ResponseModelDto> getListofServiceProviders(int serviceId, int supplierId, CancellationToken cancellationToken);
        Task<ResponseModelDto> getCPLedger(LedgerRequestDto request, CancellationToken cancellationToken);
        Task<ResponseModelDto> getListOfTranfers(BalanceTransferRequestDto request, CancellationToken cancellationToken);
        // code added by biswa
        Task<ResponseModelDto> GettxnReports(gettxndetailsreportsDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> Gettxnledger(GetretailerledgerDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> Getwithdrawalledger(cashwithdrawalledgerRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllpaymentdetailsAsync(GetallpaymentdetailsDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllpaymentsHistoryAsync(GetPaymentHistoryDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetTxnCancelOrSsucsessDetailsAsync(GetcancelTxnDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetRetalierSalsAndCancelationAsync(RetalierSalsAndCancelationDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetDistributerSalsAndCancelationAsync(DistributerSalsAndCancelationDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetchannelserviceSalsAndCancelationAsync(ChannelParametersRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetTransactionDetailsByTxnIdAsync(long p_txnid, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetRetailerTxnReportAsync(RetailerTxnReportDto entity, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetPurchaseOrderDetailsAsync(PurchaseOrderReportDto purchaseOrderReportDto, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetOrderReportCpAsync(OrderReportRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetInventoryReportAsync(InventoryReportRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetInventoryIdDetailsAsync(int p_inventoryid, CancellationToken cancellationToken = default);
    }
}