/*
   The PermissionExtensions class provides extension methods for converting Permission entities to PermissionDto objects.

   Usage:
   1. Call the ToPermissionDtoList extension method on a collection of Permission entities to convert them into PermissionDto objects.

   Methods:
   - ToPermissionDtoList(IEnumerable<Permission> permissions): Converts a collection of Permission entities to a collection of PermissionDto objects.

   Note: This class is defined as static, and its methods are defined as extension methods, allowing them to be easily used in LINQ queries and other scenarios.

   Dependencies:
   - PermissionDto: The PermissionDto class represents a data transfer object (DTO) for permission-related data.
   - Permission: The Permission class represents the domain model entity for permissions.

*/

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
