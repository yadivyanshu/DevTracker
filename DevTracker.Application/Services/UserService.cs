using DevTracker.Domain.Entities;
using DevTracker.Infrastructure.Repositories.Interfaces;
using DevTracker.Application.DTOs;
using DevTracker.Application.Interfaces;
using AutoMapper;

namespace DevTracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => _mapper.Map<UserDTO>(u)).ToList();
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);  // Hash the password
            await _userRepository.AddUserAsync(user);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task UpdateUserAsync(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            _mapper.Map(updateUserDTO, user);
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}