namespace LibraryApp.DataAccess.Entities
{
    public class RolePermission
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public Role Role { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}

