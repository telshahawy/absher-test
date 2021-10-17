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
    public class ChatInvitationEntityTypeConfiguration : IEntityTypeConfiguration<ChatInvitation>
    {
        public void Configure(EntityTypeBuilder<ChatInvitation> builder)
        {
            builder.ToTable(nameof(ChatInvitation), TablesSchema.ChatSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Sender).WithMany(x => x.SentChatInvitations).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Receiver).WithMany(x => x.ReceivedChatInvitations).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ChatGroup).WithMany(x => x.ChatInvitations).HasForeignKey(x => x.ChatGroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
