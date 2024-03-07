using Contracts.Report;
using Domain.RepositoryInterfaces;

namespace Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task<IEnumerable<TransactionDto>> GetWorkingCapitals(string loginId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
