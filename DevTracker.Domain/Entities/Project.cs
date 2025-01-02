namespace DevTracker.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);

        // Navigation property for related Features
        public ICollection<Feature> Features { get; set; } = new List<Feature>();
    }
}