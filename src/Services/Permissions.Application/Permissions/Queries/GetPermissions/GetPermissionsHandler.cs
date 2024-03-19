using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Application.Extentions;
using Permissions.Application.Permissions.Commands.RequestPermission;
using Permissions.Domain.Models;

namespace Permissions.Application.Permissions.Queries.GetPermissions;

public class GetPermissionsHandler : IQueryHandler<GetPermissionsQuery, GetPermissionsResult>
{

    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetPermissionsHandler> _logger;

    // Constructor with dbContext parameter
    public GetPermissionsHandler(IApplicationDbContext dbContext, ILogger<GetPermissionsHandler> logger)
        : this(dbContext) // Call the other constructor with dbContext parameter
    {
        _logger = logger;
    }

    // Constructor with dbContext parameter
    public GetPermissionsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPermissionsResult> Handle(GetPermissionsQuery query, CancellationToken cancellationToken)
    {
        //get permissions with pagination
        //return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await _dbContext.Permissions.LongCountAsync(cancellationToken);

        _logger.LogInformation("Get Permission Started");

        var permissions = await _dbContext.Permissions
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
