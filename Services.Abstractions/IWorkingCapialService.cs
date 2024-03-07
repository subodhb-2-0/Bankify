using Contracts.Common;
   using Contracts.WorkingCapital;

namespace Services.Abstractions
{
    public interface IWorkingCapialService
    {
        Task<ResponseModelDto> InititatePayment(WCInitiatePaymentDto request, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetListOfPaymentMode(int orgId, int orgType, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> CheckWCPayments(WCPaymentRequestDTO requestDTO, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetOrgDetail(CancellationToken cancellationToken = default);
    }
}