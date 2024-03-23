/*
 * IAggregate<T> interface represents an interface for aggregate roots in the domain.
 * It inherits from IEntity<T> and IAggregate interfaces.
 */

namespace Permissions.Domain.Abstractions
{

    /*
     * IAggregate interface represents an interface for aggregate roots in the domain.
     * It inherits from IEntity interface.
     */

    public interface IAggregate<T> : IAggregate, IEntity<T>
    {

    }
    public interface IAggregate : IEntity
    {
        // Property to get the domain events associated with the aggregate
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        // Method to clear the domain events associated with the aggregate
        IDomainEvent[] ClearDomainEvents();
    }

}
