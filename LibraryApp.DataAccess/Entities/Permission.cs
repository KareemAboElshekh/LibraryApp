using System.Collections.Generic;

namespace LibraryApp.DataAccess.Entities
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; } = "";
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}

