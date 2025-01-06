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

            CreateMap<TaskItem, TaskItemDTO>().ReverseMap();
            CreateMap<TaskItem, CreateTaskItemDTO>().ReverseMap();
            CreateMap<TaskItem, UpdateTaskItemDTO>().ReverseMap();

            CreateMap<Bug, BugDTO>().ReverseMap();
            CreateMap<Bug, CreateBugDTO>().ReverseMap();
            CreateMap<Bug, UpdateBugDTO>().ReverseMap();

            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Tag, CreateTagDTO>().ReverseMap();
        }
    }
}