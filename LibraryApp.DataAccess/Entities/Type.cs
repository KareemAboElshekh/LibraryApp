using System.Collections.Generic;

namespace LibraryApp.DataAccess.Entities
{
    public class Type
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;

        // علاقة واحد-لكثير: كل Type لديه عدة Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
