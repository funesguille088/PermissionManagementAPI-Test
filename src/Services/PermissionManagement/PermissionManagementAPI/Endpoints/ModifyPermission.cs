
using Permissions.Application.Permissions.Commands.ModifyPermission;

namespace PermissionManagementAPI.Endpoints;

//Accepts a ModifyPermission Request
//Maps the request to a ModifyPermissionCommand
//Sends the command for processing
//Returns a success or error response based on the outcome

public record ModifyPermissionRequest(PermissionDto Permission);
public record ModifyPermissionResponse(bool IsSuccess);

public class ModifyPermission : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/ModifyPermissions", async (ModifyPermissionRequest request, ISender sender) =>
        {
            var command = request.Adapt<ModifyPermissionCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<ModifyPermissionResponse>();

            return Results.Ok(response);
        })
            .WithName("ModifyPermission")
            .Produces<RequestPermissionResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Modify Permission")
            .WithDescription("Modify Permission");
    }
}
