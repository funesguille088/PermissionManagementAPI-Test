/*
   This class defines a model for the request when creating a permission in the Permissions Management API tests.
   It contains a property representing the body of the request, which includes the details of the permission to be created.
*/

namespace PermissionsManagementAPI_Test.Models.RequestPermission
{
    public class PostPermissionRequest
    {
        public PostPermissionBodyRequest permission { get; set; }
    }
}
