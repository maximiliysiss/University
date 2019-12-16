using Flats.Models;
using Microsoft.EntityFrameworkCore;

namespace Flats.Services
{
    /// <summary>
    /// БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        // Таблицы
        public DbSet<User> Users { get; set; }
        public DbSet<Realty> Realties { get; set; }

        /// <summary>
        /// Создание моделей
        /// + Добавим пару пользователей сразу
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Login = "Admin", PasswordHash = CryptService.GetMd5Hash("Admin"), UserType = UserType.Admin },
                new User { ID = 2, Login = "User", PasswordHash = CryptService.GetMd5Hash("User"), UserType = UserType.User }
            );
        }
    }
}
