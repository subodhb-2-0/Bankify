using Domain.RepositoryInterfaces;
using Logger;
using Services.Abstractions;
using Services.HttpRequestHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRoleService> _lazyRoleService;
        private readonly Lazy<IUsersService> _lazyUsersService;
        private readonly Lazy<IMasterDataService> _lazyMasterDataService;
        private readonly Lazy<IComissionService> _lazyComissionService;
        private readonly Lazy<IServiceManagementRepositoryService> _lazyserviceManagementRepositoryService;
        private readonly Lazy<IAcquisitionService> _lazyacquisitionService;
        private readonly Lazy<IWorkingCapialService> _lazyWorkingCapitalService;
        private readonly Lazy<IFundTransferService> _lazyFundTransferService;
        private readonly Lazy<IChannelPartnerService> _lazyChannelPartnerService;
        private readonly Lazy<ISalesChannelService> _lazySalesPartnerService;
        private readonly Lazy<IReportService> _lazyReportService;

        //private readonly Lazy<IInventoryService> _lazyInventoryService;
        private readonly Lazy<IAccountService> _lazyacountService;
        private readonly Lazy<IProductService> _lazyproductService;
        private readonly Lazy<IBbpsService> _lazybbpsService;
        private readonly Lazy<IAEPSService> _lazyaEPSService;

        public ServiceManager(IRepositoryManager repositoryManager, ICustomLogger logger, IHttpRequestHandlerService httpRequestHandlerService)
        {
            _lazyRoleService = new Lazy<IRoleService>(() => new RoleService(repositoryManager, logger));
            _lazyUsersService = new Lazy<IUsersService> (() => new UsersService(repositoryManager, logger));
            _lazyMasterDataService = new Lazy<IMasterDataService>(() => new MasterDataService(repositoryManager));
            //_lazyUsersService = new Lazy<IUsersService> (() => new UsersService(repositoryManager));
            _lazyComissionService = new Lazy<IComissionService> (() => new ComissionService(repositoryManager, logger));
            _lazyserviceManagementRepositoryService = new Lazy<IServiceManagementRepositoryService> (() => new ServiceManagementService(repositoryManager, logger));
            //_lazyAccountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager));
            _lazyacquisitionService = new Lazy<IAcquisitionService>(() => new AcquisitionService(repositoryManager, logger));
            _lazySalesPartnerService = new Lazy<ISalesChannelService>(() => new SalesChannelService(repositoryManager, logger));
            _lazyChannelPartnerService = new Lazy<IChannelPartnerService>(() => new ChannelPartnerService(repositoryManager, logger));
            _lazyFundTransferService = new Lazy<IFundTransferService>(() => new FundTransferService(repositoryManager, logger));
            _lazyWorkingCapitalService = new Lazy<IWorkingCapialService>(() => new WorkingCapitalService(repositoryManager, logger));
            _lazyReportService = new Lazy<IReportService>(() => new ReportService(repositoryManager, logger));
            //_lazyInventoryService = new Lazy<IInventoryService>(() => new InventoryService(repositoryManager, logger));
            _lazyacountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, logger));
            _lazyproductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, logger));
            _lazybbpsService = new Lazy<IBbpsService>(() => new BbpsService(repositoryManager, logger));
            _lazyaEPSService = new Lazy<IAEPSService>(() => new AEPSService(repositoryManager, logger, httpRequestHandlerService));
        }
        public IRoleService RoleService => _lazyRoleService.Value;
        public IUsersService UsersService => _lazyUsersService.Value;
        public IMasterDataService MasterDataService => _lazyMasterDataService.Value;

        public IComissionService ComissionService => _lazyComissionService.Value;
        public IServiceManagementRepositoryService ServiceManagementService => _lazyserviceManagementRepositoryService.Value;

        public IAcquisitionService AcquisitionService => _lazyacquisitionService.Value;

        public IWorkingCapialService WorkingCapitalService => _lazyWorkingCapitalService.Value ;

        public IReportService ReportService => _lazyReportService.Value;

        public IChannelPartnerService ChannelPartnerService => _lazyChannelPartnerService.Value;

        public ISalesChannelService SalesPartnerService => _lazySalesPartnerService.Value;

        public IFundTransferService FundTransferService => _lazyFundTransferService.Value;
        //public IReportService ReportService => throw new NotImplementedException();
        public IAccountService AccountService => _lazyacountService.Value;
        public IProductService ProductService => _lazyproductService.Value;
        public IBbpsService bbpsService => _lazybbpsService.Value;
        public IAEPSService aEPSService => _lazyaEPSService.Value;
        //IRoleService IServiceManager.RoleService => throw new NotImplementedException();
        //public IAccountService AccountService => _lazyAccountService.Value;
    }
}
