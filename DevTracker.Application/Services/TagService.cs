using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Application.DTOs;
using DevTracker.Application.Interfaces;
using AutoMapper;
using DevTracker.Domain.Enums;

namespace DevTracker.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITaskItemRepository _taskRepository;
        private readonly IBugRepository _bugRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper, ITaskItemRepository taskRepository, IBugRepository bugRepository, IFeatureRepository featureRepository)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _bugRepository = bugRepository;
            _featureRepository = featureRepository;
        }

        public async Task<List<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return tags.Select(t => _mapper.Map<TagDTO>(t)).ToList();
        }

        public async Task<TagDTO> GetTagByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<TagDTO>(tag);
        }

        public async Task CreateTagAsync(CreateTagDTO createTagDTO)
        {
            var tag = _mapper.Map<Tag>(createTagDTO);
            await _tagRepository.AddAsync(tag);
        }

        public async Task AssignTagToEntityAsync(int tagId, int entityId, EntityTypeEnum entityType)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            if (tag == null) throw new Exception("Tag not found.");

            // Validate Entity Existence Based on EntityTypeEnum
            switch (entityType)
            {
                case EntityTypeEnum.Task:
                    var taskExists = await _taskRepository.EntityExistsAsync(entityId);
                    if (!taskExists) throw new Exception("Task not found.");
                    break;
                case EntityTypeEnum.Bug:
                    var bugExists = await _bugRepository.EntityExistsAsync(entityId);
                    if (!bugExists) throw new Exception("Bug not found.");
                    break;
                case EntityTypeEnum.Feature:
                    var featureExists = await _featureRepository.EntityExistsAsync(entityId);
                    if (!featureExists) throw new Exception("Feature not found.");
                    break;
                default:
                    throw new Exception("Invalid entity type.");
            }

            // Assign the tag
            var tagging = new Tagging
            {
                TagId = tagId,
                EntityId = entityId,
                EntityType = entityType
            };
            await _tagRepository.AddTaggingAsync(tagging);
        }
    }
}