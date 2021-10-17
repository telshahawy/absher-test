
using Absher.Application.Features.EditProfile.Queries;
using Absher.Application.Features.GetProfileFeature.Queries;
using Absher.Application.Features.LoginFeature.Queries;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.Auth;
using Absher.Interfaces.Models.Dto.User.UserProfile;
using Absher.Resource;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Absher.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly IStringLocalizer<Message_Resource> _localizer;
        public AccountController(IMediator mediator, IStringLocalizer<Message_Resource> localizer) : base(mediator)
        {
            _localizer = localizer;
        }

        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult<ResponseResult<LoginResponseDto>>> Login(LoginCommand loginCommand)
        {
            return Single(await QueryAsync(loginCommand));
        }
        [HttpPost]
        [Route("EditProfilePhoto")]

        public async Task<ActionResult<ResponseResult<bool>>> EditProfilePhoto(EditProfileCommand editProfileCommand)
        {
            return Single(await CommandAsync(editProfileCommand));
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<UserProfileDto>>>> GetUserProfile(GetProfileCommand getProfileCommand)
        {
            return Single(await QueryAsync(getProfileCommand));
        }
        [HttpGet]
        [Route("TestMethod")]
        public ActionResult TestMethod()
        {
            var data = _localizer[MessageResourceKeys.NotFound];
            return Ok(data);
        }
    }
}
