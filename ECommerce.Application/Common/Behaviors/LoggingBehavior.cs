using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ECommerce.Application.Common.Behaviors
{

    public sealed class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Handling {Request}",
                typeof(TRequest).Name);

            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation(
                "Handled {Request} in {Elapsed} ms",
                typeof(TRequest).Name,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
    }
}
