using Contracts;
using Contracts.Common;
using Contracts.Constants;
using Contracts.Error;
using Contracts.Response;
using Domain.Entities.Comission;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class ComissionService : IComissionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public ComissionService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public Task<ResponseModelDto> AddAsync(ComissionDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModelDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComissionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ComissionDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseModelDto> GetAllCommisionTypeAsync( CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allComission = await _repositoryManager.comissionRepository.GetAllComissionType( cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = allComission.Adapt<IEnumerable<ComissionDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching Comission Type");
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

        public async Task<ResponseModelDto> GetCommisionTypeAsync(string commType, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allUsers = await _repositoryManager.comissionRepository.GetComissionType(commType, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = allUsers.Adapt<IEnumerable<ComissionDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Comission Type {commType}");
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
        public async Task<ResponseModelDto> GetbaseParamByCommTypeAsync(string Value2, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var allUsers = await _repositoryManager.comissionRepository.GetbaseParamByCommType(Value2, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var commDtos = allUsers.Adapt<IEnumerable<GetbaseParamByIdDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Comission Type {Value2}");
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
        public async Task<ResponseModelDto> GetCommReceivablesDtlsAsync(int commReceivableId, int Userid, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetCommReceivablesDtls(commReceivableId, Userid, cancellationToken);

                

                var commReceivablesDtlsDto = comission.Adapt<IEnumerable<CommReceivablesDtlsDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commReceivablesDtlsDto;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Comission Receivables {commReceivableId}");
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
        public async Task<ResponseModelDto> GetCommSharingModelDtlsAsync(int commSharingId, int Userid, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetCommSharingModelDtls(commSharingId, Userid, cancellationToken);



                var commSharingDtlsDto = comission.Adapt<IEnumerable<GetCommSharingModelDtlsDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commSharingDtlsDto;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Comission Sharing {commSharingId}");
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
        public async Task<ResponseModelDto> GetCommReceivablesDtlByMinAndMaxsAsync(int minimumValue, int maximumValue,int UserId, int commReceivableId, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetCommReceivablesDtlsByminAndMaxValue(minimumValue, maximumValue, UserId, commReceivableId, cancellationToken);



                var commReceivablesDtlsDto = comission.Adapt<CreateCommReceivablesDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commReceivablesDtlsDto;
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
        public async Task<ResponseModelDto> GetCommSharingDtlByMinAndMaxsAsync(int minimumValue, int maximumValue, int UserId, int commSharingId, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetCommSharingDtlsByminAndMaxValue(minimumValue, maximumValue, UserId, commSharingId, cancellationToken);



                var commReceivablesDtlsDto = comission.Adapt<CreateCommSharingModelDtlsDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commReceivablesDtlsDto;
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
        public async Task<ResponseModelDto> GetRecentIdOfComissionReciveAsync(string crname, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetRecentIdOfComissionRecive(crname, cancellationToken);



                var commReceivablesDtlsDto = comission.Adapt<ComissionReciveIdReturnDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commReceivablesDtlsDto;
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
        public async Task<ResponseModelDto> GetRecentIdOfComissionSharingAsync(string csmname, CancellationToken cancellationToken = default)
        {

            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetRecentIdOfComissionSharing(csmname, cancellationToken);



                var commReceivablesDtlsDto = comission.Adapt<ComissionReciveIdReturnDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commReceivablesDtlsDto;
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
        public async Task<ResponseModelDto> GetCommReceivablesStatus(int CRID, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var comission = await _repositoryManager.comissionRepository.GetCommReceivablesStatusAsync(CRID, cancellationToken);
                var commReceivablesDtlsDto = comission.Adapt<ComissionReciveableStatusDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = commReceivablesDtlsDto;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching Comm Receivables {CRID}");
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

    
        public async Task<ResponseModelDto> GetCommisionSharingAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allcommsharing = await _repositoryManager.comissionRepository.GetComissionSharingModel(pageSize,pageNumber,orderByColumn,orderBy,searchBy, cancellationToken);
                var commDtos = allcommsharing.Item1.Adapt<IEnumerable<ComissionSharingDto>>();
                CommSharingResponse commManagementResponse = new()
                { 
                    CommDetails = commDtos, TotalRecord = allcommsharing.Item2,
                    PageNumber = pageNumber, PageSize = pageSize,
                    OrderBy = orderBy, orderByColumn = orderByColumn
                };

                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = commManagementResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching Comm Sharing Model");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                            ErrorCode = CommonErrorCode.ContactToAdmin,
                                            Error = Resource.ContactToAdmin }
                                        };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetComissionReceivableAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allcommrecive = await _repositoryManager.comissionRepository.GetComissionReceivableModel(pageSize, pageNumber,orderByColumn,orderBy,searchBy, cancellationToken);
                var commDtos = allcommrecive.Item1.Adapt<IEnumerable<ComissionReceivableDto>>();
                CommReciveResponse commManagementResponse = new CommReciveResponse()
                {
                    CommDetails = commDtos, TotalRecord = allcommrecive.Item2,
                    PageNumber = pageNumber, PageSize = pageSize,
                    OrderBy = orderBy, orderByColumn = orderByColumn 
                };

                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = commManagementResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fatching Comm recive Model");
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto()
                                        {
                                            ErrorCode = CommonErrorCode.ContactToAdmin,
                                            Error = Resource.ContactToAdmin }
                                        };
            }
            return responseModel;
        }
        
        public Task<ResponseModelDto> UpdateAsync(ComissionDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ResponseModelDto Validate(ComissionDto entity)
        {
            ResponseModelDto responseModel = null;


            var context = new ValidationContext(entity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                responseModel = new ResponseModelDto() { Errors = new List<ErrorModelDto>() };
                foreach (var result in results)
                {

                    var _error = result.ToString().Split('|');

                    responseModel.ResponseCode = _error[0];
                    responseModel.Response = _error[1];
                    responseModel.Errors.Add(new ErrorModelDto() { Error = _error[1], ErrorCode = _error[1] });
                }

            }
            return responseModel;
        }
        public ResponseModelDto ValidateCommReceivablesDtls(CreateCommReceivablesDto entity)
        {
            ResponseModelDto responseModel = null;


            var context = new ValidationContext(entity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                responseModel = new ResponseModelDto() { Errors = new List<ErrorModelDto>() };
                foreach (var result in results)
                {

                    var _error = result.ToString().Split('|');

                    responseModel.ResponseCode = _error[0];
                    responseModel.Response = _error[1];
                    responseModel.Errors.Add(new ErrorModelDto() { Error = _error[1], ErrorCode = _error[1] });
                }

            }
            return responseModel;
        }
        public ResponseModelDto ValidateCommSharingModelDtls(CreateCommSharingModelDtlsDto entity)
        {
            ResponseModelDto responseModel = null;


            var context = new ValidationContext(entity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                responseModel = new ResponseModelDto() { Errors = new List<ErrorModelDto>() };
                foreach (var result in results)
                {

                    var _error = result.ToString().Split('|');

                    responseModel.ResponseCode = _error[0];
                    responseModel.Response = _error[1];
                    responseModel.Errors.Add(new ErrorModelDto() { Error = _error[1], ErrorCode = _error[1] });
                }

            }
            return responseModel;
        }
        public async Task<ResponseModelDto> CreateComissionReceivableAsync(CreateComissionReceivableDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var _comissionModel = entity.Adapt<CreateComissionReceivable>();

                bool isDuplicate = await _repositoryManager.comissionRepository.CheckDuplicateName(_comissionModel);
                if (isDuplicate == true)
                {
                    responseModel.ResponseCode = "1";
                    responseModel.Response = $"Duplicate name found: {entity.ComissionReceivableName}";
                }
                else
                {
                await _repositoryManager.comissionRepository.AddComissionReceivableAsync(_comissionModel, cancellationToken);
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create Comission Receivable Model");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
            
        }
        public async Task<ResponseModelDto> CreateComissionSharingModelAsync(CreateCommSharingModelDto entity, CancellationToken cancellationToken = default)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                var existingcommonRcvDtls = await _repositoryManager.comissionRepository.GetComissionSharingModelAsync(entity.CommSharingModelName, cancellationToken);
                if (existingcommonRcvDtls != null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ComissionManagementError.ComissionModelAlreadyExist,
                    Error = Resource.ComissionModelIsExit } };
                }
                else
                {
                    var _comissionModel = entity.Adapt<CreateCommSharingModel>();
                    await _repositoryManager.comissionRepository.AddComissionSharingModelAsync(_comissionModel, cancellationToken);
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create Comission Sharing Model");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
            //var _comissionModel = entity.Adapt<CreateCommSharingModel>();
            //var _responseModel = await _repositoryManager.comissionRepository.AddComissionSharingModelAsync(_comissionModel, cancellationToken);
            //return _responseModel.Adapt<ResponseModelDto>();
        }
        public async Task<ResponseModelDto> CreateComissionReceivableDetlsAsync(CreateCommReceivablesDto entity, CancellationToken cancellationToken)
        {
            var responseModel = ValidateCommReceivablesDtls(entity);
            if (responseModel != null) return await Task.FromResult(responseModel);
            responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var existingcommonRcvDtls = await _repositoryManager.comissionRepository.GetCommReceivablesDtlsByminAndMaxValue(entity.maximumValue,entity.maximumValue,entity.UserId,entity.commReceivableId, cancellationToken);

                if (existingcommonRcvDtls != null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ComissionManagementError.ComissionAlreadyExist,
                    Error = Resource.ComissionIsExit } };
                }
                else
                {
                    var commEntity = entity.Adapt<CreateCommReceivablesDtls>();
                    await _repositoryManager.comissionRepository.AddComissionReceivableDtlsAsync(commEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";

                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create Comission Receivable Detls");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> CreateComissionSharingModelDtlsAsync(CreateCommSharingModelDtlsDto entity, CancellationToken cancellationToken)
        {
            var responseModel = ValidateCommSharingModelDtls(entity);
            if (responseModel != null) return await Task.FromResult(responseModel);
            responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var existingcommonSharDtls = await _repositoryManager.comissionRepository.GetCommSharingDtlsByminAndMaxValue(entity.maximumValue, entity.maximumValue, entity.UserId,entity.commSharingId, cancellationToken);

                if (existingcommonSharDtls != null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ComissionManagementError.ComissionAlreadyExist,
                    Error = Resource.ComissionIsExit } };
                }
                else
                {
                    var commEntity = entity.Adapt<CreateCommSharingModelDtls>();
                    await _repositoryManager.comissionRepository.AddComissionSharingModelDtlsAsync(commEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";

                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Create Comission Sharing Detls");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditReceivablesStatusAsync(ComissionReciveableStatusDto entity, CancellationToken cancellationToken)
        {
            
            var responseModel = new ResponseModelDto();
            try
            {
                /*responseModel.ResponseCode = "-1";

                var existingcommonSharDtls = await _repositoryManager.comissionRepository.GetCommReceivablesStatusAsync(entity.CRID, cancellationToken);

                if (existingcommonSharDtls != null)
                {
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ComissionManagementError.ComissionAlreadyExist,
                    Error = Resource.ComissionIsExit } };
                }
                else
                {*/
                    var commEntity = entity.Adapt<ComissionReciveableStatus>();
                    await _repositoryManager.comissionRepository.UpdateCommReceivablesStatusAsync(commEntity, cancellationToken);

                    responseModel.ResponseCode = ResponseCode.Success;
                    responseModel.Response = nameof(ResponseCode.Success);

               // }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update CommReceivables Status");
                _logger.LogException(ex);
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = CommonErrorCode.ContactToAdmin,
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditReceivablesDtlsAsync(UpdateCommReceivablesDtlsDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            
            try
            {
                if(entity.statusId == 2)
                {
                    var commrcvvalue = await _repositoryManager.comissionRepository.CompairCommReceivablesDtls(entity.commReceivableId, entity.commReceivableDtlsId, cancellationToken);
                    if(commrcvvalue.statusId == 1)
                    {
                        var commEntity = entity.Adapt<UpdateCommReceivablesDtls>();
                        await _repositoryManager.comissionRepository.UpdateCommReceivablesDtlsAsync(commEntity, cancellationToken);
                        responseModel.ResponseCode = "0";
                        responseModel.Response = "Success";
                    }
                    else
                    {
                        var resultcommrcvvalue = await _repositoryManager.comissionRepository.GetCompairCommReceivablesDtlsValue(entity.commReceivableId, commrcvvalue.minimumValue, commrcvvalue.maximumValue, cancellationToken);
                        if (resultcommrcvvalue.Count() > 1)
                        {
                            responseModel.Response = "Error";
                            responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ComissionManagementError.ComissionAlreadyExist,
                    Error = Resource.ComissionIsExit } };
                        }
                        else
                        {
                            var commEntity = entity.Adapt<UpdateCommReceivablesDtls>();
                            await _repositoryManager.comissionRepository.UpdateCommReceivablesDtlsAsync(commEntity, cancellationToken);
                            responseModel.ResponseCode = "0";
                            responseModel.Response = "Success";

                        }
                    }                    
                }
                else
                {
                    var commEntity = entity.Adapt<UpdateCommReceivablesDtls>();
                    await _repositoryManager.comissionRepository.UpdateCommReceivablesDtlsAsync(commEntity, cancellationToken);
                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";

                }

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update CommReceivables Dtls");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditCommSharingModelAsync(UpdateCommSharingModelDto entity, CancellationToken cancellationToken)
        {
            var responseModel = new ResponseModelDto();
            try
            {
                responseModel.ResponseCode = "-1";

                var existingcommonSharDtls = await _repositoryManager.comissionRepository.CheckCommisionShairingAsync(entity.CsmId, cancellationToken);

                if (existingcommonSharDtls != null)
                {
                    //responseModel.Response = "Commision is alredy have its details you can not inactive";
                    //responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    //ErrorCode  = ((int)ErrorCodeEnum.ComissionManagementErrorEnum.ComissionAlreadyExist).ToString(),
                    //Error = Resource.ComissionIsExit } };

                    var commEntity = entity.Adapt<UpdateCommSharingModel>();
                    await _repositoryManager.comissionRepository.UpdateCommSharingModel(commEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";
                }
                else
                {
                    var commEntity = entity.Adapt<UpdateCommSharingModel>();
                    await _repositoryManager.comissionRepository.UpdateCommSharingModel(commEntity, cancellationToken);

                    responseModel.ResponseCode = "0";
                    responseModel.Response = "Success";

                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update CommSharing Model");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditCommSharingModelDtlsAsync(UpdateCommSharingModelDtlsDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var commEntity = entity.Adapt<UpdateCommSharingModelDtls>();
                await _repositoryManager.comissionRepository.UpdateCommSharingModelDtls(commEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update CommSharing Model Dtls");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        Task<ResponseModelDto> IGenericService<ComissionDto>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<ComissionDto>.GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetDynamicSearchComissionReceiveable(DynamicSearchComissionReceiveableDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var requestparam = entity.Adapt<DynamicSearchComissionReceiveable>();
                var _getuserResponse = await _repositoryManager.comissionRepository.GetDynamicSearchComissionReceiveable(requestparam, cancellationToken);
                if (_getuserResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString();
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                var commDtos = _getuserResponse.Adapt<IEnumerable<DynamicSearchComissionReceiveableModelDto>>();
                DynamicSearchComissionReceiveableModelResponse modelResponse = new DynamicSearchComissionReceiveableModelResponse()
                {
                    CommDetails = commDtos
                };
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = modelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the user");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return await Task.FromResult<ResponseModelDto>(responseModel);
        }

        public async Task<ResponseModelDto> GetDynamicSearchSharingModels(DynamicSearchComissionReceiveableDto entity, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            responseModel.ResponseCode = "-1";
            try
            {
                var requestparam = entity.Adapt<DynamicSearchComissionReceiveable>();
                var _getuserResponse = await _repositoryManager.comissionRepository.GetDynamicSearchSharingModels(requestparam, cancellationToken);
                if (_getuserResponse == null)
                {
                    responseModel.ResponseCode = ((int)ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString();
                    responseModel.Response = Resource.ContactToAdmin;
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.UserManagementErrorEnum.UserNotFound).ToString(),
                    Error = Resource.ContactToAdmin } };
                    return responseModel;
                }
                var commDtos = _getuserResponse.Adapt<IEnumerable<DynamicSearchSharingModelsDto>>();
                DynamicSearchSharingModelsResponse modelResponse = new DynamicSearchSharingModelsResponse()
                {
                    CommDetails = commDtos
                };
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";
                responseModel.Data = modelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while fetching the user");
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString();
                responseModel.Response = Resource.ContactToAdmin;
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return await Task.FromResult<ResponseModelDto>(responseModel);
        }
    }
}
