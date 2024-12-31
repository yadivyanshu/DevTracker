namespace DevTracker.Domain.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; } // Foreign key to Project
        public Project Project { get; set; } // Navigation property
    }
}