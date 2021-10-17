using Absher.Domain.Cache;
using Absher.Domain.ResponseModel;
using Absher.Domain.Services.Infrastructure;
using Absher.Interfaces.Cache;
using Absher.Interfaces.Domain.Response;
using Absher.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped(typeof(IReadService<>), typeof(ReadServiceBase<>));
            services.AddScoped(typeof(IWriteService<>), typeof(WriteServiceBase<>));
            services.AddTransient(typeof(IResponseResult<>), typeof(ResponseResult<>));
            services.AddTransient(typeof(IPagedResponseResult<>), typeof(PagedResponseResult<>));
        }
    }
}
