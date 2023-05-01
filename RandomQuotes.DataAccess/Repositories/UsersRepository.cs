using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Abstractions.Repositories;
using RandomQuotes.DataAccess.Models.User;

namespace RandomQuotes.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private const string CollectionName = "users";
    private readonly IMongoDatabase _database;
    private readonly IMapper _mapper;

    public UsersRepository(IMongoDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<UserInfoModel> GetUserByLogin(string login)
    {
        var user = await GetCollection().AsQueryable()
            .Where(x => x.Login == login).Sample(1)
            .FirstOrDefaultAsync();
        return _mapper.Map<UserInfoModel>(user);
    }

    public async Task Create(UserInfoModel userInfoModel)
    {
        var user = _mapper.Map<MongoUser>(userInfoModel);
        await GetCollection().InsertOneAsync(user);
    }

    private IMongoCollection<MongoUser> GetCollection() =>
        _database.GetCollection<MongoUser>(CollectionName);
}