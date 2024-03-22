using BuildingBlocks.CQRS;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;
using Permissions.Application.Exceptions;
using Permissions.Application.Permissions.EventHandlers.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Permissions.Application.Permissions.Commands.RequestPermission;
using Nest;

namespace Permissions.Application.Permissions.Commands.ModifyPermission;


public class ModifyPermissionHandler : ICommandHandler<ModifyPermissionCommand, ModifyPermissionResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<ModifyPermissionHandler> _logger;
    private readonly IElasticLowLevelClient _elasticClient;

    // Constructor with dbContext parameter
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

    public async Task<ModifyPermissionResult> Handle(ModifyPermissionCommand command, CancellationToken cancellationToken)
    {
        //Update Permission entity from command object
        //Save to database
        //Return result

        var permissionId = PermissionId.Of(command.Permission.Id);
        var permission = await _dbContext.Permissions
            .FindAsync([permissionId], cancellationToken: cancellationToken);

        if (permission is null)
        {
            throw new PermissionNotFoundException(command.Permission.Id);
        }

        ModifyPermissionWithNewValues(permission, command.Permission);

        _dbContext.Permissions.Update(permission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var permissionSyncService = new PermissionSyncService(_elasticClient);
        await permissionSyncService.SyncPermissionsAsync(_dbContext);

        return new ModifyPermissionResult(true);
    }

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
//public class ModifyPermissionHandler(IApplicationDbContext dbContext)
//    : ICommandHandler<ModifyPermissionCommand, ModifyPermissionResult>
//{
//    //private readonly PermissionSyncService? _permissionSyncService;
//    public async Task<ModifyPermissionResult> Handle(ModifyPermissionCommand command, CancellationToken cancellationToken)
//    {
//        //Update Permission entity from command object
//        //Save to database
//        //Return result

//        var permissionId = PermissionId.Of(command.Permission.Id);
//        var permission = await dbContext.Permissions
//            .FindAsync([permissionId], cancellationToken: cancellationToken);

//        if (permission is null)
//        {
//            throw new PermissionNotFoundException(command.Permission.Id);
//        }

//        ModifyPermissionWithNewValues(permission, command.Permission);

//        dbContext.Permissions.Update(permission);
//        await dbContext.SaveChangesAsync(cancellationToken);

//        var permissionSyncService = new PermissionSyncService(_elasticClient);
//        await permissionSyncService.SyncPermissionsAsync(dbContext);

//        return new ModifyPermissionResult(true);
//    }

//    private void ModifyPermissionWithNewValues(Permission permission, PermissionDto permissionDto)
//    {
//        permission.Update(
//            employeeId: EmployeeId.Of(permissionDto.EmployeeId),
//            applicationName: permissionDto.ApplicationName,
//            permissionType: permissionDto.PermissionType,
//            permissionGranted: permissionDto.PermissionGranted,
//            permissionGrantedEmployeeId: EmployeeId.Of(permissionDto.PermissionGrantedEmployeeId));
//    }
//}
