using Absher.Application.Features.Chat.Commands.InviteOneToChat;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Features.Chat.Validators
{
    public class InviteOneToChatCommandValidator : AbstractValidator<InviteOneToChatCommand>
    {
        public InviteOneToChatCommandValidator()
        {
            RuleFor(x=>x.InvitedUserId).NotEmpty();
        }
    }
}
