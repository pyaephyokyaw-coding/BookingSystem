using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.DataRepositories;

namespace BCT.BusinessRule.LogicServices;

public class UserService(User userRepository)
{
    public bool IsUserExistByUserId(int userid)
    {
        return userRepository.IsUserExistByUserId(userid);
    }

    public bool IsUserExist(UserModel user)
    {
        return userRepository.IsUserExistByUserId(user.UserId);
    }

    public UserModel? GetUserById(int userId)
    {
        return userRepository.GetUserById(userId);
    }

    public async Task<List<UserModel>> GetUsersAsync(UserModel? user)
    {
        if (user == null)
        {
            return await userRepository.GetUsersAsync(new UserModel());
        }
        return await userRepository.GetUsersAsync(user);
    }

    public UserModel? CreateUser(UserModel user)
    {
        return userRepository.CreateUser(user);
    }

    public bool UpdateUser(UserModel user)
    {
        return userRepository.UpdateUser(user);
    }

    public bool DeleteUser(int userId)
    {
        return userRepository.DeleteUser(userId);
    }
}