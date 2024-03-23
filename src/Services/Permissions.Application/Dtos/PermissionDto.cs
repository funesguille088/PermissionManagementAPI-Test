/*
   The PermissionDto record represents a data transfer object (DTO) for Permission entities.

   Usage:
   1. Use this DTO to transfer Permission data between different layers of the application, such as between the application service layer and the presentation layer.
   2. Map Permission entities to PermissionDto instances and vice versa as needed.

   Properties:
   - Id: The unique identifier of the permission.
   - EmployeeId: The unique identifier of the employee associated with the permission.
   - ApplicationName: The name of the application for which the permission is granted.
   - PermissionType: The type of permission (e.g., User, Admin).
   - PermissionGranted: A boolean indicating whether the permission is granted.
   - PermissionGrantedEmployeeId: The unique identifier of the employee who granted the permission.
*/

using Permissions.Domain.Enums;

namespace Permissions.Application.Dtos;

public record PermissionDto(
    Guid Id,
    Guid EmployeeId,
    string ApplicationName,
    PermissionType PermissionType,
    bool PermissionGranted,
    Guid PermissionGrantedEmployeeId
    );
