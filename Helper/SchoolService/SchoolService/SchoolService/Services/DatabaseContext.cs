using JetBrains.Annotations;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using SchoolService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Data;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mark>().HasOne(x => x.Teacher).WithMany(x => x.Marks).HasForeignKey(x => x.TeacherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mark>().HasOne(x => x.Child).WithMany(x => x.Marks).HasForeignKey(x => x.ChildId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>().HasOne(x => x.Class).WithMany().HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(new User
            {
                ID = 1,
                UserType = UserType.Admin,
                Login = "admin",
                PasswordHash = CryptService.CreateMD5("admin"),
                Name = "admin",
                Surname = "admin",
                SecondName = "admin",
                Birthday = DateTime.Today
            },
            new User
            {
                ID = 2,
                UserType = UserType.JobTeacher,
                Login = "jobteacher",
                PasswordHash = CryptService.CreateMD5("jobteacher"),
                Name = "jobteacher",
                Surname = "jobteacher",
                SecondName = "jobteacher",
                Birthday = DateTime.Today
            },
            new User
            {
                ID = 3,
                UserType = UserType.KnowledgeTeacher,
                Login = "knowledgeteacher",
                PasswordHash = CryptService.CreateMD5("knowledgeteacher"),
                Name = "knowledgeteacher",
                Surname = "knowledgeteacher",
                SecondName = "knowledgeteacher",
                Birthday = DateTime.Today
            },
            new User
            {
                ID = 4,
                UserType = UserType.Social,
                Login = "social",
                PasswordHash = CryptService.CreateMD5("social"),
                Name = "social",
                Surname = "social",
                SecondName = "social",
                Birthday = DateTime.Today
            });

            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                ID = 5,
                UserType = UserType.Teacher,
                Login = "teacher",
                PasswordHash = CryptService.CreateMD5("teacher"),
                Name = "teacher",
                Surname = "teacher",
                SecondName = "teacher",
                Birthday = DateTime.Today
            });

            modelBuilder.Entity<Child>().HasData(new Child
            {
                ID = 6,
                UserType = UserType.Student,
                Login = "child",
                PasswordHash = CryptService.CreateMD5("child"),
                Name = "child",
                Surname = "child",
                SecondName = "child",
                Birthday = DateTime.Today
            });
        }
    }
}
