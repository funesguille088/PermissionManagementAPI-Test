/*
   IDomainEvent interface defines a domain event in the application domain. 
   It extends the INotification interface from MediatR to facilitate event-driven communication.

   Usage:
   1. Define domain-specific events by implementing IDomainEvent interface.
   2. Implement properties specific to each domain event.
   3. Use the IMediator interface from MediatR to publish domain events within your application logic.
*/

using MediatR;

namespace Permissions.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccuredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
