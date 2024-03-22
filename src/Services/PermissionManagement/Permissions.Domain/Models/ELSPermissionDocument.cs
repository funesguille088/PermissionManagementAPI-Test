namespace Permissions.Domain.Models
{
    public class ELSPermissionDocument
    {
        public string permissionid { get; set; }
        public string employeeid { get; set; }
        public string applicationname { get; set; }
        public string permissiontype { get; set; }
        public bool permissiongranted { get; set; }
        public string permissiongrantedemployeeId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime LastModified { get; set; }
        //public string LastModifiedBy { get; set; }
    }
}
