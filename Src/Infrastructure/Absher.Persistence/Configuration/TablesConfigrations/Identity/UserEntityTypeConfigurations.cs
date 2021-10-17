using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Absher.Domain.Entities.Identity;
using Absher.Domain.Entities.TablesSchema;

namespace Absher.Persistence.Configuration.TablesConfigrations.Identity
{
    public class UserEntityTypeConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), TablesSchema.IdentitySchema);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserToken).WithOne(x => x.User).HasForeignKey(typeof(UserToken), nameof(UserToken.UserId));
        }
    }
}
