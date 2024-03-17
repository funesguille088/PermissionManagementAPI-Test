using Permissions.Application.Dtos;

namespace Permissions.Application.Extentions;

public static class PermissionExtentions
{
    public static IEnumerable<PermissionDto> ToPermissionDtoList(this IEnumerable<PermissionDto> permissions)
    {
        return permissions.Select(permission => new PermissionDto(
            Id: permission.Id,
            EmployeeId: permission.EmployeeId,
            ApplicationName: permission.ApplicationName,
            PermissionType: permission.PermissionType,
            PermissionGranted: permission.PermissionGranted,
            PermissionGrantedEmployeeId: permission.PermissionGrantedEmployeeId));
    }
}
