/*
 * ICommandHandler: A marker interface for command handlers without a specific response type.
 * 
 * This interface is used to represent command handlers that handle command objects without a specific response type.
 * It inherits from ICommandHandler<TCommand, Unit> to indicate that it doesn't return any specific response.
 * 
 * ICommandHandler<TCommand, TResponse>: A marker interface for command handlers with a specific response type.
 * 
 * This generic interface is used to represent command handlers that handle command objects with a specific response type.
 * It inherits from IRequestHandler<TCommand, TResponse>, indicating that it is a MediatR request handler with a specific response type.
 * 
 * The generic parameters:
 * - TCommand: The type of command being handled.
 * - TResponse: The type of response returned by the command handler.
 */

using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
    where TResponse : notnull

{

}
