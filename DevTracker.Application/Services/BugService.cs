using AutoMapper;
using DevTracker.Application.DTOs;
using DevTracker.Application.Interfaces;
using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;

namespace DevTracker.Application.Services
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _bugRepository;
        private readonly IMapper _mapper;

        public BugService(IBugRepository bugRepository, IMapper mapper)
        {
            _bugRepository = bugRepository;
            _mapper = mapper;
        }

        public async Task<BugDTO> GetByIdAsync(int id)
        {
            var bug = await _bugRepository.GetByIdAsync(id);
            return _mapper.Map<BugDTO>(bug);
        }

        public async Task<List<BugDTO>> GetByFeatureIdAsync(int featureId)
        {
            var bugs = await _bugRepository.GetByFeatureIdAsync(featureId);
            return bugs.Select(b => _mapper.Map<BugDTO>(b)).ToList();
        }

        public async Task<List<BugDTO>> GetAllAsync()
        {
            var bugs = await _bugRepository.GetAllAsync();
            return bugs.Select(b => _mapper.Map<BugDTO>(b)).ToList();
        }

        public async Task CreateAsync(CreateBugDTO createBugDTO)
        {
            var bug = _mapper.Map<Bug>(createBugDTO);
            await _bugRepository.AddAsync(bug);
        }

        public async Task UpdateAsync(int id, UpdateBugDTO updateBugDTO)
        {
            var bug = await _bugRepository.GetByIdAsync(id);
            bug.UpdatedAt = DateTime.UtcNow;
            _mapper.Map(updateBugDTO, bug);
            await _bugRepository.UpdateAsync(bug);
        }

        public async Task DeleteAsync(int id)
        {
            await _bugRepository.DeleteAsync(id);
        }
    }
}