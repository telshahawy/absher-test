using Absher.Domain.ResponseModel;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
namespace Absher.WebAPI.Extentions
{
    public static class GlobalExceptionSetup
    {
        public static void UseGlobalException(this IApplicationBuilder app/*, ILoggerFactory loggerFactory*/)
        {
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                            if (errorFeature != null)
                            {
                                var exception = errorFeature.Error;
                                //var logger = loggerFactory.CreateLogger("GlobalException");

                                if (!(exception is ValidationException validationException))
                                {
                                    //logger.LogCritical(exception, exception.Message);
                                    Log.Fatal(exception, exception.Message);
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    context.Response.AddApplicationError(exception.Message);
                                    await context.Response.WriteAsync(new ResponseResult<bool> { Entity = false, Status = HttpStatusCode.InternalServerError, Message = exception.Message }.ToString()).ConfigureAwait(false);
                                }
                                else
                                {
                                    //logger.LogInformation(exception.Message);
                                    Log.Information(exception, exception.Message);
                                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                    var errors = validationException.Errors.Select(err => new
                                    {
                                        err.PropertyName,
                                        err.ErrorMessage
                                    });
                                    var errorText = JsonSerializer.Serialize(errors);
                                    context.Response.ContentType = "application/json";
                                    await context.Response.WriteAsync(new ResponseResult<bool> { Entity = false, Status = HttpStatusCode.BadRequest, Message = errorText }.ToString(), Encoding.UTF8);
                                }
                            }
                        });
                });
        }
    }
}
