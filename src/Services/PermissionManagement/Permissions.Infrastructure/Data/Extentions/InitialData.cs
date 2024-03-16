
using Permissions.Domain.Enums;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;

namespace Permissions.Infrastructure.Data.Extentions
{
    public class InitialData
    {
        public static IEnumerable<Employee> Employees =>
            new List<Employee>
            {
                Employee.Create(EmployeeId.Of(new Guid("aed86f38-1254-4052-b6b6-8fe3d148dfa3")), "Pedro", "Lopez", "pedro.lopez@N5.com","plopez"),
                Employee.Create(EmployeeId.Of(new Guid("bb847c3d-9577-49f3-bcf0-f469b99fe64c")), "Luis", "Gomez", "luis.gomez@N5.com","lgomez"),
                Employee.Create(EmployeeId.Of(new Guid("2b9e0651-5668-4f17-acc3-c0b54e628eb7")), "Gaston", "Fernandez", "gaston.fernandez@N5.com","gfernandez"),
                Employee.Create(EmployeeId.Of(new Guid("788c3650-d9fc-4e89-9712-d4ee093298af")), "Guillermo", "Funes", "guillermo.funes@N5.com","gfunes"),
                Employee.Create(EmployeeId.Of(new Guid("741f06d2-258d-4b4e-af61-c1b5fe7a129f")), "Catalina", "Marinero", "catalina.marinero@N5.com","cmarinero"),
                Employee.Create(EmployeeId.Of(new Guid("397d6c28-f8c9-4788-8640-dd7590748998")), "Generic", "User", "admin@N5.com","admin")
            };
        public static IEnumerable<Permission> Permissions
        {
            get
            {
                var permission1 = Permission.Create(
                    PermissionId.Of(Guid.NewGuid()),
                    EmployeeId.Of(new Guid("741f06d2-258d-4b4e-af61-c1b5fe7a129f")),
                    "Salesforce",
                    PermissionType.User,
                    EmployeeId.Of(new Guid("397d6c28-f8c9-4788-8640-dd7590748998")));

                var permission2 = Permission.Create(
                    PermissionId.Of(Guid.NewGuid()),
                    EmployeeId.Of(new Guid("788c3650-d9fc-4e89-9712-d4ee093298af")),
                    "Salesforce",
                    PermissionType.Admin,
                    EmployeeId.Of(new Guid("397d6c28-f8c9-4788-8640-dd7590748998")));

                var permission3 = Permission.Create(
                    PermissionId.Of(Guid.NewGuid()),
                    EmployeeId.Of(new Guid("2b9e0651-5668-4f17-acc3-c0b54e628eb7")),
                    "Salesforce",
                    PermissionType.CLevel,
                    EmployeeId.Of(new Guid("397d6c28-f8c9-4788-8640-dd7590748998")));

                var permission4 = Permission.Create(
                    PermissionId.Of(Guid.NewGuid()),
                    EmployeeId.Of(new Guid("bb847c3d-9577-49f3-bcf0-f469b99fe64c")),
                    "Salesforce",
                    PermissionType.Manager,
                    EmployeeId.Of(new Guid("397d6c28-f8c9-4788-8640-dd7590748998")));

                var permission5 = Permission.Create(
                    PermissionId.Of(Guid.NewGuid()),
                    EmployeeId.Of(new Guid("aed86f38-1254-4052-b6b6-8fe3d148dfa3")),
                    "Salesforce",
                    PermissionType.Director,
                    EmployeeId.Of(new Guid("397d6c28-f8c9-4788-8640-dd7590748998")));

                return new List<Permission> { permission1 , permission2, permission3, permission4, permission5 };
            }
        }
    }
}
