using WasteBinsAPI.Models;

namespace WasteBinsAPI.Data.Repository;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    IEnumerable<UserModel> GetAllReference(int lastReference, int size);
    Task<UserModel?> GetUserByIdAsync(int userId);
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task AddUserAsync(UserModel user);
    Task UpdateUserAsync(UserModel user);
    Task DeleteUserAsync(int userId);
}