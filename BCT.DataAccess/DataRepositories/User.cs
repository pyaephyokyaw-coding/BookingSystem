using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BCT.DataAccess.DataRepositories;

public class User(BookingSystemDbContext dbContext)
{
    public async Task<bool> IsUserExistByUserIdAsync(int userId)
    {
        var user = await dbContext.Users.FindAsync(userId);
        return user != null;
    }

    public async Task<bool> IsUserExistAsync(UserModel user)
    {
        var isUserExist = await dbContext.Users
            .AnyAsync(u =>
                u.UserId == user.UserId ||
                u.FullName == user.FullName ||
                u.Email == user.Email ||
                u.Phone == user.Phone ||
                u.Role == user.Role ||
                u.CreatedAt == user.CreatedAt
            );

        return isUserExist;
    }

    public async Task<UserModel?> GetUserByIdAsync(int userId)
    {
        var user = await dbContext.Users.FindAsync(userId);
        return user;
    }

    public async Task<bool> IsUserExistByEmailAsync(string email)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        return user != null;
    }

    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        var user = await dbContext.Users
        .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<List<UserModel>> GetUsersAsync(UserModel user)
    {
        var users = await dbContext.Users
            .Where(u =>
                u.UserId == user.UserId ||
                u.FullName == user.FullName ||
                u.Email == user.Email ||
                u.Phone == user.Phone ||
                u.Role == user.Role ||
                u.CreatedAt == user.CreatedAt
            )
            .ToListAsync();

        return users;
    }

    public async Task<UserModel?> CreateUserAsync(UserModel user)
    {
        var hasData = await IsUserExistByEmailAsync(user.Email);
        if (hasData)
        {
            throw new Exception("User with the same email already exists.");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = hashedPassword;

        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return user;
    }

    public async Task<bool> UpdateUserAsync(UserModel user)
    {
        var existingUser = await dbContext.Users.FindAsync(user.UserId);
        if (existingUser == null)
        {
            return false;
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.Password = hashedPassword;
        existingUser.Role = user.Role;
        var result = dbContext.SaveChanges();
        return result > 0;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await dbContext.Users.FindAsync(userId);
        if (user == null) return false;

        dbContext.Users.Remove(user);
        var result = dbContext.SaveChanges();
        return result > 0;
    }
}
