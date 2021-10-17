using Absher.Domain.Entities.Audit;
using Absher.Domain.Entities.Chat;
using Absher.Domain.Entities.Identity;
using Absher.Interfaces.DBContext;
using Absher.Interfaces.UserResolverHandler;
using Absher.Persistence.Configuration.TablesConfigrations;
using Absher.Persistence.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Persistence.DBContext
{
    public class AbsherDbContext : DbContext, IAbsherDbContext
    {
        private readonly IUserResolverHandler _userResolverHandler;

        internal AbsherDbContext(DbContextOptions<AbsherDbContext> options) : base(options)
        {
        }

        public AbsherDbContext(DbContextOptions<AbsherDbContext> options, IUserResolverHandler userResolverHandler) : base(options)
        {
            _userResolverHandler = userResolverHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationDBTables.ApplyConfigurations(modelBuilder);
            modelBuilder.Seed();
            modelBuilder.Ignore<EntityFrameworkCore.MemoryJoin.QueryModelClass>();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.ApplyAuditInformation(_userResolverHandler);
            var temoraryAuditEntities = ChangeTracker.AuditNonTemporaryProperties(AuditChangedData, _userResolverHandler).Result;
            var result = base.SaveChanges();
            if (result >= 0)
            {
                var auditTemporaryPropertiesResult = ChangeTracker.AuditTemporaryProperties(temoraryAuditEntities, AuditChangedData).Result;
                if (auditTemporaryPropertiesResult)
                    base.SaveChanges();
            }
            return result;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation(_userResolverHandler);

            var temoraryAuditEntities = await ChangeTracker.AuditNonTemporaryProperties(AuditChangedData, _userResolverHandler);
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (result >= 0)
            {
                var auditTemporaryPropertiesResult = await ChangeTracker.AuditTemporaryProperties(temoraryAuditEntities, AuditChangedData);
                if (auditTemporaryPropertiesResult)
                    await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            return result;
        }

        #region Tables

        #region Audit
        public DbSet<AuditUserAction> AuditUserAction { get; set; }
        public DbSet<AuditChangedData> AuditChangedData { get; set; }
        #endregion

        #region Memory Join
        // This is virtual table used by MemoryJoin Libirary in memory only not create in DB
        public DbSet<EntityFrameworkCore.MemoryJoin.QueryModelClass> QueryData { get; set; }
        #endregion

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        #endregion

        #region Chat
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<ChatInvitation> ChatInvitations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserChatGroup> UserChatGroups { get; set; }

        #endregion
         
        #endregion
    }

}
