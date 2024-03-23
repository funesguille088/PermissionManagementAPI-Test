/*
   This class defines a model for the response when modifying a permission in the Permissions Management API tests.
   It contains a property indicating whether the modification was successful.
*/


namespace PermissionsManagementAPI_Test.Models.ModifyPermission
{
    public class PutPermissionResponse
    {
        public bool isSuccess { get; set; }
    }
}
