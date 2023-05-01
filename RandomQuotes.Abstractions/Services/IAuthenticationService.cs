using System.Threading.Tasks;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Resources;

namespace RandomQuotes.Abstractions.Services;

public interface IAuthenticationService
{
    Task<WriteResult<AuthenticationResponseModel>> LoginAsync(LoginRequestModel user);
}