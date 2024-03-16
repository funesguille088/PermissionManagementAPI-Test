
using Permissions.Domain.Abstractions;
using Permissions.Domain.Enums;
using Permissions.Domain.Events;
using Permissions.Domain.ValueObjects;

namespace Permissions.Domain.Models
{
    public class Permission : Aggregate<PermissionId>
    {
        public EmployeeId EmployeeId { get; private set; } = default!;
        public string ApplicationName { get; private set; } = default!;
        public PermissionType PermissionType { get; private set; } = PermissionType.User;

        public static Permission Create(PermissionId id, EmployeeId employeeId, string applicationName, PermissionType permissionType)
        {
            var permission = new Permission
            {
                Id = id,
                EmployeeId = employeeId,
                ApplicationName = applicationName,
                PermissionType = PermissionType.User
            };

            permission.AddDomainEvent(new PermissionCreatedEvent(permission));
            return permission;
        }

        public void Update(EmployeeId employeeId, string applicationName, PermissionType permissionType)
        {
            EmployeeId = employeeId;
            ApplicationName = applicationName;
            PermissionType = permissionType;

            AddDomainEvent(new PermissionUpdatedEvent(this));
        }
    }
}
