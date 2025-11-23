using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace BCT.DataAccess.DataRepositories;

public class User
{
    private readonly BookingSystemDbContext _dbContext;
    public User(BookingSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsUserExist(int userId)
    {
        var user = _dbContext.Users.Find(userId);
        return user != null;
    }

    public UserModel? GetUserById(int userId)
    {
        var user = _dbContext.Users.Find(userId);
        return user;
    }

    public UserModel GetUserByToken(UserModel user)
    {
        var existingUser = _dbContext.Users
            .FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        return existingUser;
    }

    public async Task<List<UserModel>> GetUsersAsync(UserModel user)
    {
        var users = await _dbContext.Users
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

    public UserModel CreateUser(UserModel user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public bool UpdateUser(UserModel user)
    {
        var existingUser = _dbContext.Users.Find(user.UserId);
        if (existingUser == null)
        {
            return false;
        }
        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.Role = user.Role;
        var result = _dbContext.SaveChanges();
        return result > 0;
    }

    public bool DeleteUser(int userId)
    {
        var user = _dbContext.Users.Find(userId);
        if (user == null)
        {
            return false;
        }
        _dbContext.Users.Remove(user);
        var result = _dbContext.SaveChanges();
        return result > 0;
    }
}
