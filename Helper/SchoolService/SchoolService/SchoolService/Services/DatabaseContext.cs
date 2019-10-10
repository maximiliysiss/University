using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SchoolService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Child> Children { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RiskGroup> RiskGroups { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<ChildInRiskGroup> ChildInRiskGroups { get; set; }
    }
}
