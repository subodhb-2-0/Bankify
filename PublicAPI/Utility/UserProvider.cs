using Services.Abstractions;
using System.Security.Claims;

namespace PublicAPI.Utility
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserProvider(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserId()
        {
            return _context.HttpContext.User.Claims
                       .First(i => i.Type == ClaimTypes.Sid).Value;
        }
        public string GetUserName()
        {
            return _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}
