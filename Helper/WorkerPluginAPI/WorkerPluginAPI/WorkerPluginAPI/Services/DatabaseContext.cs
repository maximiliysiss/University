using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WorkerPluginAPI.Models;

namespace WorkerPluginAPI.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerCheck> WorkerChecks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Worker>().HasData(
                new Worker { ID = 1, Login = "Worker", PasswordHash = CryptService.CreateMD5("Worker"), WorkerType = WorkerType.Worker },
                new Worker { ID = 2, Login = "Admin", PasswordHash = CryptService.CreateMD5("Admin"), WorkerType = WorkerType.Admin }
            );
        }
    }
}
