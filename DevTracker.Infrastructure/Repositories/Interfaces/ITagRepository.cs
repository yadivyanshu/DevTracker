using DevTracker.Domain.Entities;

namespace DevTracker.Infrastructure.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
        Task AddAsync(Tag tag);
        Task AddTaggingAsync(Tagging tagging);
    }
}