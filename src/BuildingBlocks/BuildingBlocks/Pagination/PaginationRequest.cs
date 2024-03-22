/*
 * PaginationRequest: Represents a request for pagination.
 * 
 * This record is used to encapsulate pagination parameters such as page index and page size.
 * It provides default values for page index (0) and page size (10).
 */
namespace BuildingBlocks.Pagination;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
