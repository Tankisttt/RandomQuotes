using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Resources;

namespace RandomQuotes.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthorizationSettings _authorizationSettings;
    private readonly IUsersService _usersService;

    public AuthenticationService(AuthorizationSettings authorizationSettings, IUsersService usersService)
    {
        _authorizationSettings = authorizationSettings;
        _usersService = usersService;
    }

    private const string UserIdClaim = "UserId";

    public async Task<WriteResult<AuthenticationResponseModel>> LoginAsync(LoginRequestModel user)
    {
        var existingUser = await _usersService.GetUserByLogin(user.Login);

        if (!existingUser.IsSuccess)
            return WriteResult<AuthenticationResponseModel>.FromError(existingUser.Error);

        if (!CheckPassword(existingUser.ResultData, user.Password))
            return WriteResult<AuthenticationResponseModel>.FromError(DefaultErrorModels.UserIsNotFound);

        var authenticationResponseModel = GenerateAuthenticationResponseForUser(existingUser.ResultData);
        return authenticationResponseModel is null
            ? WriteResult<AuthenticationResponseModel>.FromError(DefaultErrorModels.UserIsNotFound)
            : WriteResult<AuthenticationResponseModel>.FromValue(authenticationResponseModel);
    }

    private AuthenticationResponseModel GenerateAuthenticationResponseForUser(UserInfoModel user)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authorizationSettings.Secret);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, user.UserRole.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.Login),
            new(UserIdClaim, user.Id)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_authorizationSettings.TokenLifetime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);

        return new AuthenticationResponseModel
        {
            Token = jwtHandler.WriteToken(token),
            Role = user.UserRole,
            ExpiresAt = token.ValidTo
        };
    }

    private static bool CheckPassword(UserInfoModel existingUser, string userPassword)
    {
        var passwordHash = BCryptTools.CreatePasswordHash(userPassword, existingUser.PasswordSalt);
        return existingUser.Password == passwordHash;
    }
}