/*
 * ValidationBehavior: A pipeline behavior for validating requests using FluentValidation.
 * 
 * This behavior ensures that incoming requests, which implement the ICommand interface,
 * are validated using FluentValidation validators. If validation fails, a ValidationException
 * is thrown.
 * 
 * Usage:
 * This behavior can be added to the MediatR pipeline to validate requests before handling.
 */

using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TRequest>
{
    // Implementation of Handle method from ValidationBehavior
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request); // Creating validation context

        // Performing asynchronous validation on request using injected validators
        var validationResults =
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Aggregating validation failures
        var failures =
            validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        // Throwing ValidationException if validation failures exist
        if (failures.Any())
            throw new ValidationException(failures);

        return await next(); // Proceeding to handle the request
    }

}
