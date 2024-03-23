/*
   The GetPermissionsResult represents the result of a query to retrieve permissions.

   Dependencies:
   - PaginatedResult<PermissionDto>: Represents a paginated result of permission DTOs.

   Usage:
   - Contains a property Permissions of type PaginatedResult<PermissionDto>.
   - Use it to access the paginated list of permissions.
*/
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Permissions.Application.Dtos;

namespace Permissions.Application.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetPermissionsResult>;

public record GetPermissionsResult(PaginatedResult<PermissionDto> Permissions);