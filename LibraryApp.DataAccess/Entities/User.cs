using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.DataAccess.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;

        // FK إلى Type
        public int TypeId { get; set; }
        public Type Type { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;


        // ← هذه الخاصّية الجديدة
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // (اختياري) يمكنك إضافة اختصار لجلب الأدوار مباشرةً
        public IEnumerable<Role> Roles
            => UserRoles.Select(ur => ur.Role);
    

        // علاقة المستخدم بالحجوزات
        public ICollection<Reservation> Reservations { get; set; }
            = new List<Reservation>();
    }
}
