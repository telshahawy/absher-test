using Absher.Domain.Abstracts;
using Absher.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Entities.Chat
{
    public class ChatMessage : UpdateSoftDeleteEntity<Guid>
    {
        public string Message { get; set; }
        public string MessageFile { get; set; }
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ChatGroupId { get; set; }
        public ChatGroup ChatGroup { get; set; }
        public bool IsDeletedBySuperAdmin { get; set; }
    }
}
