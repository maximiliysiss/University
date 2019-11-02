using AutoStation.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStation.Services
{
    /// <summary>
    /// БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Расписание
        /// </summary>
        /// <value></value>
        public DbSet<Schedule> Schedules { get; set; }
        /// <summary>
        /// Точки
        /// </summary>
        /// <value></value>
        public DbSet<Point> Points { get; set; }
        /// <summary>
        /// Покупки
        /// </summary>
        /// <value></value>
        public DbSet<Buying> Buyings { get; set; }
        /// <summary>
        /// Пользователи
        /// </summary>
        /// <value></value>
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// Удалим лишниие зависимости, чтобы не было циклов
            /// </summary>
            /// <typeparam name="Buying"></typeparam>
            /// <returns></returns>
            modelBuilder.Entity<Buying>().HasOne(x => x.Schedule).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Schedule>().HasOne(x => x.To).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>().HasOne(x => x.From).WithMany().OnDelete(DeleteBehavior.Restrict);
            /// <summary>
            /// Добавим пользователя
            /// </summary>
            /// <value></value>
            modelBuilder.Entity<User>().HasData(new User
            {
                ID = 1,
                Login = "admin",
                PasswordHash = CryptService.CreateMd5("admin")
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
