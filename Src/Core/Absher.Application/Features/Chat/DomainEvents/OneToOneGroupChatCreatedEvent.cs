using Absher.Domain.Entities.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Features.Chat.DomainEvents
{
    public class OneToOneGroupChatCreatedEvent : INotification
    {
        public ChatGroup ChatGroup { get; set; }
        public Guid InvitedUserId { get; set; }
        public OneToOneGroupChatCreatedEvent(ChatGroup chat, Guid invitedUserId)
        {
            ChatGroup = chat;
            InvitedUserId = invitedUserId;
        }
    }
}
