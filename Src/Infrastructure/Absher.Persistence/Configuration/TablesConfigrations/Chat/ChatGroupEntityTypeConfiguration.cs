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
    public class ChatGroupEntityTypeConfiguration : IEntityTypeConfiguration<ChatGroup>
    {
        public void Configure(EntityTypeBuilder<ChatGroup> builder)
        {
            builder.ToTable(nameof(ChatGroup), TablesSchema.ChatSchema);
            builder.HasKey(x => x.Id);
        }
    }
}
