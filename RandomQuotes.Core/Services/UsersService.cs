using System.Threading.Tasks;
using AutoMapper;
using RandomQuotes.Abstractions.Models.Common;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Abstractions.Repositories;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Resources;

namespace RandomQuotes.Core.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<WriteResult<UserInfoModel>> GetUserById(string userId)
    {
        throw new System.NotImplementedException();
    }

    public async Task<WriteResult<UserInfoModel>> GetCurrentProfile()
    {
        throw new System.NotImplementedException();
    }

    public async Task<PagingResultModel<UserShortInfoModel>> GetUsers(GetUsersFilterModel getUsersFilter)
    {
        throw new System.NotImplementedException();
    }

    public async Task<WriteResult> DeleteUser(string userId)
    {
        throw new System.NotImplementedException();
    }

    public async Task<WriteResult> RegisterUser(CreateUserRequestModel createUserRequestModel)
    {
        var userByLogin = await _usersRepository.GetUserByLogin(createUserRequestModel.Login);

        if (userByLogin is not null)
            return WriteResult.FromError(DefaultErrorModels.UserAlreadyExists);

        var userToCreate = _mapper.Map<UserInfoModel>(createUserRequestModel);
        userToCreate.PasswordSalt = BCryptTools.CreatePasswordSalt();
        userToCreate.Password = BCryptTools.CreatePasswordHash(userToCreate.Password, userToCreate.PasswordSalt);

        await _usersRepository.Create(userToCreate);
        return WriteResult.Successful;
    }

    public async Task<WriteResult<UserInfoModel>> GetUserByLogin(string login)
    {
        var user = await _usersRepository.GetUserByLogin(login);
        return user is null
            ? WriteResult<UserInfoModel>.FromError(DefaultErrorModels.UserIsNotFound)
            : WriteResult<UserInfoModel>.FromValue(user);
    }

    public async Task<WriteResult> UpdateUser(string userId, UpdateUserRequestModel updateUserRequestModel)
    {
        throw new System.NotImplementedException();
    }

    public async Task<WriteResult> ChangePassword(string userId, ChangeUserPasswordRequestModel request)
    {
        throw new System.NotImplementedException();
    }
}