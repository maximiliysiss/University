using Microsoft.EntityFrameworkCore;
using PeopleAnalysis.Models;

namespace PeopleAnalysis.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
