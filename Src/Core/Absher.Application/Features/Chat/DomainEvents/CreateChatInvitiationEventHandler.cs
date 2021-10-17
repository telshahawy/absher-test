using Absher.Domain.Entities.Chat;
using Absher.Domain.Entities.Common.Enum;
using Absher.Interfaces.Repositories;
using Absher.Interfaces.UserResolverHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.Chat.DomainEvents
{
    public class CreateChatInvitiationEventHandler : INotificationHandler<OneToOneGroupChatCreatedEvent>
    {
        private readonly IWriteRepository<ChatInvitation> _chatInvitationWriteRepository;
        private readonly IUserResolverHandler _userResolverHandler;

        public CreateChatInvitiationEventHandler(IWriteRepository<ChatInvitation> chatInvitationWriteRepository, IUserResolverHandler userResolverHandler)
        {
            _chatInvitationWriteRepository = chatInvitationWriteRepository;
            _userResolverHandler = userResolverHandler;
        }
        public async Task Handle(OneToOneGroupChatCreatedEvent notification, CancellationToken cancellationToken)
        {
            var chatInvitation = new ChatInvitation()
            {
                ChatGroupId = notification.ChatGroup.Id,
                SenderId = Guid.Parse(_userResolverHandler.GetUserId()),
                ReceiverId = notification.InvitedUserId,
                ChatInvitationStatus = ChatInvitationStatus.Pending
            };
            await _chatInvitationWriteRepository.AddAsync(chatInvitation);
        }
    }
}
