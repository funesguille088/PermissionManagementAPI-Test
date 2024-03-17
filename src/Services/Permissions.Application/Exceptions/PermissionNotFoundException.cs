using BuildingBlocks.Exceptions;

namespace Permissions.Application.Exceptions;
public class PermissionNotFoundException : NotFoundException
{
    public PermissionNotFoundException(Guid id) : base("Permission", id)
    {

    }
}
