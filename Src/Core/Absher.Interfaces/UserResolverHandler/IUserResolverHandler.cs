using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.UserResolverHandler
{
    public interface IUserResolverHandler
    {
        string GetUserId();
        string GetUserName();
        string GetUserFullName();
    }
}
