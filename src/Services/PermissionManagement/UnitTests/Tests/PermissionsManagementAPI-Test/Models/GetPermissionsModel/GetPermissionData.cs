/*
   This class defines a model for permission data used in the Permissions Management API tests.
   It includes properties such as ID, employee ID, application name, permission type, permission granted status,
   and the ID of the employee who granted the permission.
*/
namespace PermissionsManagementAPI_Test.Models.GetPermissionsModel
{
    public class GetPermissionData
    {
        public string id { get; set; }
        public string employeeId { get; set; }
        public string applicationName { get; set; }
        public int permissionType { get; set; }
        public bool permissionGranted { get; set; }
        public string permissionGrantedEmployeeId { get; set; }
    }
}
