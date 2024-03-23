/*
   The ModifyPermissionCommand represents a command to modify a permission entity. 
   It contains the data required to update a permission, encapsulated within a PermissionDto.

   Usage:
   1. Instantiate the ModifyPermissionCommand with a PermissionDto object containing the updated permission information.
   2. Execute the command by passing it to a command handler.

   Dependencies:
   - ICommand<ModifyPermissionResult>: Represents the command interface with ModifyPermissionResult as the result type.
   - PermissionDto: Data transfer object representing the permission information to be modified.
   - ModifyPermissionResult: Represents the result of the modify permission operation.
   - ModifyPermissionCommandValidator: Validates the ModifyPermissionCommand before execution.
*/

using BuildingBlocks.CQRS;
using FluentValidation;
using Permissions.Application.Dtos;

namespace Permissions.Application.Permissions.Commands.ModifyPermission;

// Represents a command to modify a permission
public record ModifyPermissionCommand(PermissionDto Permission)
    : ICommand<ModifyPermissionResult>;

// Represents the result of the modify permission operation
public record ModifyPermissionResult(bool IsSuccess);

// Validator for the ModifyPermissionCommand
public class ModifyPermissionCommandValidator : AbstractValidator<ModifyPermissionCommand>
{
    public ModifyPermissionCommandValidator()
    {
        // Validation rules for the ModifyPermissionCommand

        RuleFor(x => x.Permission.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Permission.ApplicationName).NotEmpty().WithMessage("Application Name is required");
        RuleFor(x => x.Permission.EmployeeId).NotNull().WithMessage("Employee Id is required");
    }
}
