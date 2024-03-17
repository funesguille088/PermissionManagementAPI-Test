

using BuildingBlocks.CQRS;

namespace Permissions.Application.Permissions.Commands.RequestPermission;
public class RequestPermissionHandler : ICommandHandler<RequestPermissionCommand, RequestPermissionResult>
{
    public Task<RequestPermissionResult> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        // Create Permission entity from command object
        // Save to database
        // Return result

        throw new NotImplementedException();
    }
}
