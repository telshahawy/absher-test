using Absher.Application.Common;
using Absher.Application.Features.Chat.DomainEvents;
using Absher.Domain.Entities.Chat;
using Absher.Domain.Entities.Common.Enum;
using Absher.Domain.Entities.Identity;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Repositories;
using Absher.Interfaces.UserResolverHandler;
using Absher.Resource;
using Absher.Utility.Exceptions;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.Chat.Commands.InviteOneToChat
{
    public class InviteOneToChatCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid InvitedUserId { get; set; }

        private class Handler : IRequestHandler<InviteOneToChatCommand, ResponseResult<bool>>
        {
            private readonly IStringLocalizer<Message_Resource> _localizer;
            private readonly IReadRepository<User> _userReadRepository;
            private readonly IWriteRepository<ChatGroup> _chatGroupWriteRepository;
            private readonly IReadRepository<ChatInvitation> _chatInvitationReadRepository;
            private readonly IUserResolverHandler _userResolverHandler;
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            public Handler(IReadRepository<User> userReadRepository, IStringLocalizer<Message_Resource> localizer, IUserResolverHandler userResolverHandler, IMediator mediator, IUnitOfWork unitOfWork, IWriteRepository<ChatGroup> chatGroupWriteRepository, IReadRepository<ChatInvitation> chatInvitationReadRepository)
            {
                _userReadRepository = userReadRepository;
                _localizer = localizer;
                _userResolverHandler = userResolverHandler;
                _mediator = mediator;
                _unitOfWork = unitOfWork;
                _chatGroupWriteRepository = chatGroupWriteRepository;
                _chatInvitationReadRepository = chatInvitationReadRepository;
            }

            public async Task<ResponseResult<bool>> Handle(InviteOneToChatCommand request, CancellationToken cancellationToken)
            {
                var invited = await _userReadRepository.GetAsync(x => x.Id == request.InvitedUserId);
                if (invited == null)
                    throw new NotFoundException(_localizer[MessageResourceKeys.UserNotFound]);

                var alreadySent = await _chatInvitationReadRepository.GetAnyAsync(x => x.SenderId == Guid.Parse(_userResolverHandler.GetUserId()) &&
                                                                                     x.ReceiverId == request.InvitedUserId &&
                                                                                     x.ChatInvitationStatus == ChatInvitationStatus.Pending);
                if(alreadySent)
                    throw new BusinessException(_localizer[MessageResourceKeys.ChatInvitationAlreadySent]);

                var chatGroup = new ChatGroup()
                {
                    GroupName = string.Format(_localizer[MessageResourceKeys.ChatGroupName], _userResolverHandler.GetUserFullName(), invited.Name),
                };
                chatGroup.AddDomainEvent(new OneToOneGroupChatCreatedEvent(chatGroup, request.InvitedUserId));

                await _chatGroupWriteRepository.AddAsync(chatGroup);

                await _unitOfWork.CommitAsync();

                return new ResponseResult<bool>()
                {
                    Entity = true,
                    IsSuccess = true,
                    Status = HttpStatusCode.OK,
                    Message = _localizer[MessageResourceKeys.DefaultSave]
                };
            }
        }
    }
}
