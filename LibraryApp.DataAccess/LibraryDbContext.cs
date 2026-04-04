using Microsoft.EntityFrameworkCore;
using LibraryApp.DataAccess.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace LibraryApp.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public DbSet<Type> Types => Set<Type>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<ReservationItem> ReservationItems => Set<ReservationItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(LibraryDbContext).Assembly
            );
        }
    }
}

