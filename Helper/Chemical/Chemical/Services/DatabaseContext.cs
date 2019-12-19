using Chemical.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Services
{
    /// <summary>
    /// Подключение к БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public string ConnectionString { get; set; }
        private bool isCustomCreate;

        /// <summary>
        /// Для создания в контейнере
        /// </summary>
        /// <param name="dbString"></param>
        public DatabaseContext(string dbString)
        {
            ConnectionString = dbString;
            isCustomCreate = true;
        }

        /// <summary>
        /// Для генерации миграций
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Конфигурация
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (isCustomCreate)
                optionsBuilder.UseSqlServer(ConnectionString);
        }

        /// <summary>
        /// Таблицы
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<MaterialInStock> MaterialInStocks { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<DetailMaterial> DetailMaterials { get; set; }

        /// <summary>
        /// Добавление пользователей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, UserRole = UserRole.ProWorker, Login = "ProWorker", PasswordHash = CryptService.CreateMD5("ProWorker") },
                new User { ID = 2, UserRole = UserRole.Storage, Login = "Storage", PasswordHash = CryptService.CreateMD5("Storage") },
                new User { ID = 3, UserRole = UserRole.Techolog, Login = "Techolog", PasswordHash = CryptService.CreateMD5("Techolog") }
            );
        }
    }

    /// <summary>
    /// Для миграций
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-06B1B3A\SQLEXPRESS;Initial Catalog=chemical.api;Integrated Security=True");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
