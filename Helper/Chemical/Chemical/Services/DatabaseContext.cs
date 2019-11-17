using Chemical.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Chemical.Services
{
    public class DatabaseContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DatabaseContext(string dbString)
        {
            ConnectionString = dbString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { UserRole = UserRole.ProWorker, Login = "ProWorker", PasswordHash = CryptService.CreateMD5("ProWorker") },
                new User { UserRole = UserRole.Storage, Login = "Storage", PasswordHash = CryptService.CreateMD5("Storage") },
                new User { UserRole = UserRole.Techolog, Login = "Techolog", PasswordHash = CryptService.CreateMD5("Techolog") }
            );
        }
    }
}
