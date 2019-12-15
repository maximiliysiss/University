using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Production.Services
{
    public class DatabaseContext : DbContext
    {
        public string ConnectionString { get; set; }
        private readonly bool isCustomCreate;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserInTeam> UserInTeams { get; set; }
        public DbSet<DayPlan> DayPlans { get; set; }

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
        /// Добавление пользователей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            for (int i = 1; i < 11; i++)
                modelBuilder.Entity<Team>().HasData(new Team { ID = i, Name = $"Бригада {i}" });
            for (int i = 1; i < 11; i++)
                modelBuilder.Entity<User>().HasData(new User { ID = i, Login = $"Brigadir{i}", PasswordHash = CryptService.CreateMD5($"Brigadir{i}"), TeamId = i, UserRole = UserRole.Brigadir });
            for (int i = 11; i < 212; i++)
                modelBuilder.Entity<User>().HasData(new User { ID = i, Login = $"Worker{i}", PasswordHash = CryptService.CreateMD5($"Worker{i}"), UserRole = UserRole.Worker });
            for (int i = 11; i < 212; i++)
                modelBuilder.Entity<UserInTeam>().HasData(new UserInTeam { ID = i - 10, TeamId = (i - 10) % 10 + 1, WorkerId = i });

            modelBuilder.Entity<User>().HasData(
                new User { ID = 212, UserRole = UserRole.Admin, Login = "Admin", PasswordHash = CryptService.CreateMD5("Admin") },
                new User { ID = 213, UserRole = UserRole.Director, Login = "Director", PasswordHash = CryptService.CreateMD5("Director") }
            );

            modelBuilder.Entity<UserInTeam>().HasOne(x => x.Worker).WithMany().OnDelete(DeleteBehavior.Restrict);
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
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-06B1B3A\SQLEXPRESS;Initial Catalog=production.api;Integrated Security=True");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
