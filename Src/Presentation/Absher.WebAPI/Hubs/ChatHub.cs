using Absher.Interfaces.Hubs;
using Absher.Interfaces.Hubs.Dto;
using Absher.Interfaces.Managers;
using Absher.Interfaces.UserResolverHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absher.WebAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHubClient>
    {
        private readonly IUserResolverHandler _userResolverHandler;
        private readonly ITokenManager _tokenManger;
        private readonly IChatManager _chatManger;
        public ChatHub(IUserResolverHandler userResolverHandler, ITokenManager tokenManger, IChatManager chatManger)
        {
            _userResolverHandler = userResolverHandler;
            _tokenManger = tokenManger;
            _chatManger = chatManger;
        }
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = Guid.Parse(_userResolverHandler.GetUserId());

            await _tokenManger.UpdateConnectionId(userId, connectionId);
            var groupIds = await _chatManger.GetUserGroupIds(userId);

            groupIds.Select(x => Groups.AddToGroupAsync(connectionId, x.ToString()));
            await base.OnConnectedAsync();
        }


        public async Task SendMessage(ChatMessageDto message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}
