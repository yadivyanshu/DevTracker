using Microsoft.EntityFrameworkCore;
using DevTracker.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DevTracker.Infrastructure.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Project> Projects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
             _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configurations if required
        }
    }
}