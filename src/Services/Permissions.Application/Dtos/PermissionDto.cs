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
