using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.DataRepositories;

namespace BCT.BusinessRule.LogicServices;

public class UserService
{
    private readonly User _userRepository;

    public UserService(User userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsUserExist(int userId)
    {
        return _userRepository.IsUserExist(userId);
    }

    public UserModel? GetUserById(int userId)
    {
        return _userRepository.GetUserById(userId);
    }

    public async Task<List<UserModel>> GetUsersAsync(UserModel? user)
    {
        if (user == null)
        {
            return await _userRepository.GetUsersAsync(new UserModel());
        }
        return await _userRepository.GetUsersAsync(user);
    }

    public UserModel CreateUser(UserModel user)
    {
        return _userRepository.CreateUser(user);
    }

    public bool UpdateUser(UserModel user)
    {
        return _userRepository.UpdateUser(user);
    }

    public bool DeleteUser(int userId)
    {
        return _userRepository.DeleteUser(userId);
    }
}