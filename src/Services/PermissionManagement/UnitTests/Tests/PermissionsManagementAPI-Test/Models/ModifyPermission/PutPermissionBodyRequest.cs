/*
   This class defines a model for the request body when modifying a permission in the Permissions Management API tests.
*/

namespace PermissionsManagementAPI_Test.Models.ModifyPermission
{
    public class PutPermissionBodyRequest
    {
        public string id { get; set; }
        public string employeeId { get; set; }
        public string applicationName { get; set; }
        public int permissionType { get; set; }
        public bool permissionGranted { get; set; }
        public string permissionGrantedEmployeeId { get; set; }
    }
}
