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
    public class DatabaseContext : DbContext
    {
        private readonly bool isContaniner;
        private readonly string connectionString;

        protected DatabaseContext(string connectionString)
        {
            isContaniner = true;
            this.connectionString = connectionString;
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            isContaniner = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (isContaniner)
                optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rent>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<Rent> Rents { get; set; }
    }


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
