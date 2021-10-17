using Absher.Domain.Entities.Chat;
using Absher.Interfaces.Managers;
using Absher.Interfaces.Models.Dto.Chat.ChatRoom;
using Absher.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Managers
{
    public class ChatManager : IChatManager
    {
        private readonly IReadRepository<ChatGroup> _chatGroupReadRepo;
        private readonly IReadRepository<UserChatGroup> _userChatGroupReadRepo;
        private readonly IWriteRepository<ChatGroup> _chatGroupWriteRepo;
        public ChatManager(IReadRepository<ChatGroup> chatGroupReadRepository, IWriteRepository<ChatGroup> chatGroupWriteRepository, IReadRepository<UserChatGroup> userChatGroupReadRepo)
        {
            _chatGroupReadRepo = chatGroupReadRepository;
            _chatGroupWriteRepo = chatGroupWriteRepository;
            _userChatGroupReadRepo = userChatGroupReadRepo;
        }
        public async Task<List<Guid>> GetUserGroupIds(Guid userId)
        {
            var groups = await _userChatGroupReadRepo.GetManyAsync(x => x.UserId == userId);

            return groups.Select(x=>x.ChatGroupId).ToList();
        }
    }
}
