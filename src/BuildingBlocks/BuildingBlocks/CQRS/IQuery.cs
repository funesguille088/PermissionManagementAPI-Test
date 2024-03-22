/*
 * IQuery<TResponse>: A marker interface for query objects with a specific response type.
 * 
 * This generic interface is used to represent query objects that have a specific response type.
 * It inherits from IRequest<TResponse>, indicating that it is a MediatR request with a specific response.
 * 
 * Generic parameters:
 * - TResponse: The type of response returned by the query.
 */
using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> 
    where TResponse : notnull
{

}
