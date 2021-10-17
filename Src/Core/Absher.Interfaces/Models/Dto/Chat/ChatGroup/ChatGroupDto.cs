using Absher.Interfaces.Models.Dto.User.UserChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Models.Dto.Chat.ChatRoom
{
    public class ChatGroupDto
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public List<UserChatDto> Users { get; set; }
    }
}
