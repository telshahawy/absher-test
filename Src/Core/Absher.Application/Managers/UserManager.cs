using Absher.Domain.Entities.Identity;
using Absher.Interfaces.Managers;
using Absher.Interfaces.Models.Dto.User;
using Absher.Interfaces.Models.Dto.User.UserAttachment;
using Absher.Interfaces.Repositories;
using Absher.Resource;
using Absher.Utility.Exceptions;
using Absher.Utility.HelperOperation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IReadRepository<User> _userReadRepository;
        private readonly IWriteRepository<User> _userWriteRepository;
        private readonly IStringLocalizer<Message_Resource> _localizer;

        public UserManager(IReadRepository<User> userReadRepository, IWriteRepository<User> userWriteRepository, IStringLocalizer<Message_Resource> localizer)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _localizer = localizer;
        }
        public UserDto GetById(Guid id)
        {
            if (id == Guid.Parse("db267b38-2f9e-4c24-903b-782da7d7ee3f"))
            {
                return new UserDto()
                {
                    UserId = id,
                    Email = "admin1@absher.test.com",
                    FullName = "Absher Admin",
                    UserName = "Absher_Admin",
                    Picture = new UserAttachmentDto()
                    {
                        FileName = "db267b38-db267b38-db267b38-db267b38",
                        FilePath = "/path/directory/folder",
                        Extention = ".png"
                    }
                };
            }
            return new UserDto()
            {
                UserId = id,
                Email = "admin2@absher.test.com",
                FullName = "Absher Admin2",
                UserName = "Absher_Admin2",
                Picture = new UserAttachmentDto()
                {
                    FileName = "db267b38-db267b38-db267b38-db267b38",
                    FilePath = "/path/directory/folder",
                    Extention = ".png"
                }
            };
        }

        public async Task<UserDto> GetByUserNameAndPassword(string userName, string password)
        {
            UserDto result = new UserDto();

            var user = await _userReadRepository.GetAsync(x => x.UserName == userName.Trim());
            if (user == null)
                throw new NotFoundException(_localizer[MessageResourceKeys.InvalidUserName]);
            var passwordHashed = HashHelper.sha256(password.Trim());
            if (passwordHashed != user.Password)
                throw new Exception(_localizer[MessageResourceKeys.InvalidPassword]);
            
            // TODO: update picture object
            result = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.Name,
                UserName = user.UserName,
                Picture = new UserAttachmentDto()
                {
                    FileName = "db267b38-db267b38-db267b38-db267b38",
                    FilePath = "/path/directory/folder",
                    Extention = ".png"
                }
            };
            return result;
        }
    }
}
