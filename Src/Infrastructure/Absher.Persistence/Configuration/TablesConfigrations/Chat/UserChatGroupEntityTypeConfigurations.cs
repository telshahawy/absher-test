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
    public class UserChatGroupEntityTypeConfigurations : IEntityTypeConfiguration<UserChatGroup>
    {
        public void Configure(EntityTypeBuilder<UserChatGroup> builder)
        {

            builder.ToTable(nameof(UserChatGroup), TablesSchema.ChatSchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.UserChatGroups).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ChatGroup).WithMany(x => x.UserChatGroups).HasForeignKey(x => x.ChatGroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
