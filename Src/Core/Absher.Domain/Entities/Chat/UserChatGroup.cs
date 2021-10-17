using Absher.Domain.Abstracts;
using Absher.Domain.Entities.Common.Enum;
using Absher.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Entities.Chat
{
    public class UserChatGroup : UpdateSoftDeleteEntity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ChatGroupId { get; set; }
        public ChatGroup ChatGroup { get; set; }
        public ChatRole ChatRole { get; set; }
    }
}
