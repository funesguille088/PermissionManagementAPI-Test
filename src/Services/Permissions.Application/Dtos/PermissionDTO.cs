
using Permissions.Domain.Enums;

namespace Permissions.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid EmployeeId,
    string ApplicationName,
    PermissionType PermissionType,
    bool PermissionGranted,
    Guid PermissionGrantedEmployeeId);

