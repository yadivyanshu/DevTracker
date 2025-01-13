using DevTracker.Domain.Entities;
namespace DevTracker.Infrastructure.Repositories.Interfaces{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    }
}