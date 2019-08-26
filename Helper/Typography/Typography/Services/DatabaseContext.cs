using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typography.Models;

namespace Typography.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        public DbSet<Paper> Papers { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<PostOfficer> PostOfficers { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Models.Typography> Typographies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString).UseLazyLoadingProxies();
        }
    }
}
