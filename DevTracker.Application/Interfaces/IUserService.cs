using System.Threading.Tasks;
using DevTracker.Application.DTOs;
using DevTracker.Domain.Entities;

namespace DevTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        System.Threading.Tasks.Task UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);
        System.Threading.Tasks.Task DeleteUserAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}