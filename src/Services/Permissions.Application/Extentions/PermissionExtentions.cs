using Permissions.Application.Dtos;
using Permissions.Domain.Models;

namespace Permissions.Application.Extentions;

public static class PermissionExtentions
{
    public static IEnumerable<PermissionDto> ToPermissionDtoList(this IEnumerable<Permission> permissions)
    {
        return permissions.Select(permission => new PermissionDto(
            Id: permission.Id.Value,
            EmployeeId: permission.EmployeeId.Value,
            ApplicationName: permission.ApplicationName,
            PermissionType: permission.PermissionType,
            PermissionGranted: permission.PermissionGranted,
            PermissionGrantedEmployeeId: permission.PermissionGrantedEmployeeId.Value));
    }
}
