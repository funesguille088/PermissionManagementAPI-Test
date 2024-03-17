using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Permissions.Application.Dtos;

namespace Permissions.Application.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery(PaginationRequest PaginationRequest)
    : IQuery<GetPermissionsResult>;

public record GetPermissionsResult(PaginatedResult<PermissionDto> Permissions);