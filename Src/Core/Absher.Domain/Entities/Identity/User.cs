using Absher.Domain.Abstracts;
using Absher.Domain.Entities.Chat;
using Absher.Domain.Entities.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Entities.Identity
{
    public class User : UpdateSoftDeleteEntity<Guid>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public Guid RegionId { get; set; }
        public Guid SectorId { get; set; }
        public Guid PositionId { get; set; }
        public UserToken UserToken { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserChatGroup> UserChatGroups { get; set; }
        public ICollection<ChatInvitation> SentChatInvitations { get; set; }
        public ICollection<ChatMessage> SentChatMessages { get; set; }
        public ICollection<ChatInvitation> ReceivedChatInvitations { get; set; }
    }
}
