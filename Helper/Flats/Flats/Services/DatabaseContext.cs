using Flats.Models;
using Microsoft.EntityFrameworkCore;

namespace Flats.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Realty> Realties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Login = "Admin", PasswordHash = CryptService.GetMd5Hash("Admin"), UserType = UserType.Admin },
                new User { ID = 1, Login = "User", PasswordHash = CryptService.GetMd5Hash("User"), UserType = UserType.User }
            );
        }
    }
}
