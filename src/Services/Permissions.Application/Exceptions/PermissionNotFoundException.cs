/*
   The PermissionNotFoundException class represents an exception that is thrown when a permission with a specific ID is not found.

   Usage:
   1. Throw this exception when attempting to retrieve a permission by its ID from a repository or database, but the permission does not exist.
   2. Catch this exception in the appropriate layer of the application to handle the scenario where a requested permission is not found.

   Constructor:
   - PermissionNotFoundException(Guid id): Initializes a new instance of the PermissionNotFoundException class with the ID of the permission that was not found.

   Properties:
   - Id: Gets the ID of the permission that was not found.

   Base Class:
   - NotFoundException: This exception class is derived from the NotFoundException base class provided by the BuildingBlocks.Exceptions namespace.

   Note: The base class NotFoundException provides common functionality for handling not found exceptions, such as specifying the type and ID of the entity that was not found.
*/

using BuildingBlocks.Exceptions;

namespace Permissions.Application.Exceptions;
public class PermissionNotFoundException : NotFoundException
{
    public PermissionNotFoundException(Guid id) : base("Permission", id)
    {

    }
}
