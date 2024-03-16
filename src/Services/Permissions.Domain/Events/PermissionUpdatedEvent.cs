
using Permissions.Domain.Abstractions;
using Permissions.Domain.Models;

namespace Permissions.Domain.Events
{
    public record PermissionUpdatedEvent(Permission permission) : IDomainEvent;
}
