using DevTracker.Application.Interfaces;
using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;

namespace DevTracker.Application.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task<List<Feature>> GetAllFeatures()
        {
            return await _featureRepository.GetAllAsync();
        }
        
        public async Task<Feature> GetFeatureByIdAsync(int id)
        {
            return await _featureRepository.GetByIdAsync(id);
        }

        public async Task<Feature> CreateFeature(Feature feature)
        {
            feature.CreatedAt = feature.UpdatedAt = DateTime.UtcNow;
            return await _featureRepository.AddAsync(feature);
        }
        public async Task<Feature> AddFeatureAsync(Feature feature)
        {
            return await _featureRepository.AddAsync(feature);
            // return await _featureRepository.SaveChangesAsync() > 0;
        }

        public async Task<Feature> UpdateFeature(int id, Feature feature)
        {
            var existingFeature = await _featureRepository.GetByIdAsync(id);
            if (existingFeature != null)
            {
                existingFeature.Title = feature.Title;
                existingFeature.Description = feature.Description;
                existingFeature.Status = feature.Status;
                existingFeature.UpdatedAt = DateTime.UtcNow;

                return await _featureRepository.UpdateAsync(existingFeature);
            }
            return null;
        }

        public async Task<bool> DeleteFeature(int id)
        {
            var feature = await _featureRepository.GetByIdAsync(id);
            if (feature != null)
            {
                return await _featureRepository.DeleteAsync(feature);
            }
            return false;
        }
    }
}