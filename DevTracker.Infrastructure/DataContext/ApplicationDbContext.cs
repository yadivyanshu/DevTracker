using DevTracker.Domain.Entities;
using DevTracker.Domain.Enums;
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
        // public DbSet<Task> Tasks { get; set; }
        // public DbSet<Bug> Bugs { get; set; }
        // public DbSet<Tagging> Taggings { get; set; }
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

            modelBuilder.Entity<Feature>()
                        .HasIndex(p => p.Title)
                        .IsUnique();

            // Fixing column in Feature
            modelBuilder.Entity<Feature>()
            .HasOne(f => f.Project)
            .WithMany(p => p.Features)
            .HasForeignKey(f => f.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Feature>()
                        .Property(f => f.Status)
                        .HasConversion(
                            v => v.ToString(), // Enum to string for database
                            v => (FeatureStatus)Enum.Parse(typeof(FeatureStatus), v) // String to enum for code
                        );
        }
    }
}