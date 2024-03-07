using Contracts;
using Contracts.Common;
using Contracts.Error;
using Contracts.Response;
using Contracts.Acquisition;
using Domain.Entities.Comission;
using Domain.Entities.Servicemanagement;
using Domain.Entities.UserManagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Acquisition;
using Contracts.Account;
using Contracts.Product;
using Domain.Entities.Account;
using Domain.Entities.Product;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;
        public ProductService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public async Task<ResponseModelDto> GetAllProductList(int channelId, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.productRepository.GetAllProductList(channelId, pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<ProductListDto>>();
                ProductListResponse userDetailResponse = new ProductListResponse() { ProductListDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> GetProductDetailsbyId(int productId,int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.productRepository.GetProductDetailsbyId(productId,pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<ProductDetailsbyIdDto>>();
                ProductDetailsbyIdResponse userDetailResponse = new ProductDetailsbyIdResponse() { ProductDetailsbyIdDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> ActivaeOrDeactivateProduct(int productId, int status, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                //if (!(status == 2 || status == 3))
                //{
                //    responseModelDto.Response = "Error";
                //    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                //    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.InValidStatus).ToString(),
                //    Error = Resource.InValidStatus } };
                //}
                //else
                //{
                    var result = await _repositoryManager.productRepository.ActivaeOrDeactivateProduct(productId, status, cancellationToken);
                    if (result > 0)
                    {
                        responseModelDto.ResponseCode = "0";
                        responseModelDto.Response = status == 3 ? "Product has been deactivated" : "Product has been activated";
                    }
                    else
                    {
                        responseModelDto.Response = "Error";
                        responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                    }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = "Error";
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModelDto;
        }

        public async Task<ResponseModelDto> AddProductName(AddProductDto entity, CancellationToken cancellationToken)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<AddProduct>();
                var response = await _repositoryManager.productRepository.AddProductName(serviceEntity, cancellationToken);
                //await _repositoryManager.productRepository.AddProductName(productId, productName, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = response;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Add Product Name");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> AddServiceOffering(AddServiceOfferingDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var serviceEntity = entity.Adapt<AddServiceOffering>();
                await _repositoryManager.productRepository.AddServiceOffering(serviceEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Add Service Offering");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;

        }

        public async Task<ResponseModelDto> DeactivateServiceOffering(int svcoffid, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                var result = await _repositoryManager.productRepository.DeactivateServiceOffering(svcoffid, cancellationToken);
                if (result > 0)
                {
                    responseModelDto.ResponseCode = "0";
                    //responseModelDto.Response = status == 3 ? "Product has been deactivated" : "Product has been activated";
                    responseModelDto.Response = "Success";
                }
                else
                {
                    responseModelDto.Response = "Error";
                    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = "Error";
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModelDto;
        }

        public async Task<ResponseModelDto> GetServiceSupplierListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.productRepository.GetServiceSupplierListbyServiceId( serviceid,  status, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ServiceSupplierListDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetServiceSupplierListbyServiceId {serviceid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetServiceProviderListbyServiceId(int serviceid, int status, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.productRepository.GetServiceProviderListbyServiceId(serviceid, status, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ServiceProviderListDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetServiceProviderListbyServiceId {serviceid}");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> GetServiceCSM( CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.productRepository.GetServiceCSM( cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
                var userDtos = allUsers.Adapt<IEnumerable<ServiceCSMListDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while GetServiceCSM ");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
        {
            ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin }
    };
            }

            return responseModel;

        }

        public async Task<ResponseModelDto> UpdateProductDetailsById(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModelDto = new ResponseModelDto();
            try
            {
                var serviceEntity = updateProductDto.Adapt<UpdateProduct>();
                var result = await _repositoryManager.productRepository.UpdateProductDetailsById(serviceEntity, cancellationToken);
                if (result > 0)
                {
                    responseModelDto.ResponseCode = "0";
                    responseModelDto.Response = "Product has been Updated";
                }
                else
                {
                    responseModelDto.Response = "Error";
                    responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.UserNotFound } };
                }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                responseModelDto.Response = "Error";
                responseModelDto.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModelDto;
        }
        Task<ResponseModelDto> IGenericService<ResponseModelDto>.AddAsync(ResponseModelDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        Task<ResponseModelDto> IGenericService<ResponseModelDto>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ResponseModelDto>.UpdateAsync(ResponseModelDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ResponseModelDto IGenericService<ResponseModelDto>.Validate(ResponseModelDto entity)
        {
            throw new NotImplementedException();
        }

    }
}
