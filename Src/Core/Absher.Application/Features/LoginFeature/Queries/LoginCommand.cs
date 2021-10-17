using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.Auth;

namespace Absher.Application.Features.LoginFeature.Queries
{
    public class LoginCommand : QueryBase<ResponseResult<LoginResponseDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
