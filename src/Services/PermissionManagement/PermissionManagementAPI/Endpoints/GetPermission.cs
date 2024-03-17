using BuildingBlocks.Pagination;
using Permissions.Application.Permissions.Queries.GetPermissions;

namespace PermissionManagementAPI.Endpoints;

//Accepts the pagination parameters
//Constructs a GetPermissionsQuery with these parameters
//Retrieves the data and returns it in a paginated format.

public record GetPermissionsResponse(PaginatedResult<PermissionDto> Permissions);
public class GetPermission : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetPermissions", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetPermissionsQuery(request));

            var response = result.Adapt<GetPermissionsResponse>();

            return Results.Ok(response);
        })
            .WithName("GetPermissions")
            .Produces<GetPermissionsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Permission")
            .WithDescription("Get Permission");
    }
}
