/*
   This class defines a model for the permissions data used in the Permissions Management API tests.
   It includes properties to hold information about pagination and a list of permission data items.
*/

namespace PermissionsManagementAPI_Test.Models.GetPermissionsModel
{
    public class GetPermissions
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public List<GetPermissionData> data { get; set; }
    }
}
