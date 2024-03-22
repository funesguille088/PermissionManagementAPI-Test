

using BuildingBlocks.Behaviors;
using BuildingBlocks.CQRS;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Nest;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Application.Permissions.EventHandlers.Elasticsearch;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;
using Serilog;

namespace Permissions.Application.Permissions.Commands.RequestPermission;
public class RequestPermissionHandler : ICommandHandler<RequestPermissionCommand, RequestPermissionResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<RequestPermissionHandler> _logger;
    private readonly IElasticLowLevelClient _elasticClient;

    // Constructor with dbContext parameter
    public RequestPermissionHandler(IApplicationDbContext dbContext, ILogger<RequestPermissionHandler> logger, IElasticLowLevelClient elasticClient)
        : this(dbContext) // Call the other constructor with dbContext parameter
    {
        _logger = logger;
        _elasticClient = elasticClient;
    }

    // Constructor with dbContext parameter
    public RequestPermissionHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RequestPermissionResult> Handle(RequestPermissionCommand command, CancellationToken cancellationToken)
    {
        // Create Permission entity from command object
        // Save to database
        // Return result
    
        var permission = RequestPermission(command.Permission);

        _dbContext.Permissions.Add(permission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var permissionSyncService = new PermissionSyncService(_elasticClient);
        await permissionSyncService.SyncPermissionsAsync(_dbContext);

        return new RequestPermissionResult(permission.Id.Value);

    }

    private Permission RequestPermission(PermissionDto permissionDto)
    {
        var newPermission = Permission.Create(
            id: PermissionId.Of(Guid.NewGuid()),
            employeeId: EmployeeId.Of(permissionDto.EmployeeId),
            applicationName: permissionDto.ApplicationName,
            permissionType: permissionDto.PermissionType,
            permissionGrantedEmployeeId: EmployeeId.Of(permissionDto.PermissionGrantedEmployeeId));

        return newPermission;
    }
}
