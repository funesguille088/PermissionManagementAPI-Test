

using BuildingBlocks.CQRS;
using Permissions.Application.Data;
using Permissions.Application.Dtos;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;

namespace Permissions.Application.Permissions.Commands.RequestPermission;
public class RequestPermissionHandler(IApplicationDbContext dbContext) 
    : ICommandHandler<RequestPermissionCommand, RequestPermissionResult>
{
    public async Task<RequestPermissionResult> Handle(RequestPermissionCommand command, CancellationToken cancellationToken)
    {
        // Create Permission entity from command object
        // Save to database
        // Return result

        var permission = RequestPermission(command.Permission);

        dbContext.Permissions.Add(permission);
        await dbContext.SaveChangesAsync(cancellationToken);

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
