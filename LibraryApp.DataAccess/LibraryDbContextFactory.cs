
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryApp.DataAccess
{
    /// <summary>
    /// Factory design-time لإنشاء LibraryDbContext عند استخدام أدوات EF Core.
    /// </summary>
    public class LibraryDbContextFactory
        : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();

            // ضع هنا سلسلة الاتصال المناسبة لمشروعك:
           optionsBuilder.UseSqlServer(
           "Server=DESKTOP-CIJQFAK;" +
           "Database=LibraryAppDb;" +        //<— اسم القاعدة الجديدة
           "User Id=sa;" +
           "Password=1234;" +
           "TrustServerCertificate=True;"
           );


            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}
