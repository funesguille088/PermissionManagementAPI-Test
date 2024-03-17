using BuildingBlocks.CQRS;
using FluentValidation;
using Permissions.Application.Dtos;

namespace Permissions.Application.Permissions.Commands.ModifyPermission;
public record ModifyPermissionCommand(PermissionDto Permission)
    : ICommand<ModifyPermissionResult>;

public record ModifyPermissionResult(bool IsSuccess);

public class ModifyPermissionCommandValidator : AbstractValidator<ModifyPermissionCommand>
{
    public ModifyPermissionCommandValidator()
    {
        RuleFor(x => x.Permission.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Permission.ApplicationName).NotEmpty().WithMessage("Application Name is required");
        RuleFor(x => x.Permission.EmployeeId).NotNull().WithMessage("Employee Id is required");
    }
}
