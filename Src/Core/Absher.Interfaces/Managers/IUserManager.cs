using Absher.Interfaces.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Managers
{
    public interface IUserManager
    {
        UserDto GetById(Guid id);
        Task<UserDto> GetByUserNameAndPassword(string userName, string password);
    }
}
