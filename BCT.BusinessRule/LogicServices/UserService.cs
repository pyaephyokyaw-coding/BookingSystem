using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.DataRepositories;
using System.Threading.Tasks;

namespace BCT.BusinessRule.LogicServices;

public class UserService(User userRepository, Booking bookingRepository)
{
    public async Task<bool> IsUserExistByUserIdAsync(int userid)
    {
        return await userRepository.IsUserExistByUserIdAsync(userid);
    }

    public async Task<bool> IsUserExistAsync(UserModel user)
    {
        return await userRepository.IsUserExistByUserIdAsync(user.UserId);
    }

    public async Task<UserModel?> GetUserByIdAsync(int userId)
    {
        return await userRepository.GetUserByIdAsync(userId);
    }

    public async Task<List<UserModel>> GetUsersAsync(UserModel? user)
    {
        if (user == null)
        {
            return await userRepository.GetUsersAsync(new UserModel());
        }
        return await userRepository.GetUsersAsync(user);
    }

    public async Task<UserModel?> CreateUserAsync(UserModel user)
    {
        return await userRepository.CreateUserAsync(user);
    }

    public async Task<bool> UpdateUserAsync(UserModel user)
    {
        return await userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        await bookingRepository.DeleteAsync(userId);

        return await userRepository.DeleteUserAsync(userId);
    }
}