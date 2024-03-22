/*
 * IQueryHandler<TQuery, TResponse>: A marker interface for query handlers with a specific response type.
 * 
 * This generic interface is used to represent query handlers that handle query objects with a specific response type.
 * It inherits from IRequestHandler<TQuery, TResponse>, indicating that it is a MediatR request handler with a specific response type.
 * 
 * Generic parameters:
 * - TQuery: The type of query being handled.
 * - TResponse: The type of response returned by the query handler.
 */

using MediatR;

namespace BuildingBlocks.CQRS
{

    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
