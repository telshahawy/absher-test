using Absher.Domain.Entities.Chat;
using Absher.Domain.Entities.TablesSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Persistence.Configuration.TablesConfigrations.Chat
{
    public class ChatMessageEntityTypeConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable(nameof(ChatMessage), TablesSchema.ChatSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Sender).WithMany(x => x.SentChatMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ChatGroup).WithMany(x => x.ChatMessages).HasForeignKey(x => x.ChatGroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
