using System.Threading.Tasks;
using DevTracker.Application.DTOs;
using DevTracker.Domain.Entities;

namespace DevTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);
        Task DeleteUserAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    }
}