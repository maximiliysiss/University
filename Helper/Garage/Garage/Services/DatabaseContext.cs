using Garage.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Services
{
    /// <summary>
    /// БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Запуск через контейнер или нет
        /// </summary>
        private readonly bool isContaniner;
        /// <summary>
        /// Строка подключения
        /// </summary>
        private readonly string connectionString;

        public DatabaseContext(string dbString)
        {
            isContaniner = true;
            this.connectionString = dbString;
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Создание подключения
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (isContaniner)
                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        }

        /// <summary>
        /// Создание моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rent>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Login = "User", PasswordHash = CryptService.CreateMD5("User"), UserRole = UserRole.User },
                new User { ID = 2, Login = "Home", PasswordHash = CryptService.CreateMD5("Home"), UserRole = UserRole.HomeKeeper }
            );
        }

        /// <summary>
        /// Таблицы
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<Rent> Rents { get; set; }
    }

    /// <summary>
    /// Для создания миграций
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-06B1B3A\SQLEXPRESS;Initial Catalog=garage.api;Integrated Security=True");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
