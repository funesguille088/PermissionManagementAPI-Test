/*
   The DeletePermissionHandler class is responsible for handling the DeletePermissionCommand by deleting the corresponding permission entity.

   Usage:
   1. Instantiate the DeletePermissionHandler with an instance of IApplicationDbContext.
   2. Call the Handle method with a DeletePermissionCommand to delete the permission.

   Dependencies:
   - ICommandHandler<DeletePermissionCommand, DeletePermissionResult>: Defines the contract for a command handler that handles DeletePermissionCommand and produces DeletePermissionResult.
   - DeletePermissionCommand: Represents the command to delete a permission.
   - DeletePermissionResult: Represents the result of the delete permission operation.
   - IApplicationDbContext: Represents the interface for the application's database context.

*/

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
            // Retrieve permission entity from the database
            var permissionId = PermissionId.Of(command.PermissionId);
            var permission = await dbContext.Permissions
                .FindAsync([permissionId], cancellationToken: cancellationToken);

            // If permission is not found, throw NotFoundException
            if (permission is null)
            {
                throw new PermissionNotFoundException(command.PermissionId);
            }

            // Remove permission from the database
            dbContext.Permissions.Remove(permission);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Return success result
            return new DeletePermissionResult(true);
        }
    }
}
