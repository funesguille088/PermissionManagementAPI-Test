using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
