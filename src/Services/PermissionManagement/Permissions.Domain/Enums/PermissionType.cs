/*
   PermissionType enum represents the different types of permissions available in the application domain.

   Usage:
   1. Define properties of type PermissionType in relevant domain classes.
   2. Use PermissionType to specify the type of permission granted to users.
*/

namespace Permissions.Domain.Enums
{
    public enum PermissionType
    {
        User = 0,
        Staff = 1,
        Manager = 2,
        Director = 3,
        VP = 4,
        CLevel = 5,
        Admin = 6
    }
}
