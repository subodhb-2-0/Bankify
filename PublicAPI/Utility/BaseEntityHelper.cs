using Contracts.Common;
using System.Security.Claims;

namespace PublicAPI.Utility
{
    public class BaseEntityHelper
    {
        private readonly IPAddressHelper _ipAddressHelper;
        public BaseEntityHelper()
        {
            _ipAddressHelper = new IPAddressHelper();
        }
        public BaseEntityDto GetBaseEntity(HttpContext httpContext)
        {

            BaseEntityDto baseEntityDto = null;
            if(httpContext == null)
            {
                return baseEntityDto;
            }
            baseEntityDto = new BaseEntityDto();
            baseEntityDto.CreatedBy = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.Sid).Value);
            baseEntityDto.CreatedOn = DateTime.Now;
            baseEntityDto.UpdatedBy= Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.Sid).Value);
            baseEntityDto.UpdatedOn= DateTime.Now;
            baseEntityDto.IPAddress = _ipAddressHelper.GetClientIpAddress(httpContext);
            return baseEntityDto;

        }
    }
}
