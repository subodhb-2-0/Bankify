using Contracts.Account;
using Contracts.Common;
using Contracts.FundTransfer;

namespace Services.Abstractions
{
    public interface IFundTransferService
    {
        Task<ResponseModelDto> GetOrgDetails(int orgId, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListOfChannelPartner(int distributororgid, int retailerorgid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetChannelPartnerList(int distributorid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> InitiateFundTransfer(FundTransferRequestDto request, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UPIUpdatePayment(PaymentUpdateInModelDto paymentUpdateInModel, CancellationToken cancellationToken = default);
    }
}