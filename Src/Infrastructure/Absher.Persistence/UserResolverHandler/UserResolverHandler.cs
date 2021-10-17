using Absher.Interfaces.Models.Dto.User;
using Absher.Interfaces.UserResolverHandler;
using Absher.Utility.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Persistence.UserResolverHandler
{
    public class UserResolverHandler : IUserResolverHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User?.GetLoggedInUserName();// .Identity?.Name;
        }
        public string GetUserFullName()
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDto;
            return user.FullName;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.GetLoggedInUserId<string>();
            //return _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
