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
    public class ChatInvitation : UpdateSoftDeleteEntity<Guid>
    {
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
        public Guid ChatGroupId { get; set; }
        public ChatGroup ChatGroup { get; set; }
        public ChatInvitationStatus ChatInvitationStatus { get; set; }
    }
}
