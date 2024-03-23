

/*
   IDomainEvent interface defines a domain event in the application domain. 
   It extends the INotification interface from MediatR to facilitate event-driven communication.

   Usage:
   1. Define domain-specific events by implementing IDomainEvent interface.
   2. Implement properties specific to each domain event.
   3. Use the IMediator interface from MediatR to publish domain events within your application logic.
*/
namespace Permissions.Domain.Abstractions
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

    }
}
