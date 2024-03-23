/*
   This class defines a model for the body of the request when creating a permission in the Permissions Management API tests.
   It contains properties representing the details of the permission to be created.
*/


namespace PermissionsManagementAPI_Test.Models.RequestPermission
{
    public class PostPermissionBodyRequest
    {
        public string employeeId { get; set; }
        public string applicationName { get; set; }
        public int permissionType { get; set; }
        public bool permissionGranted { get; set; }
        public string permissionGrantedEmployeeId { get; set; }
    }
}
