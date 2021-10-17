using Absher.Utility.CommonModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absher.WebAPI.Extentions
{
    public static class SectionsConfiguration
    {
        public static void AddSectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSetting>(configuration.GetSection("AppSetting"));
            var appSettings = configuration.GetSection("AppSetting").Get<AppSetting>();
            services.AddSingleton(appSettings);

            //services.Configure<AppJWTSetting>(configuration.GetSection("AppJWTSetting"));
            //var appJWTSetting = configuration.GetSection("AppJWTSetting").Get<AppJWTSetting>();
            //services.AddSingleton(appJWTSetting);

            services.Configure<RedisSetting>(configuration.GetSection("RedisSetting"));
            var redisSettings = configuration.GetSection("RedisSetting").Get<RedisSetting>();
            services.AddSingleton(redisSettings);
        }
    }
}
