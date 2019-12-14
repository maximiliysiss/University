﻿using Dapper;
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

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, UserRole = UserRole.Brigadir, Login = "Brigadir", PasswordHash = CryptService.CreateMD5("Brigadir") },
                new User { ID = 2, UserRole = UserRole.Admin, Login = "Admin", PasswordHash = CryptService.CreateMD5("Admin") },
                new User { ID = 3, UserRole = UserRole.Director, Login = "Director", PasswordHash = CryptService.CreateMD5("Director") },
                new User { ID = 4, UserRole = UserRole.Worker, Login = "Worker", PasswordHash = CryptService.CreateMD5("Worker") },
                new User { ID = 5, UserRole = UserRole.Worker, Login = "Worker1", PasswordHash = CryptService.CreateMD5("Worker1") }
            );

            modelBuilder.Entity<Team>().HasData(new Team { ID = 1, Name = "Бригада" });

            modelBuilder.Entity<UserInTeam>().HasOne(x => x.Worker).WithMany().OnDelete(DeleteBehavior.Restrict);
        }

        public bool Execute(string script, object args = null)
        {
            try
            {
                using (var connection = Database.GetDbConnection())
                {
                    if (args != null)
                        connection.Execute(script, args);
                    else
                        connection.Execute(script);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
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
