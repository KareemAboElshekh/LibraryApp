using System.Collections.Generic;

namespace LibraryApp.DataAccess.Entities
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = "";
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
