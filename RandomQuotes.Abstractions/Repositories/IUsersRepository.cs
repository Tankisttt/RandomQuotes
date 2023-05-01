using System.Threading.Tasks;
using RandomQuotes.Abstractions.Models.User;

namespace RandomQuotes.Abstractions.Repositories;

public interface IUsersRepository
{
    /// <summary>
    /// Get user by login
    /// </summary>
    Task<UserInfoModel> GetUserByLogin(string login);
    
    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="login"></param>
    Task Create(UserInfoModel login);
}