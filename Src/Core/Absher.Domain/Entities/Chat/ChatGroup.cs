using Absher.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Entities.Chat
{
    public class ChatGroup : UpdateSoftDeleteEntity<Guid>
    {
        public string GroupName { get; set; }
        public ICollection<UserChatGroup> UserChatGroups { get; set; }
        public ICollection<ChatInvitation> ChatInvitations { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }


    }
}
