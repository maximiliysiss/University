using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleAnalysis.Models;

namespace PeopleAnalysis.Services
{
    public class AuthContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AuthContext(DbContextOptions options) : base(options)
        {
        }
    }
}
