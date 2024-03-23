/*
   This class defines a model for the response containing permissions data used in the Permissions Management API tests.
   It includes a property to hold the permissions data retrieved from the API.
*/
namespace PermissionsManagementAPI_Test.Models.GetPermissionsModel
{
    public class GetPermissionResponse
    {
        public GetPermissions permissions { get; set; }
    }
}
