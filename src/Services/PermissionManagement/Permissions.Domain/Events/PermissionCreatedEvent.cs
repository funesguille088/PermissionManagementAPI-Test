/*
   PermissionCreatedEvent represents an event that occurs when a permission is created.

   Usage:
   1. Define handlers for PermissionCreatedEvent to react to permission creation events.
   2. Use PermissionCreatedEvent to encapsulate information about the created permission.
*/

using Permissions.Domain.Abstractions;
using Permissions.Domain.Models;

namespace Permissions.Domain.Events
{
    public record PermissionCreatedEvent(Permission permission) : IDomainEvent;
    
}
