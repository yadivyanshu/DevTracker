using DevTracker.API.DTOs;
using DevTracker.Application.Interfaces;
using DevTracker.Domain.Entities;
using DevTracker.Domain.Enums;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Infrastructure.DataContext;

namespace DevTracker.Application.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _repository;
        private readonly IUserRepository _userRepository;

        public DiscussionService(IDiscussionRepository repository, IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task<DiscussionDTO> CreateAsync(CreateDiscussionDTO dto)
        {
            var user =  await _userRepository.GetUserByUsernameAsync(dto.CreatedByUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var discussion = new Discussion
            {
                EntityId = dto.EntityId,
                EntityType = dto.EntityType,
                CreatedBy = user,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentDiscussionId = dto.ParentDiscussionId,
                Content = dto.Content
            };

            var created = await _repository.CreateAsync(discussion);

            return new DiscussionDTO
            {
                Id = created.Id,
                EntityId = created.EntityId,
                EntityType = created.EntityType.ToString(),
                CreatedBy = created.CreatedBy.Username,
                Content = created.Content,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<DiscussionDTO> GetByIdAsync(int id)
        {
            var discussion = await _repository.GetByIdAsync(id);
            return discussion != null
                ? new DiscussionDTO
                {
                    Id = discussion.Id,
                    Content = discussion.Content,
                    CreatedAt = discussion.CreatedAt,
                    CreatedBy = discussion.CreatedBy.Username,
                    EntityType = discussion.EntityType.ToString(),
                    EntityId = discussion.EntityId,
                    // Mentions = discussion.Mentions.Select(m => m.Username).ToList(),
                    Reactions = discussion.Reactions.Select(r => r.ReactionType.ToString()).ToList(),
                    ParentDiscussionId = discussion.ParentDiscussionId,
                    UpdatedAt = discussion.UpdatedAt
                }
                : null;
        }

        public async Task<List<DiscussionDTO>> GetByEntityAsync(int entityId, EntityTypeEnum entityType)
{
    // Fetch discussions with navigation properties eagerly loaded
    var discussions = await _repository.GetByEntityAsync(entityId, entityType);

    // Map discussions to DTOs, adding null checks
    return discussions.Select(d => new DiscussionDTO
    {
        Id = d.Id,
        EntityId = d.EntityId,
        EntityType = d.EntityType.ToString(),
        Content = d.Content,
        CreatedBy = d.CreatedBy?.Username ?? "Unknown", // Handle null CreatedBy
        CreatedAt = d.CreatedAt,
        UpdatedAt = d.UpdatedAt,
        ParentDiscussionId = d.ParentDiscussionId,
        // Mentions = d.Mentions?.Select(m => new MentionDTO
        // {
        //     Id = m.Id,
        //     MentionedUser = m.MentionedUser?.Username ?? "Unknown" // Handle null MentionedUser
        // }).ToList(),
        // Reactions = d.Reactions?.Select(r => new ReactionDTO
        // {
        //     Id = r.Id,
        //     ReactionType = r.ReactionType,
        //     CreatedBy = r.CreatedBy?.Username ?? "Unknown" // Handle null CreatedBy
        // }).ToList()
    }).ToList();
}

        public async Task UpdateAsync(int id, UpdateDiscussionDTO dto)
        {
            var discussion = await _repository.GetByIdAsync(id);
            if (discussion != null)
            {
                discussion.Content = dto.Content;
                discussion.UpdatedAt = DateTime.UtcNow;
                await _repository.UpdateAsync(discussion);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}