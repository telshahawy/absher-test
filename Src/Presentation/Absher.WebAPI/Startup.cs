using Absher.Application.Extentions;
using Absher.Domain.Extentions;
using Absher.Persistence.DBContext;
using Absher.Persistence.Extentions;
using Absher.WebAPI.Extentions;
using Absher.WebAPI.Hubs;
using Absher.WebAPI.Middlewares;
using Audit.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Enrichers.Sensitive;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Absher.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", false, true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
               .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();

            var baseDirectory = env.ContentRootPath;// AppDomain.CurrentDomain.BaseDirectory;
            var exprExcludedLogs = "StartsWith(SourceContext,'Hangfire') or StartsWith(SourceContext,'System') or StartsWith(SourceContext,'Microsoft')";
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .MinimumLevel.Override("System", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
               .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Verbose)
               .MinimumLevel.Override("Hangfire.BackgroundJobServer", LogEventLevel.Verbose)
               .MinimumLevel.Override("Hangfire.Server.BackgroundServerProcess", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore.SqlServer", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Migrations", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Infrastructure", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager", LogEventLevel.Verbose)
               .Enrich.WithThreadId()
               .Enrich.WithProcessId()
               .Enrich.WithUserName()
               .Enrich.WithAssemblyName()
               .Enrich.WithClientAgent()
               .Enrich.WithClientIp()
               .Enrich.WithEnvironmentName()
               .Enrich.WithExceptionData()
               .Enrich.FromLogContext()
               .Enrich.WithSensitiveDataMasking(MaskingMode.InArea, new IMaskingOperator[]
                  {
                      new EmailAddressMaskingOperator(),
                      new IbanMaskingOperator(),
                      new CreditCardMaskingOperator(false)
                  })
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\Fatal\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\Errors\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\Warning\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information).Filter.ByIncludingOnly(Matching.FromSource<RequestResponseLoggingMiddleware>()).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\RequestsAndResponses\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information).Filter.ByExcluding(Matching.FromSource<RequestResponseLoggingMiddleware>()).Filter.ByExcluding(exprExcludedLogs).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\Information\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\Debug\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Verbose).WriteTo.Map(e => new { Year = DateTime.Now.Year, Month = DateTime.Now.Month },
                   (v, wt) =>
                       wt.File(new CompactJsonFormatter(), $"{baseDirectory}\\Logs\\Verbose\\{v.Year}\\{v.Month}\\Logs_.txt", shared: true, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 209715200 /*200 MB*/, rollOnFileSizeLimit: true, retainedFileCountLimit: 7, flushToDiskInterval: TimeSpan.FromSeconds(1))
                   ))
               .CreateLogger();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Api Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
                //HTTP Header based versioning -- add in header request “x-api-version”
                config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
            });
            #endregion
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-eg"),
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.AddInitialRequestCultureProvider(new AcceptLanguageHeaderRequestCultureProvider()); 
            });
            services.AddSectionConfiguration(Configuration);
            services.AddRedisCacheSetup(Configuration);
            services.AddPersistence(Configuration);
            services.AddJWTSetup(Configuration);
            services.AddDomain();
            services.AddApplication();
            services.AddControllers();
            services.AdAuditUserAction(Configuration);
            services.AddSwaggerSetup();
            services.AddSignalR();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Absher.WebAPI", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAutoMigrateDatabase<AbsherDbContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Absher.WebAPI v1"));
            }

            app.UseSwaggerSetup();
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.SetIsOriginAllowed((host) => true);
                c.AllowCredentials();
            });

            app.UseHttpsRedirection();

            //app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseGlobalException();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuditMiddleware(_ => _
                  .FilterByRequest(rq => !rq.Path.Value.EndsWith("favicon.ico"))
                  .WithEventType("{verb}:{url}")
                  .IncludeHeaders()
                  .IncludeResponseHeaders()
                  .IncludeRequestBody()
                  .IncludeResponseBody());

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });
        }
    }
}
