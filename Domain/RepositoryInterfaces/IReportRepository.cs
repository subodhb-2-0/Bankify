using Contracts.Report;
using Domain.Entities.Reports;

namespace Domain.RepositoryInterfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<txndetailsresponse>> GetTxnreportDetails(gettxndetailsreports entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetretailerledgerResponse>> GetledgerDetails(Getretailerledger entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetretailerledgerResponse>> GetwithdrawalledgerDetails(cashwithdrawalledgerRequest entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<AllpaymentdetailsResponse>> GetAllpaymentdetails(Getallpaymentdetails entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetPaymentHistoryResponse>> GetAllPaymentHistory(GetPaymentHistory entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetcancelTxnResponse>> GetTxnCancelOrSsucsessDetails(GetcancelTxn entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<RetalierSalsAndCancelationResponse>> GetRetalierSalsAndCancelation(RetalierSalsAndCancelation entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<DistributerSalsAndCancelationResponse>> GetDistributerSalsAndCancelation(DistributerSalsAndCancelationModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<State>> getListofState(CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceDto>> getListofServices(CancellationToken cancellationToken = default);
        Task<IEnumerable<SupplierDto>> getListofSuppliers(int serviceId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceProviderDto>> getListofServiceProviders(int serviceId,int supplierId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentModeDto>> getListofPaymentMode(int? orgid, int? orgType, CancellationToken cancellationToken = default);
        Task<IEnumerable<channelserviceSalesandCancelResponseModel>> GetchannelserviceSalsAndCancelation(ChannelParametersRequestModel entity, CancellationToken cancellationToken = default);
        Task<TransactionDetailsByTxnId> GetTransactionDetailsByTxnId(long p_txnid, CancellationToken cancellationToken = default);

        Task<IEnumerable<TransactionResponse>> ViewTxnsOfCPs(TransactionRequest entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<RetailerTxnReportResponse>> GetRetailerTxnReport(RetailerTxnReport entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<PurchaseOrderDetailsReport>> GetPurchaseOrderDetailsAsync(PurchaseOrderReportDto purchaseOrderReportDto, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderReportResponseModel>> GetOrderReportCP(OrderReportRequestModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<InventoryReportResponseModel>> GetInventoryReport(InventoryReportRequestModel entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<InventoryIdDetailsResponse>> GetInventoryIdDetails(int p_inventoryid, CancellationToken cancellationToken = default);
    }
}
