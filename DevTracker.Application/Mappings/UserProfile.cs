using AutoMapper;
using DevTracker.Domain.Entities;
using DevTracker.Application.DTOs;

namespace DevTracker.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, UserDTO>();

            // CreateMap<FeatureTask, TaskDTO>().ReverseMap();
            // CreateMap<FeatureTask, CreateTaskDTO>().ReverseMap();
            // CreateMap<FeatureTask, UpdateTaskDTO>().ReverseMap();
        }
    }
}