using BuildingBlocks.CQRS;
using FluentValidation;
using Permissions.Application.Dtos;

namespace Permissions.Application.Permissions.Commands.RequestPermission;
public record RequestPermissionCommand(PermissionDto Permission) : ICommand<RequestPermissionResult>;

public record RequestPermissionResult(Guid Id);

public class RequestPermissionCommandValidator : AbstractValidator<RequestPermissionCommand>
{
    public RequestPermissionCommandValidator()
    {
        RuleFor(x => x.Permission.ApplicationName).NotEmpty().WithMessage("Application Name is required");
        RuleFor(x => x.Permission.EmployeeId).NotNull().WithMessage("Employee Id is required");
        RuleFor(x => x.Permission.PermissionGrantedEmployeeId).NotNull().WithMessage("Permission Granted Employee Id is required");
    }

}