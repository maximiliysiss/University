using Bank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Services
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
                optionsBuilder.UseSqlServer(ConnectionString).UseLazyLoadingProxies();
        }

        /// <summary>
        /// Таблицы
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<PrivateAccount> PrivateAccounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ConvertCurrency> ConvertCurrencies { get; set; }

        /// <summary>
        /// Добавление пользователей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Address = "Admin", DocumentCode = "Admin", FIO = "Admin", Login = "Admin", PasswordHash = CryptService.CreateMD5("Admin"), Role = Role.Admin },
                new User { Id = 2, Address = "Client", DocumentCode = "Client", FIO = "Client", Login = "Client", PasswordHash = CryptService.CreateMD5("Client"), Role = Role.Client },
                new User { Id = 3, Address = "Director", DocumentCode = "Director", FIO = "Director", Login = "Director", PasswordHash = CryptService.CreateMD5("Director"), Role = Role.Director },
                new User { Id = 4, Address = "Worker", DocumentCode = "Worker", FIO = "Worker", Login = "Worker", PasswordHash = CryptService.CreateMD5("Worker"), Role = Role.Worker }
            );

            modelBuilder.Entity<ConvertCurrency>().HasOne(x => x.CurrencyTo).WithMany().OnDelete(DeleteBehavior.Restrict);
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
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-N9HBPPP\SQLEXPRESS;Initial Catalog=bank;Integrated Security=True");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
