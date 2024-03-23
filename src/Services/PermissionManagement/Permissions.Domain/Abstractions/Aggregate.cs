/*
 * Aggregate<TId> class represents an abstract base class for aggregate roots in the domain.
 * It provides functionality for managing domain events.
 *
 * Usage:
 * - Inherit from Aggregate<TId> in domain classes that serve as aggregate roots.
 * - Use AddDomainEvent method to add domain events to the aggregate.
 * - Use ClearDomainEvents method to clear all domain events and retrieve them.
 */

namespace Permissions.Domain.Abstractions
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        // List to store domain events
        private readonly List<IDomainEvent> _domainEvents = new();
        
        // Property to expose read-only list of domain events
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // Method to add a domain event to the list
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        // Method to clear all domain events and return them
        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] dequeueEvents = _domainEvents.ToArray();

            _domainEvents.Clear();

            return dequeueEvents;
        }
    }
}
