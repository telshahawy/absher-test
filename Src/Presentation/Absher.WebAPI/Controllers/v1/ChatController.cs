using Absher.Application.Features.Chat.Commands.InviteOneToChat;
using Absher.Domain.ResponseModel;
using Absher.Resource;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absher.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ChatController : ApiControllerBase
    {
        private readonly IStringLocalizer<Message_Resource> _localizer;
        public ChatController(IMediator mediator, IStringLocalizer<Message_Resource> localizer) : base(mediator)
        {
            _localizer = localizer;
        }

        [HttpPost]
        [Route("InviteOneToChat")]
        public async Task<ActionResult<ResponseResult<bool>>> InviteOneToChat(InviteOneToChatCommand command)
        {
            return Single(await CommandAsync(command));
        }
    }
}
