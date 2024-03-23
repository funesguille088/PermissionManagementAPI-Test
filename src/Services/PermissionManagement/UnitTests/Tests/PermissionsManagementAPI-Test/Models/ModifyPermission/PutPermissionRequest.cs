/*
   This class defines a model for the request when modifying a permission in the Permissions Management API tests.
   It contains a property to hold the body of the request.
*/

namespace PermissionsManagementAPI_Test.Models.ModifyPermission
{
    public class PutPermissionRequest
    {
        public PutPermissionBodyRequest permission { get; set; }
    }
}
