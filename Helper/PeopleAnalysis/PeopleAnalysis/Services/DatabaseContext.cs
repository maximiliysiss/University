using Microsoft.EntityFrameworkCore;
using PeopleAnalysis.Models;

namespace PeopleAnalysis.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
    }
}
