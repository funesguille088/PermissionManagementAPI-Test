using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Application.Extentions;
using Permissions.Domain.Models;

namespace Permissions.Application.Permissions.Queries.GetPermissions;

public class GetPermissionsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPermissionsQuery, GetPermissionsResult>
{
    public async Task<GetPermissionsResult> Handle(GetPermissionsQuery query, CancellationToken cancellationToken)
    {
        //get permissions with pagination
        //return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Permissions.LongCountAsync(cancellationToken);

        var permissions = await dbContext.Permissions
            .OrderBy(p => p.ApplicationName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetPermissionsResult(
            new PaginatedResult<PermissionDto>(
                pageIndex,
                pageSize,
                totalCount,
                permissions.ToPermissionDtoList()));
    }
}
