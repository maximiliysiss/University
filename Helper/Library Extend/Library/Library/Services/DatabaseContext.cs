using JetBrains.Annotations;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    /// <summary>
    /// Подклчючение к БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        /// <summary>
        /// Пользователи
        /// </summary>
        /// <value></value>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Книги
        /// </summary>
        /// <value></value>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Создание модели
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// Сразу добавим админа
            /// </summary>
            /// <value></value>
            modelBuilder.Entity<User>().HasData(new User
            {
                ID = 1,
                Login = "admin",
                PasswordHash = CryptService.CreateMd5("admin"),
                UserRole = UserRole.Admin
            });
        }
    }
}
