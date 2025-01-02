using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DevTracker.Domain.Enums;

namespace DevTracker.Domain.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public FeatureStatus Status { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
        // public ICollection<Tagging> Taggings { get; set; } // To handle many-to-many relationship
        // public ICollection<Task> Tasks { get; set; }
        // public ICollection<Bug> Bugs { get; set; }
    }
}