using Microsoft.EntityFrameworkCore;
using WasteBinsAPI.Data.Contexts;
using WasteBinsAPI.Models;

namespace WasteBinsAPI.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _context.Set<UserModel>().ToListAsync();
    }

    public IEnumerable<UserModel> GetAllReference(int lastReference, int size)
    {
        return _context.Users
            .Where(user => user.UserId > lastReference)
            .OrderBy(user => user.UserId)
            .Take(size)
            .AsNoTracking()
            .ToList();
    }

    public async Task<UserModel?> GetUserByIdAsync(int userId)
    {
        return await _context.Set<UserModel>().FindAsync(userId);
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        return await _context.Set<UserModel>().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddUserAsync(UserModel user)
    {
        await _context.Set<UserModel>().AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        _context.Set<UserModel>().Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user != null)
        {
            _context.Set<UserModel>().Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}