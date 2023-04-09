using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wedding.Data.Entities;

namespace Wedding.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Party> Party { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<DietaryRequirements> DietaryRequirements { get; set; }
    }
}