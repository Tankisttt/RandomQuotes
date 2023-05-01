using System.Threading.Tasks;
using RandomQuotes.Abstractions.Models.Common;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Resources;

namespace RandomQuotes.Abstractions.Services;

public interface IUsersService
{
    Task<WriteResult<UserInfoModel>> GetUserById(string userId);

    Task<PagingResultModel<UserShortInfoModel>> GetUsers(GetUsersFilterModel getUsersFilter);

    Task<WriteResult> DeleteUser(string userId);

    Task<WriteResult> RegisterUser(CreateUserRequestModel createUserRequestModel);

    Task<WriteResult<UserInfoModel>> GetUserByLogin(string login);

    Task<WriteResult> UpdateUser(string id, UpdateUserRequestModel updateUserRequestModel);

    Task<WriteResult> ChangePassword(string userId, ChangeUserPasswordRequestModel request);
}