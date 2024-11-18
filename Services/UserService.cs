using WasteBinsAPI.Data.Repository;
using WasteBinsAPI.Models;

namespace WasteBinsAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public IEnumerable<UserModel> GetAllReference(int lastReference, int size) =>
        _userRepository.GetAllReference(lastReference, size);

    public async Task<UserModel?> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task AddUserAsync(UserModel user)
    {
        user.Password = _passwordHasher.HashPassword(user.Password);
        
        await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(UserModel updatedUser)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(updatedUser.UserId);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        existingUser.Username = updatedUser.Username;
        existingUser.Role = updatedUser.Role;
        
        if (!string.IsNullOrEmpty(updatedUser.Password))
        {
            existingUser.Password = _passwordHasher.HashPassword(updatedUser.Password);
        }

        await _userRepository.UpdateUserAsync(existingUser);
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }
}