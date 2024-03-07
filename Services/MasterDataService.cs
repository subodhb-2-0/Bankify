using Contracts;
using Contracts.Common;
using Contracts.Error;
using Domain.Entities.UserManagement;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;
using Mapster;
using Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class MasterDataService : IMasterDataService
    {
       
        private readonly IRepositoryManager _repositoryManager;

        public MasterDataService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public Task<ResponseModelDto> AddAsync(MasterDataDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetDepartmentListAsync(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {

                var departmentList = await _repositoryManager.masterDataRepository.GetDepartmentListAsync(cancellationToken);

                var departmentListDto = departmentList.Adapt< IEnumerable<MasterDataDto>>();
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                    responseModel.Data = departmentListDto;
                 
            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetDesignationListAsync(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {

                var designationList = await _repositoryManager.masterDataRepository.GetDesignationListAsync(cancellationToken);

                var designationListDto = designationList.Adapt<IEnumerable<MasterDataDto>>();
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = designationListDto;

            }
            catch (Exception ex)
            {
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public Task<ResponseModelDto> UpdateAsync(MasterDataDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ResponseModelDto Validate(MasterDataDto entity)
        {
            throw new NotImplementedException();
        }
    }
}