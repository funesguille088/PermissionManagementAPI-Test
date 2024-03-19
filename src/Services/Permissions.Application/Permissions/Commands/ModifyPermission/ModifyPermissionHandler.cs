using BuildingBlocks.CQRS;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;
using Permissions.Application.Exceptions;
using Permissions.Application.Permissions.EventHandlers.Elasticsearch;

namespace Permissions.Application.Permissions.Commands.ModifyPermission;
public class ModifyPermissionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ModifyPermissionCommand, ModifyPermissionResult>
{
    private readonly PermissionSyncService? _permissionSyncService;
    public async Task<ModifyPermissionResult> Handle(ModifyPermissionCommand command, CancellationToken cancellationToken)
    {
        //Update Permission entity from command object
        //Save to database
        //Return result

        var permissionId = PermissionId.Of(command.Permission.Id);
        var permission = await dbContext.Permissions
            .FindAsync([permissionId], cancellationToken: cancellationToken);

        if (permission is null)
        {
            throw new PermissionNotFoundException(command.Permission.Id);
        }

        ModifyPermissionWithNewValues(permission, command.Permission);

        dbContext.Permissions.Update(permission);
        await dbContext.SaveChangesAsync(cancellationToken);

        //await _permissionSyncService.SyncPermissionsAsync();
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
