
using DevTracker.Application.DTOs;

namespace DevTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);
        Task DeleteUserAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}