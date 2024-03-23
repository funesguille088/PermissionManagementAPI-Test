/*
 * This class represents an interceptor for dispatching domain events before saving changes to the database.
 */

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Permissions.Domain.Abstractions;

namespace Permissions.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        
        // Method called when saving changes to the database synchronously
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        // Method to dispatch domain events for all aggregates in the DbContext
        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            // Get all aggregates with domain events
            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a => a.Entity.DomainEvents.Any())
                .Select(a => a.Entity);

            // Flatten the list of domain events
            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            // Clear domain events for all aggregates
            aggregates.ToList().ForEach(a => a.ClearDomainEvents());

            // Publish domain events using the mediator
            foreach ( var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
