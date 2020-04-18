using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TranslateChatter.Models;

namespace TranslateChatter.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Property(x => x.LanguageId).HasDefaultValue(1);
            builder.Entity<Language>().ToTable("Languages").HasData(new[] {
                new Language{ Id = 1, Code = "en", Name = "English", UICode = "en-US" },
                new Language{ Id = 2, Code = "ru", Name = "Русский", UICode = "ru-RU" },
            });
        }
    }
}
