using Absher.Interfaces.Models.Dto.Chat.ChatRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Managers
{
    public interface IChatManager
    {
        Task<List<Guid>> GetUserGroupIds(Guid userId);
    }
}
