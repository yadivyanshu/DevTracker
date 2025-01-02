using DevTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Infrastructure.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Feature> Features { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships if needed
            modelBuilder.Entity<Project>()
                        .HasMany(p => p.Features)
                        .WithOne()
                        .HasForeignKey(f => f.ProjectId);

            // Add a unique index
            modelBuilder.Entity<Project>()
                        .HasIndex(p => p.Name)
                        .IsUnique();
        }
    }
}