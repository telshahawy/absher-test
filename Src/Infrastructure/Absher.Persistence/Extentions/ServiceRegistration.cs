using Absher.Interfaces.DBContext;
using Absher.Interfaces.Repositories;
using Absher.Interfaces.UserResolverHandler;
using Absher.Persistence.Configuration;
using Absher.Persistence.DBContext;
using Absher.Persistence.Repositories.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Absher.Persistence.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Add DbContext
            services.AddDbContext<AbsherDbContext>(options =>
               options.UseSqlServer(GetAbsherDbContextConnectionString(),
                   b => b.MigrationsAssembly(typeof(AbsherDbContext).Assembly.FullName)));
            services.AddScoped<DbContext, AbsherDbContext>();
            services.AddScoped<IAbsherDbContext>(provider => provider.GetService<AbsherDbContext>());
            #endregion

            #region Add Repository
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepositoryBase<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepositoryBase<>));
            #endregion

            #region Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Add Http Context Accessor
            services.AddHttpContextAccessor();
            services.AddScoped<IUserResolverHandler, UserResolverHandler.UserResolverHandler>();
            #endregion
        }

        public static void UseAutoMigrateDatabase<TDbContext>(this IApplicationBuilder builder)
            where TDbContext : DbContext
        {
            using (var serviceScope =
                builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TDbContext>().Database.Migrate();
            }
        }

        private static string GetAbsherDbContextConnectionString()
        {
            return new DatabaseConfiguration().GetAbsherDbContextConnectionString();
        }
    }
}
