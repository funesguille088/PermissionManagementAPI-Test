/*
 * PaginatedResult<TEntity>: Represents a paginated result containing data and pagination information.
 * 
 * This class is used to encapsulate paginated data along with information about the pagination such as page index, page size, and total count.
 * It provides properties to access page index, page size, total count, and the paginated data.
 * 
 * Generic parameters:
 * - TEntity: The type of entities contained in the paginated result.
 */


namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>
    (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex; 
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
    
}
