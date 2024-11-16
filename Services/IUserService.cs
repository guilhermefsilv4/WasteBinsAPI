using WasteBinsAPI.Models;

namespace WasteBinsAPI.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int userId);
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task AddUserAsync(UserModel user);
    Task UpdateUserAsync(UserModel updatedUser);
    Task DeleteUserAsync(int userId);
}