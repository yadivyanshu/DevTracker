// namespace DevTracker.Domain.Entities
// {
//     public class Project
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public string Description { get; set; }
//         public List<Feature> Features { get; set; } // A project has many features
//     }
// }

namespace DevTracker.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}