using Domain.Entities.Onboarding;

namespace Domain.RepositoryInterfaces
{
    public interface IChannelRepository: IGenericRepository<Channel> {
        Task<IEnumerable<Channel>> GetAllByStatusAsync(int status, CancellationToken cancellationToken = default);
    }
}
