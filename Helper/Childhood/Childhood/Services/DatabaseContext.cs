using Childhood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Childhood.Services
{
    public class DatabaseContext : DbContext
    {
        private readonly string connectionString;
        private readonly bool isCustom;

        public DatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
            this.isCustom = true;
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (isCustom)
                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Login = "Director", FIO = "Director", Phone = "000", UserType = UserType.Director, PasswordHash = CryptService.CreateMD5("Director") },
                new User { ID = 2, Login = "Tutor", FIO = "Tutor", Phone = "000", UserType = UserType.Tutor, PasswordHash = CryptService.CreateMD5("Tutor") }
            );

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<AddActions> AddActions { get; set; }
        public DbSet<ChildCheck> ChildChecks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Information> Information { get; set; }
    }

    /// <summary>
    /// Для создания миграций
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-06B1B3A\SQLEXPRESS;Initial Catalog=childhood.api;Integrated Security=True");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
