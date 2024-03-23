using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
