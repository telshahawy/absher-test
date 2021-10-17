using Absher.Application.Common.Behaviours;
using Absher.Application.Features.PostFeature.Commands.CreatePost;
using Absher.Application.Features.PostFeature.Validators;
using Absher.Application.Managers;
using Absher.Interfaces.Cache;
using Absher.Interfaces.Domain.Response;
using Absher.Interfaces.Managers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(CreatePostCommandValidator).Assembly);

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IChatManager, ChatManager>();
        }
    }
}
