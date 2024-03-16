
using Permissions.Domain.Abstractions;
using Permissions.Domain.Models;

namespace Permissions.Domain.Events
{
    public record PermissionCreatedEvent(Permission permission) : IDomainEvent;
    
}
