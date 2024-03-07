
namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IRoleService RoleService { get; }
        IUsersService UsersService { get; }
        IChannelPartnerService ChannelPartnerService { get; }
        ISalesChannelService SalesPartnerService { get; }
        IMasterDataService MasterDataService{ get; }
        IComissionService ComissionService { get; }
        IServiceManagementRepositoryService ServiceManagementService { get; }
        IAcquisitionService AcquisitionService { get; }
        IWorkingCapialService WorkingCapitalService { get; }
        IFundTransferService FundTransferService { get; }
        IReportService ReportService { get; }
        IAccountService AccountService { get; }
        IProductService ProductService { get; }
        IBbpsService bbpsService { get; }
        IAEPSService aEPSService { get; }
    }
}
