using AutoMapper;
using DevTracker.Application.DTOs;
using DevTracker.Domain.Entities;
// using DevTracker.Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Create mappings between domain entities and DTOs
        CreateMap<Project, ProjectDTO>();
    }
}