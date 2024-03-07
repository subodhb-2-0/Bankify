using Contracts.Report;

namespace Domain.RepositoryInterfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionDto>> GetWorkingCapitals(string loginId, CancellationToken cancellationToken= default);
    }
}
