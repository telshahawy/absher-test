﻿using Absher.Interfaces.Hubs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Hubs
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(ChatMessageDto message);
    }
}
