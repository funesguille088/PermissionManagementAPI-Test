/*
   The RequestPermissionCommand represents a command to request a new permission.

   Usage:
   - Instantiate the RequestPermissionCommand with the necessary data.
   - Execute the command by passing it to the appropriate command handler.

   Dependencies:
   - ICommand<RequestPermissionResult>: Represents the command interface with RequestPermissionResult as the result type.
   - PermissionDto: Represents the data transfer object for permission.
   - RequestPermissionResult: Represents the result of the request permission operation.
   - RequestPermissionCommandValidator: Represents the validator for the RequestPermissionCommand.

*/
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
        // Validation rules for the request permission command

        RuleFor(x => x.Permission.ApplicationName).NotEmpty().WithMessage("Application Name is required");
        RuleFor(x => x.Permission.EmployeeId).NotNull().WithMessage("Employee Id is required");
        RuleFor(x => x.Permission.PermissionGrantedEmployeeId).NotNull().WithMessage("Permission Granted Employee Id is required");
    }

}