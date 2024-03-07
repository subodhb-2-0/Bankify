using Contracts;
using Contracts.Common;
using Contracts.Constants;
using Contracts.Error;
using Contracts.PageManagement;
using Contracts.Response;
using Contracts.Role;
using Domain.Entities.PageManagement;
using Domain.Entities.RoleManagement;
using Domain.RepositoryInterfaces;
using Logger;
using Mapster;
using Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class RoleService : IRoleService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ICustomLogger _logger;

        public RoleService(IRepositoryManager repositoryManager, ICustomLogger logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        async Task<ResponseModelDto> IGenericService<RoleDto>.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var role = await _repositoryManager.roleRepository.GetByIdAsync(id, cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var roleDto = role.Adapt<RoleDto>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleDto;
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

        async Task<ResponseModelDto> IGenericService<RoleDto>.GetAllAsync(CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var roles = await _repositoryManager.roleRepository.GetAllAsync(cancellationToken);  //await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);

                var userDtos = roles.Adapt<IEnumerable<RoleDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = userDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInfo("Exception while accessing GetAllAsync");
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

        async Task<ResponseModelDto> IGenericService<RoleDto>.AddAsync(RoleDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();

            //code temp commented by swapnal for checking duplicate id


            var role = await _repositoryManager.roleRepository.GetByNameAsync(entity.RoleName, cancellationToken);
            if (role is not null && role.Id > 0)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = RoleErrorCode.RoleAlreadyExists, Error = Resource.RoleAlreadyExists } };

            }
            else
            {
                responseModel = Validate(entity);
                if (responseModel.Response == "")
                {
                    var _roleModel = entity.Adapt<Role>();
                    var result = await _repositoryManager.roleRepository.AddAsync(_roleModel, cancellationToken);
                    entity.RoleId = result.RoleId;
                }
            }



            return responseModel;

        }

        public async Task<ResponseModelDto> CreateRoleAndPageAccess(RolePageDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();

            //code temp commented by swapnal for checking duplicate id
            try
            {

                var role = await _repositoryManager.roleRepository.GetByNameAsync(entity.RoleName.Trim(), cancellationToken);
                if (role is not null && role.RoleId > 0)
                {
                    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                    responseModel.Response = "Error";
                    responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = RoleErrorCode.RoleAlreadyExists, Error = Resource.RoleAlreadyExists } };

                }
                else
                {
                    var _roleModel = entity.Adapt<Role>();
                    var _rolePageAccess = entity.RolePages.Adapt<List<RolePage>>();
                    await _repositoryManager.roleRepository.CreateAndAssignPageAccess(_roleModel, _rolePageAccess, cancellationToken);
                    role = await _repositoryManager.roleRepository.GetByNameAsync(entity.RoleName.Trim(), cancellationToken);
                    entity.RoleId = role.RoleId;
                    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                    responseModel.Response = "Success";
                    responseModel.Data = entity.Adapt<RoleDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo("Exception while accessing CreateRoleAndPageAccess");
                _logger.LogObject(entity);
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>()
                                            { new ErrorModelDto()
                                                {   ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                    Error = Resource.ContactToAdmin
                                                }
                                            };
            }


            return responseModel;

        }

        Task<ResponseModelDto> IGenericService<RoleDto>.UpdateAsync(RoleDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModelDto> IGenericService<RoleDto>.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModelDto> GetPageAccess(int roleId, int pageSourceId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var roleAccessList = await _repositoryManager.roleRepository.GetPageAccess(roleId, pageSourceId, cancellationToken);

                var roleAccessDtos = roleAccessList.Adapt<IEnumerable<RoleAccessDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }
        public async Task<ResponseModelDto> EditAndAssignRole(AssignRoleDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var roleEntity = entity.Adapt<AssignRole>();
                await _repositoryManager.roleRepository.AssignRoleAsync(roleEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update Role Model");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> GetRolePages(int roleId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var rolePages = await _repositoryManager.roleRepository.GetRolePages(roleId, cancellationToken);

                var roleAccessDtos = rolePages.Adapt<IEnumerable<RoleAccessDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetAllPages(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {

                var rolePages = await _repositoryManager.roleRepository.GetAllPagesAsync(cancellationToken);

                var roleAccessDtos = rolePages.Adapt<IEnumerable<RoleAccessDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public ResponseModelDto Validate(RoleDto entity)
        {
            ResponseModelDto responseModel;
            ErrorModelDto errorModel;

            var context = new ValidationContext(entity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                responseModel = new ResponseModelDto();
                foreach (var result in results)
                {
                    errorModel = new ErrorModelDto();
                    var _error = result.ToString().Split('|');

                    /*responseEntity.ResponseCode = _error[0];
                    responseEntity.Response = _error[1];
                    responseModel.Responses.Add(responseEntity);*/
                }
                return responseModel;
            }
            return responseModel = null;
        }

        public async Task<ResponseModelDto> UpdateRoleAndPageAccess(EditRolePageDto entity, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            { 
                    var _roleModel = entity.Adapt<RolepageeditByRoleId>();
                    var _rolePageAccess = entity.RolePages.Adapt<List<RolePageEdit>>();
                    await _repositoryManager.roleRepository.EditRoleAndPageAccess(_roleModel, _rolePageAccess, cancellationToken);

                    responseModel.ResponseCode = ResponseCode.Success;
                    responseModel.Response = nameof(ResponseCode.Success);
                    responseModel.Data = entity.Adapt<RoleDto>();
                
            }
            catch (Exception ex)
            {
                _logger.LogInfo("Exception while accessing CreateRoleAndPageAccess");
                _logger.LogObject(entity);
                _logger.LogException(ex);
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>()
                                            { new ErrorModelDto()
                                                {   ErrorCode = CommonErrorCode.ContactToAdmin,
                                                    Error = Resource.ContactToAdmin
                                                }
                                            };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> ViewRolePages(int roleid, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var rolePages = await _repositoryManager.roleRepository.ViewRolePages(roleid, cancellationToken);
                var roleAccessDtos = rolePages.Adapt<IEnumerable<ViewRolePages>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }
        public async Task<ResponseModelDto> EditRoleStatus(ActiveDeactiveRoleDto entity, CancellationToken cancellationToken)
        {

            var responseModel = new ResponseModelDto();
            try
            {
                var roleEntity = entity.Adapt<ActiveDeactiveRole>();
                await _repositoryManager.roleRepository.ActiveDeactiveRoleAsync(roleEntity, cancellationToken);
                responseModel.ResponseCode = "0";
                responseModel.Response = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error while Update Role Model");
                _logger.LogException(ex);
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                    ErrorCode  = ((int) ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                    Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetListofRole(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var rolePages = await _repositoryManager.roleRepository.GetListofRole(cancellationToken);
                var roleAccessDtos = rolePages.Adapt<IEnumerable<ListofRole>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetRoleByRoleId(int roleId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var rolePages = await _repositoryManager.roleRepository.GetRoleByRoleId(roleId, cancellationToken);
                var roleAccessDtos = rolePages.Adapt<IEnumerable<RoleAccessDto>>();
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ResponseCode.Error;
                responseModel.Response = nameof(ResponseCode.Error);
                responseModel.Errors = new List<ErrorModelDto>() {
                                       new ErrorModelDto() 
                                       { 
                                           ErrorCode = CommonErrorCode.ContactToAdmin,
                                           Error = Resource.ContactToAdmin,
                                       } };
            }
            return responseModel;
        }

        public async Task<ResponseModelDto> GetPageDetailsByRoleId(int roleId, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var rolePages = await _repositoryManager.roleRepository.GetPageDetailsByRoleId(roleId, cancellationToken);
                var roleAccessDtos = rolePages.Adapt<IEnumerable<PageDetailsByRoleDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = roleAccessDtos;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>() { new ErrorModelDto() { ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(), Error = Resource.ContactToAdmin } };
            }

            return responseModel;
        }

        public async Task<ResponseModelDto> GetAllPagesAsync(int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, string? searchBy, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allUsers = await _repositoryManager.roleRepository.GetAllPagesAsync(pageSize, pageNumber, orderByColumn, orderBy, searchBy, cancellationToken);
                var userDtos = allUsers.Item1.Adapt<IEnumerable<RoleDto>>();
                RolePageResponse userDetailResponse = new RolePageResponse() 
                {
                    roleDtos = userDtos, TotalRecord = allUsers.Item2, PageNumber = pageNumber,
                    PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn
                };
                responseModel.ResponseCode = ResponseCode.Success;
                responseModel.Response = nameof(ResponseCode.Success);
                responseModel.Data = userDetailResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogInfo($"Error while Get List of WCRequest");
                //_logger.LogException(ex);
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

        public async Task<ResponseModelDto> GetAllPageDetails(int? roleId, string? searchValue, int? pageSize, int? pageNumber, string? orderByColumn, string? orderBy, CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allPages = await _repositoryManager.roleRepository.GetAllPageDetails(roleId,searchValue, pageSize, pageNumber, orderByColumn, orderBy, cancellationToken);
                var pageDtos = allPages.Item1.Adapt<IEnumerable<GetPageDetailDto>>();
                GetPageDetailResponse pageDetailResponse = new GetPageDetailResponse() { pageDetailsDtos = pageDtos, SerachValue = searchValue, TotalRecord = allPages.Item2, PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, orderByColumn = orderByColumn, RoleId = roleId };
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = pageDetailResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogInfo($"Error while Get List of WCRequest");
                //_logger.LogException(ex);
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

        public async Task<ResponseModelDto> CreatePageDetails(AddPageDetailDto addPageDetailDto, CancellationToken cancellationToken)
        {
            ResponseModelDto responseModel = new ResponseModelDto();

            try
            {
                var entity = addPageDetailDto.Adapt<AddPageDetail>();

                bool isDuplicate = await _repositoryManager.roleRepository.CheckDuplicatePage(entity);
                if (isDuplicate)
                {
                    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                    responseModel.Response = $"Duplicate data found for page name: {entity.PageName} or page path: {entity.PagePath}";
                }
                else
                {
                    var pageDetail = await _repositoryManager.roleRepository.CreatePageDetails(entity, cancellationToken);
                    responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                    responseModel.Response = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo("Exception while adding PageDetails");
                _logger.LogObject(addPageDetailDto);
                _logger.LogException(ex);
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Error).ToString();
                responseModel.Response = "Error";
                responseModel.Errors = new List<ErrorModelDto>()
                                            { new ErrorModelDto()
                                                {   ErrorCode = ((int)ErrorCodeEnum.CommonErrorEnum.ContactToAdmin).ToString(),
                                                    Error = Resource.ContactToAdmin
                                                }
                                            };
            }


            return responseModel;
        }

        public async Task<ResponseModelDto> GetAllParentPage(CancellationToken cancellationToken = default)
        {
            ResponseModelDto responseModel = new ResponseModelDto();
            try
            {
                var allParentPages = await _repositoryManager.roleRepository.GetAllParentPage(cancellationToken);
                var pageParentDtos = allParentPages.Adapt<IEnumerable<GetParentPageDetailDto>>();
                responseModel.ResponseCode = ((int)ResponseCodeEnum.CommonResponseEnum.Success).ToString();
                responseModel.Response = "Success";
                responseModel.Data = pageParentDtos;
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
    }
}