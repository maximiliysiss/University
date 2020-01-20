using Microsoft.EntityFrameworkCore;
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
    }
}
