using Permissions.Application.Permissions.Commands.RequestPermission;

namespace PermissionManagementAPI.Endpoints;

//Accepts a Permission Request object
//Maps the request to a RequestPermissionCommand
//Uses MediatR to send the command to the corresponding handler
//Returns a response with the created Permission Id

public record RequestPermissionRequest(PermissionDto Permission);
public record RequestPermissionResponse(Guid Id);
public class RequestPermission : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/permissions", async (RequestPermissionRequest request, ISender sender) =>
        {
            var command = request.Adapt<RequestPermissionCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<RequestPermissionResponse>();

            return Results.Created($"/permissions/{response.Id}", response);
        })
            .WithName("RequestPermission")
            .Produces<RequestPermissionResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Request Permission")
            .WithDescription("Request Permission");
    }
}
