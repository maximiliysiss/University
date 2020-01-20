using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using test_angry_service.Models;

namespace test_angry_service.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<ExecutedLog> ExecutedLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var data = new List<Question>();

            using (var fileStream = new StreamReader(new FileStream(@"InitFolder\InitQuestions.txt", FileMode.Open)))
            {
                string line = string.Empty;
                int i = 1;
                while (!string.IsNullOrEmpty(line = fileStream.ReadLine()))
                    data.Add(new Question { Id = i++, Content = line });
            }

            modelBuilder.Entity<Question>().HasData(data);
        }
    }
}
