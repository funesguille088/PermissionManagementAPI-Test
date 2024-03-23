/*
   The DeletePermissionCommand class represents a command for deleting a permission.

   Usage:
   1. Create an instance of DeletePermissionCommand with the ID of the permission to be deleted.
   2. Execute the command using a mediator or command handler.

   Properties:
   - PermissionId: The ID of the permission to be deleted.

   Dependencies:
   - ICommand<DeletePermissionResult>: Defines the contract for a command with a result.
   - DeletePermissionResult: Represents the result of the delete permission operation.

*/


using BuildingBlocks.CQRS;
using FluentValidation;

namespace Permissions.Application.Permissions.Commands.DeletePermission;

public record DeletePermissionCommand(Guid PermissionId)
    : ICommand<DeletePermissionResult>;

public record DeletePermissionResult(bool IsSuccess);

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator()
    {
        RuleFor(x => x.PermissionId).NotEmpty().WithMessage("PermissionId is required");
    }

}