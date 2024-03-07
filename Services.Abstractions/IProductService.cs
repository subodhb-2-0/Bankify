using Contracts;
using Contracts.Acquisition;
using Contracts.Onboarding;
using Contracts.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Product;

namespace Services.Abstractions
{
    public interface IProductService : IGenericService<ResponseModelDto>
    {
        Task<ResponseModelDto> GetAllProductList(int channelId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetProductDetailsbyId(int productId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> ActivaeOrDeactivateProduct(int productId, int status, CancellationToken cancellationToken = default);

        Task<ResponseModelDto> AddProductName(AddProductDto addProductDto, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> AddServiceOffering(AddServiceOfferingDto addServiceOffering, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> DeactivateServiceOffering(int svcoffid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetServiceSupplierListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetServiceProviderListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetServiceCSM( CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateProductDetailsById(UpdateProductDto updateProductDto, CancellationToken cancellationToken = default);
    }
    
}
