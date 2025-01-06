using DevTracker.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevTracker.Application.Interfaces
{
    public interface IFeatureService
    {
        Task<List<Feature>> GetAllFeatures();
        Task<Feature> GetFeatureByIdAsync(int id);
        Task<Feature> CreateFeature(Feature feature);
        Task<Feature> AddFeatureAsync(Feature feature);
        Task<Feature> UpdateFeature(int id, Feature feature);
        Task<bool> DeleteFeature(int id);
        Task<List<Feature>> GetFeaturesByProjectIdAsync(int projectId);
    }
}