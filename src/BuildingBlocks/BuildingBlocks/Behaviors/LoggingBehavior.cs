/*
 * LoggingBehavior: A pipeline behavior for logging request and response information.
 * 
 * This behavior logs the start and end of request handling, along with request/response details.
 * It also logs a warning if the request takes more than 3 seconds to handle.
 * 
 * Usage:
 * This behavior can be added to the MediatR pipeline to log information for each request.
 */

using MediatR; // Importing MediatR library for IRequest and IPipelineBehavior
using Microsoft.Extensions.Logging; // Importing Microsoft.Extensions.Logging for logging capabilities
using System.Diagnostics; // Importing System.Diagnostics for Stopwatch

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger) // Constructor to inject ILogger
        : IPipelineBehavior<TRequest, TResponse> // Implementing IPipelineBehavior
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Log start of request handling with request and response information
            logger.LogInformation("[START] Handle request = {Request} - Response = {Response} - RequestData = {RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start(); // Start timer to measure request handling time

            var response = await next(); // Proceed to handle the request

            timer.Stop();
            var timeTaken = timer.Elapsed;

            // Log warning if request handling takes more than 3 seconds
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                    typeof(TRequest).Name, timeTaken.Seconds);
            }

            // Log end of request handling with request and response types
            logger.LogInformation("[END] Handled {Request} with {Response}",
                    typeof(TRequest).Name, typeof(TResponse).Name);

            return response; // Return the response
        }
    }
}