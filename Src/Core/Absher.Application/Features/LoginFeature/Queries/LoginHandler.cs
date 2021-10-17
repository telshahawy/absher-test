using Absher.Domain.Entities.Identity;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Managers;
using Absher.Interfaces.Models.Dto.Auth;
using Absher.Interfaces.Repositories;
using Absher.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.LoginFeature.Queries
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResponseResult<LoginResponseDto>>
    {
        private readonly IUserManager _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly IReadRepository<User> _userReadRepository;
        private readonly IWriteRepository<User> _userWriteRepository;
        private readonly IStringLocalizer<Message_Resource> _localizer;

        public LoginHandler(IUserManager userManager, ITokenManager tokenManager, IWriteRepository<User> userWriteRepository, IReadRepository<User> userReadRepository, IStringLocalizer<Message_Resource> localizer)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _localizer = localizer;
        }

        public async Task<ResponseResult<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ResponseResult<LoginResponseDto> responseResult = new ResponseResult<LoginResponseDto>();

            var user = await _userManager.GetByUserNameAndPassword(request.UserName, request.Password);
            var token = _tokenManager.GenerateToken(user);
            await _tokenManager.CreateOrUpdateToken(user.UserId, token);

            responseResult.Entity = new LoginResponseDto()
            {
                UserId = user.UserId,
                FullName = user.FullName,
                UserName = user.UserName,
                ProfileImage = user.Picture,
                Email = user.Email,
                Token = token,
            };
            responseResult.IsSuccess = true;
            responseResult.Status = System.Net.HttpStatusCode.OK;

            return responseResult;
        }
    }
}
