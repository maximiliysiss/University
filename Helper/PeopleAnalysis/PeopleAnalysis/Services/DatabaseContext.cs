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
        public DbSet<Result> Results { get; set; }
        public DbSet<AnalysObject> AnalysObjects { get; set; }
        public DbSet<ResultObject> ResultObjects { get; set; }
    }
}
