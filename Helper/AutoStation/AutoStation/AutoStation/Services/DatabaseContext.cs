using AutoStation.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStation.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Buying> Buyings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buying>().HasOne(x => x.Schedule).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Schedule>().HasOne(x => x.To).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>().HasOne(x => x.From).WithMany().OnDelete(DeleteBehavior.Restrict);
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
