
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
        public bool PermissionGranted { get; private set; } = default!;
        public EmployeeId PermissionGrantedEmployeeId { get; private set; } = default!;

        public static Permission Create(PermissionId id, EmployeeId employeeId, string applicationName, PermissionType permissionType, EmployeeId permissionGrantedEmployeeId)
        {
            var permission = new Permission
            {
                Id = id,
                EmployeeId = employeeId,
                ApplicationName = applicationName,
                PermissionType = permissionType,
                PermissionGranted = false,
                PermissionGrantedEmployeeId = permissionGrantedEmployeeId
            };

            permission.AddDomainEvent(new PermissionCreatedEvent(permission));
            return permission;
        }

        public void Update(EmployeeId employeeId, string applicationName, PermissionType permissionType, bool permissionGranted, EmployeeId permissionGrantedEmployeeId)
        {
            EmployeeId = employeeId;
            ApplicationName = applicationName;
            PermissionType = permissionType;
            PermissionGranted = permissionGranted;
            PermissionGrantedEmployeeId = permissionGrantedEmployeeId;

            AddDomainEvent(new PermissionUpdatedEvent(this));
        }
    }
}
