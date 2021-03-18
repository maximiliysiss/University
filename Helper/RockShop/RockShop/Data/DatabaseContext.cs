using Microsoft.EntityFrameworkCore;
using RockShop.Data.Models;
using RockShop.Services;

namespace RockShop.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly ICryptService cryptService;

        public DatabaseContext(DbContextOptions options, ICryptService cryptService) : base(options)
        {
            this.cryptService = cryptService;
        }

        public DbSet<Rock> Rocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(x =>
            {
                x.HasData(new User { Id = 1, Login = "admin", PasswordHash = cryptService.CreateMD5("admin") });
            });
        }
    }
}
