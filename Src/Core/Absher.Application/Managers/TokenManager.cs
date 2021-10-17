using Absher.Domain.Entities.Identity;
using Absher.Interfaces.Managers;
using Absher.Interfaces.Models.Dto.User;
using Absher.Interfaces.Repositories;
using Absher.Resource;
using Absher.Utility.CommonModels;
using Absher.Utility.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Absher.Utility.Extensions;

namespace Absher.Application.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly AppJWTSetting _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadRepository<UserToken> _tokenReadRepository;
        private readonly IWriteRepository<UserToken> _tokenWriteRepository;
        private readonly IStringLocalizer<Message_Resource> _localizer;
        public TokenManager(IOptions<AppJWTSetting> appSettings, IUnitOfWork unitOfWork, IReadRepository<UserToken> readRepository, IStringLocalizer<Message_Resource> localizer, IWriteRepository<UserToken> tokenWriteRepository)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _tokenReadRepository = readRepository;
            _localizer = localizer;
            _tokenWriteRepository = tokenWriteRepository;
        }
        public string GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Sid,user.UserId.ToString()),
                        new Claim(ClaimTypes.Role,"Admin"),
                        new Claim(ClaimTypes.GivenName,user.FullName),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim("profile_image",JsonConvert.SerializeObject( user.Picture)),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<string> GetConnectionId(Guid userId)
        {
            var token = await _tokenReadRepository.GetSingleAsync(e => e.UserId == userId);

            if (token == null)
                throw new NotFoundException(_localizer[MessageResourceKeys.UserNotFound]);

            return token.ConnectionId;
        }

        public async Task<bool> UpdateConnectionId(Guid userId, string ConnectionId)
        {
            var token = _tokenReadRepository.GetSingle(e => e.UserId == userId);

            if (token == null)
                throw new NotFoundException(_localizer[MessageResourceKeys.UserNotFound]);

            token.ConnectionId = ConnectionId;

            _tokenWriteRepository.Update(token);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> CreateOrUpdateToken(Guid userId, string jwtToken)
        {
            try
            {
                var token = _tokenReadRepository.GetSingle(e => e.UserId == userId);

                if (token == null)
                {
                    // Add 
                    token = new UserToken()
                    {
                        UserId = userId,
                        Token = jwtToken,
                        LastLoginDate = DateTime.Now.GetCurrentDateTime(),
                    };
                    await _tokenWriteRepository.AddAsync(token);
                }
                else
                {
                    // edit 
                    token.Token = jwtToken;
                    _tokenWriteRepository.Update(token);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
