using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Wedding.Data.Entities;
using Wedding.Data.Mappings;

namespace Wedding.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccomodationOptions>()
                .Property(e => e.PhoneNumber)
                .HasConversion(new PhoneNumberConverter());
        }

        public DbSet<Party> Party { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public DbSet<AccomodationOptions> AccomodationOptions { get; set; }
        public DbSet<WeddingSetup> WeddingSetups { get; set; }
    }
}