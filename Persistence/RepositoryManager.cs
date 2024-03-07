using Domain.RepositoryInterfaces;
using Persistence.Onboarding;

namespace Persistence
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IRoleRepository> _lazyroleRepository;
        private readonly Lazy<IUsersRepository> _lazyusersRepository;
        private readonly Lazy<IMasterDataRepository> _lazyMasterDataRepository;
        private readonly Lazy<IOTPRepository> _lazyotpRepository;
        private readonly Lazy<IComissionRepository> _lazycomissionRepository;
        private readonly Lazy<IServiceManagementRepository> _lazyserviceManagementRepository;
        private readonly Lazy<IAcquisitionRepository> _lazyacquisitionRepositoryRepository;
        private readonly Lazy<ITransactionRepository> _lazyWorkingRepository;
        private readonly Lazy<IChhannelPartnerRepository> _lazyChannelPartnerRepository;
        private readonly Lazy<IChannelRepository> _lazyChannelRepository;
        private readonly Lazy<IInventoryRepository> _lazyInventoryRepository;
        private readonly Lazy<IInventoryDetailsRepository> _lazyInventoryDetailsRepository;
        private readonly Lazy<IProductRepository> _lazyProductRepository;
        private readonly Lazy<IAccountRepository> _lazyaccountRepositoryRepository;
        private readonly Lazy<IReportRepository> _lazyreportRepository;
        private readonly Lazy<IBbpsRepository> _lazybbpsRepository;
        private readonly Lazy<ICpBcOnboardingRepository> _lazycpBcOnboardingRepository;

        public RepositoryManager(DapperContext context)
        {
            _lazyroleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _lazyusersRepository = new Lazy<IUsersRepository>(() => new UsersRepository(context));
            _lazyMasterDataRepository = new Lazy<IMasterDataRepository>(() => new MasterDataRepository(context));
            _lazyotpRepository = new Lazy<IOTPRepository>(() => new OTPRepository(context));
            _lazyMasterDataRepository = new Lazy<IMasterDataRepository>(() => new MasterDataRepository(context));            
            _lazycomissionRepository = new Lazy<IComissionRepository>(() => new ComissionRepository(context));
            _lazyserviceManagementRepository = new Lazy<IServiceManagementRepository>(() => new ServiceManagementRepository(context));
            _lazyacquisitionRepositoryRepository = new Lazy<IAcquisitionRepository>(() => new AcquisitionRepository(context));
            _lazyChannelPartnerRepository = new Lazy<IChhannelPartnerRepository>(() => new ChannelPartnerRepository(context));
            _lazyChannelRepository = new Lazy<IChannelRepository>(() => new ChannelRepository(context));
            _lazyInventoryRepository = new Lazy<IInventoryRepository>(() => new InventoryRepository(context));
            _lazyInventoryDetailsRepository = new Lazy<IInventoryDetailsRepository>(() => new InventoryDetailsRepository(context));
            _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
            //_lazyAccountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(dbContext));
            //_lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));

            _lazyaccountRepositoryRepository = new Lazy<IAccountRepository>(() => new AccountRepository(context));
            _lazyreportRepository = new Lazy<IReportRepository>(() => new ReportsRepository(context));
            _lazybbpsRepository = new Lazy<IBbpsRepository>(() => new BbpsRepository(context));
            _lazycpBcOnboardingRepository = new Lazy<ICpBcOnboardingRepository>(() => new CpBcOnboardingRepository(context));
        }

        public IRoleRepository roleRepository => _lazyroleRepository.Value;
        public IUsersRepository usersRepository => _lazyusersRepository.Value;
        public IMasterDataRepository masterDataRepository => _lazyMasterDataRepository.Value;
        public IOTPRepository oTPRepository => _lazyotpRepository.Value;

 

        public IComissionRepository comissionRepository => _lazycomissionRepository.Value;
        public IServiceManagementRepository serviceManagementRepository => _lazyserviceManagementRepository.Value;

        public IAcquisitionRepository acquisitionRepository => _lazyacquisitionRepositoryRepository.Value;
        public ITransactionRepository workingCapitalRepository => _lazyWorkingRepository.Value;

        public IChhannelPartnerRepository channelPartnerRepository => _lazyChannelPartnerRepository.Value;

        public IChannelRepository channelRepository => _lazyChannelRepository.Value;

        public IInventoryRepository inventoryRepository => _lazyInventoryRepository.Value;

        public IInventoryDetailsRepository inventoryDetailsRepository => _lazyInventoryDetailsRepository.Value;

        public IProductRepository productRepository => _lazyProductRepository.Value;
        public IAccountRepository accountRepository => _lazyaccountRepositoryRepository.Value;
        public IReportRepository reportsRepository => _lazyreportRepository.Value;
        public IBbpsRepository bbpsRepository => _lazybbpsRepository.Value;

        public ICpBcOnboardingRepository cpBcOnboardingRepository => _lazycpBcOnboardingRepository.Value;
    }
}
