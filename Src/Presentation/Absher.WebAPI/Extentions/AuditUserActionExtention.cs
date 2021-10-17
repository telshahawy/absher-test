using Absher.Domain.Entities.Audit;
using Absher.Domain.Entities.TablesSchema;
using Absher.Interfaces.Domain;
using Absher.Interfaces.UserResolverHandler;
using Audit.WebApi;
using Audit.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Core;
using Absher.Utility.Extensions;

namespace Absher.WebAPI.Extentions
{
    public static class AuditUserActionExtention
    {
        public static void AdAuditUserAction(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMvc(options =>
            //  {
            //      options.AddAudit();
            //      options.EnableEndpointRouting = false;
            //  });

            var sp = services.BuildServiceProvider();
            var userResolverHandler = sp.GetService<IUserResolverHandler>();
            //var sqlAudit = new Audit.SqlServer.Providers.SqlDataProvider()
            //{
            //    ConnectionString = configuration.GetConnectionString("AbsherDbContextConnection"),
            //    Schema = TablesSchema.AuditSchema,
            //    TableName = nameof(AuditUserAction),
            //    JsonColumnName = nameof(AuditUserAction.JsonData),
            //    IdColumnName = nameof(AuditUserAction.AuditUserActionId),
            //    LastUpdatedDateColumnName = nameof(IEntity<Guid>.CreatedDate),
            //    CustomColumns = new List<Audit.SqlServer.CustomColumn>()
            //    {
            //        new Audit.SqlServer.CustomColumn(nameof(AuditUserAction.EventType), ev => ev.EventType),
            //        new Audit.SqlServer.CustomColumn(nameof(IEntity<Guid>.CreatedBy), ev => userResolverHandler.GetUserId())
            //    }
            //};

            Audit.Core.Configuration.Setup()
                    .UseSqlServer(config => config
                    .ConnectionString(configuration.GetConnectionString("AbsherDbContextConnection"))
                    .Schema(TablesSchema.AuditSchema)
                    .TableName(nameof(AuditUserAction))
                    .JsonColumnName(nameof(AuditUserAction.JsonData))
                    .IdColumnName(nameof(AuditUserAction.AuditUserActionId))
                    .LastUpdatedColumnName(nameof(AuditUserAction.UpdatedDate))
                    .CustomColumn(nameof(AuditUserAction.EventType), ev => ev.EventType)
                    .CustomColumn(nameof(AuditUserAction.CreatedBy), ev => userResolverHandler.GetUserId())
                    .CustomColumn(nameof(AuditUserAction.UpdatedBy), ev => userResolverHandler.GetUserId())
                    .CustomColumn(nameof(AuditUserAction.CreatedDate), ev => DateTime.Now.GetCurrentDateTime())
                );
            //.UseDynamicAsyncProvider
            //(config => config.OnInsertAndReplace(async ev =>
            //    {
            //        Audit.Core.Configuration.DataProvider = null;
            //        if (ev.GetWebApiAuditAction() != null)
            //        {
            //            Audit.Core.Configuration.DataProvider = sqlAudit;
            //        }
            //    })
            //);

            services.AddMvc(mvc =>
            {
                mvc.Filters.Add(new AuditIgnoreActionFilter());
            });

        }
    }
}
