using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BCT.DataAccess.DataRepositories;

public class User(BookingSystemDbContext dbContext)
{
    public bool IsUserExistByUserId(int userId)
    {
        var user = dbContext.Users.Find(userId);
        return user != null;
    }

    public bool IsUserExist(UserModel user)
    {
        var isUserExist = dbContext.Users
            .Any(u =>
                u.UserId == user.UserId ||
                u.FullName == user.FullName ||
                u.Email == user.Email ||
                u.Phone == user.Phone ||
                u.Role == user.Role ||
                u.CreatedAt == user.CreatedAt
            );

        return isUserExist;
    }

    public UserModel? GetUserById(int userId)
    {
        var user = dbContext.Users.Find(userId);
        return user;
    }

    public bool IsUserExistByEmail(string email)
    {
        var user = dbContext.Users
            .FirstOrDefault(u => u.Email == email);
        return user != null;
    }

    public UserModel? GetUserByEmail(string email)
    {
        var user = dbContext.Users
        .FirstOrDefault(u => u.Email == email);

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

    public UserModel? CreateUser(UserModel user)
    {
        var hasData = IsUserExistByEmail(user.Email);
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

    public bool UpdateUser(UserModel user)
    {
        var existingUser = dbContext.Users.Find(user.UserId);
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

    public bool DeleteUser(int userId)
    {
        var user = dbContext.Users.Find(userId);
        if (user == null)
        {
            return false;
        }
        dbContext.Users.Remove(user);
        var result = dbContext.SaveChanges();
        return result > 0;
    }
}
