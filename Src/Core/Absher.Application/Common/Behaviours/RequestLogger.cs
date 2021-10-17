using Absher.Interfaces.UserResolverHandler;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        //private readonly ILogger _logger;
        private readonly IUserResolverHandler _userResolverHandler;

        public RequestLogger(/*ILogger<TRequest> logger,*/ IUserResolverHandler userResolverHandler)
        {
            //_logger = logger;
            _userResolverHandler = userResolverHandler;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            Log.Information("Absher Request: {Name}, {@UserId}, {@Request}",
                name, _userResolverHandler.GetUserId(), request);

            return Task.CompletedTask;
        }
    }
}
