
using BuildingBlocks.CQRS;
using FluentValidation;
using Permissions.Application.Permissions.Commands.ModifyPermission;

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