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
