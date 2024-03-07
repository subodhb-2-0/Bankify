namespace Domain.RepositoryInterfaces
{
    public interface IRepositoryManager
    {
        public IRoleRepository roleRepository { get; }
        public IUsersRepository usersRepository { get; }
        public IComissionRepository comissionRepository { get; }
        public IMasterDataRepository masterDataRepository { get; }
        public IOTPRepository oTPRepository { get; }
        public IServiceManagementRepository serviceManagementRepository { get; }
        public IAcquisitionRepository acquisitionRepository { get; }
        public ITransactionRepository workingCapitalRepository { get; }
        public IChhannelPartnerRepository channelPartnerRepository { get; }
        public IChannelRepository channelRepository { get; }
        public IInventoryRepository inventoryRepository { get; }
        public IInventoryDetailsRepository inventoryDetailsRepository { get; }
        public IProductRepository productRepository { get; }
        public IAccountRepository accountRepository { get; }
        public IReportRepository reportsRepository { get; }
        public IBbpsRepository bbpsRepository { get; }
        public ICpBcOnboardingRepository cpBcOnboardingRepository { get; }
    }
}
