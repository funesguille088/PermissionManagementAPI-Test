/*
   The ModifyPermissionHandler is responsible for handling the ModifyPermissionCommand,
   which modifies an existing permission entity.

   Usage:
   1. Instantiate the ModifyPermissionHandler with required dependencies: IApplicationDbContext, ILogger, and IElasticLowLevelClient.
   2. Execute the command by passing it to the Handle method.

   Dependencies:
   - ICommandHandler<ModifyPermissionCommand, ModifyPermissionResult>: Represents the command handler interface with ModifyPermissionCommand and ModifyPermissionResult as the command and result types.
   - IApplicationDbContext: Represents the application's database context interface.
   - ILogger<ModifyPermissionHandler>: Represents the logger interface for logging.
   - IElasticLowLevelClient: Represents the Elasticsearch low-level client interface for indexing permissions.
   - ModifyPermissionCommand: Represents the command to modify a permission.
   - ModifyPermissionResult: Represents the result of the modify permission operation.
   - Permission: Represents the permission entity.
   - PermissionDto: Represents the data transfer object for permission.
   - PermissionId: Represents the value object for permission id.
   - PermissionNotFoundException: Represents an exception thrown when the permission is not found.
   - PermissionSyncService: Service for synchronizing permissions with Elasticsearch.

*/

using BuildingBlocks.CQRS;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;
using Permissions.Application.Exceptions;
using Permissions.Application.Permissions.EventHandlers.Elasticsearch;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;

namespace Permissions.Application.Permissions.Commands.ModifyPermission;


public class ModifyPermissionHandler : ICommandHandler<ModifyPermissionCommand, ModifyPermissionResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<ModifyPermissionHandler> _logger;
    private readonly IElasticLowLevelClient _elasticClient;

    // Constructor with dependencies
    public ModifyPermissionHandler(IApplicationDbContext dbContext, ILogger<ModifyPermissionHandler> logger, IElasticLowLevelClient elasticClient)
        : this(dbContext) // Call the other constructor with dbContext parameter
    {
        _logger = logger;
        _elasticClient = elasticClient;
    }

    // Constructor with dbContext parameter
    public ModifyPermissionHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Handle method to process the ModifyPermissionCommand
    public async Task<ModifyPermissionResult> Handle(ModifyPermissionCommand command, CancellationToken cancellationToken)
    {
        // Find the permission entity in the database
        var permissionId = PermissionId.Of(command.Permission.Id);
        var permission = await _dbContext.Permissions
            .FindAsync([permissionId], cancellationToken: cancellationToken);

        // If permission is not found, throw an exception
        if (permission is null)
        {
            throw new PermissionNotFoundException(command.Permission.Id);
        }

        // Update permission entity with new values from the command
        ModifyPermissionWithNewValues(permission, command.Permission);

        // Update the permission entity in the database
        _dbContext.Permissions.Update(permission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Synchronize permissions with Elasticsearch
        var permissionSyncService = new PermissionSyncService(_elasticClient);
        await permissionSyncService.SyncPermissionsAsync(_dbContext);

        // Return success result
        return new ModifyPermissionResult(true);
    }

    // Method to update permission entity with new values
    private void ModifyPermissionWithNewValues(Permission permission, PermissionDto permissionDto)
    {
        permission.Update(
            employeeId: EmployeeId.Of(permissionDto.EmployeeId),
            applicationName: permissionDto.ApplicationName,
            permissionType: permissionDto.PermissionType,
            permissionGranted: permissionDto.PermissionGranted,
            permissionGrantedEmployeeId: EmployeeId.Of(permissionDto.PermissionGrantedEmployeeId));
    }

}