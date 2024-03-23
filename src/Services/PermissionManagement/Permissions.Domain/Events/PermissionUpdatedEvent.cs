/*
   PermissionUpdatedEvent represents an event that occurs when a permission is updated.

   Usage:
   1. Define handlers for PermissionUpdatedEvent to react to permission update events.
   2. Use PermissionUpdatedEvent to encapsulate information about the updated permission.
*/

using Permissions.Domain.Abstractions;
using Permissions.Domain.Models;

namespace Permissions.Domain.Events
{
    public record PermissionUpdatedEvent(Permission permission) : IDomainEvent;
}
