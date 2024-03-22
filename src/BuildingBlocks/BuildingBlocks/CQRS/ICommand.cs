/*
 * ICommand: A marker interface for command objects without a specific response type.
 * 
 * This interface is used to represent command objects that do not have a specific response type.
 * It inherits from ICommand<Unit> to indicate that it doesn't return any specific response.
 * 
 * ICommand<TResponse>: A marker interface for command objects with a specific response type.
 * 
 * This generic interface is used to represent command objects that have a specific response type.
 * It inherits from IRequest<TResponse>, indicating that it is a MediatR request with a response.
 */

using MediatR;

namespace BuildingBlocks.CQRS;

// ICommand interface definition
public interface ICommand : ICommand<Unit>
{
}

// Generic ICommand interface definition with a response type parameter
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
