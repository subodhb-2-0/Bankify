using Domain.Entities.Acquisition;
using Domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Acquisition;
using Domain.Entities.Product;
using Contracts.Onboarding;
using Domain.Entities.Onboarding;

namespace Domain.RepositoryInterfaces
{
    public interface IProductRepository : IGenericRepository<ProductList>
    {
        Task<Tuple<IEnumerable<ProductList>, int>> GetAllProductList(int channelId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<ProductDetailsbyId>, int>> GetProductDetailsbyId(int productId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);

        Task<int> ActivaeOrDeactivateProduct(int productId, int status, CancellationToken cancellationToken = default);
        Task<AddAcquisitionResponse> AddProductName(AddProduct addProduct, CancellationToken cancellationToken = default);

        Task<AddServiceOffering> AddServiceOffering(AddServiceOffering addServiceOffering, CancellationToken cancellationToken = default);
        Task<int> DeactivateServiceOffering(int svcoffid, CancellationToken cancellationToken = default);

        Task<IEnumerable<ServiceSupplierList>> GetServiceSupplierListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceProviderList>> GetServiceProviderListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default);

        Task<IEnumerable<ServiceCSMList>> GetServiceCSM(CancellationToken cancellationToken = default);
        Task<int> UpdateProductDetailsById(UpdateProduct updateProduct, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductDetails(CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductByIdAsync(int id,CancellationToken cancellationToken = default);

    }
}
