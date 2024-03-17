using BuildingBlocks.CQRS;
using Permissions.Application.Data;
using Permissions.Application.Exceptions;
using Permissions.Domain.ValueObjects;

namespace Permissions.Application.Permissions.Commands.DeletePermission
{
    public class DeletePermissionHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeletePermissionCommand, DeletePermissionResult>
    {
        public async Task<DeletePermissionResult> Handle(DeletePermissionCommand command, CancellationToken cancellationToken)
        {
            //Delete Permission entity from command object
            //Save to Database
            //Return result
            
            var permissionId= PermissionId.Of(command.PermissionId);
            var permission = await dbContext.Permissions
                .FindAsync([permissionId], cancellationToken: cancellationToken);

            if (permission is null)
            {
                throw new PermissionNotFoundException(command.PermissionId);
            }

            dbContext.Permissions.Remove(permission);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeletePermissionResult(true);
        }
    }
}
