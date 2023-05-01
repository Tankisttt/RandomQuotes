using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Contract.Common;
using RandomQuotes.Contract.User.Requests;
using RandomQuotes.Contract.User.Responses;
using RandomQuotes.Resources;

namespace RandomQuotes.Api.Controllers;

/// <summary>
/// Users controller
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]")]
public class UsersController : VersionedControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;
    private readonly IAuthenticationService _authenticationService;

    public UsersController(IMapper mapper, IUsersService usersService, IAuthenticationService authenticationService) :
        base(mapper)
    {
        _mapper = mapper;
        _usersService = usersService;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Get information about an user by unique user identifier 
    /// </summary>
    /// <param name="userId">Unique user identifier</param>
    /// <returns> User information </returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string userId) =>
        RenderResult<UserInfo>(await _usersService.GetUserById(userId));

    /// <summary>
    /// Get current user info
    /// </summary>
    /// <returns> User information </returns>
    [HttpGet("Profile")]
    [ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Profile() =>
        RenderResult<UserInfo>(await _usersService.GetUserById(GetUserId()));

    private string GetUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == Constants.UserIdClaim)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        return userId;
    }

    /// <summary>
    /// Get users list 
    /// </summary>
    /// <param name="getUsersFilter">Request filter for selecting users</param>
    /// <returns> List of users with their information </returns>
    [Authorize(Roles = Constants.AdminRole)]
    [HttpGet]
    [ProducesResponseType(typeof(PagingResult<UserShortInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<PagingResult<UserShortInfo>> GetUsers([FromQuery] GetUsersFilter getUsersFilter)
    {
        var getUsersFilterModel = _mapper.Map<GetUsersFilterModel>(getUsersFilter);
        var users = await _usersService.GetUsers(getUsersFilterModel);
        return _mapper.Map<PagingResult<UserShortInfo>>(users);
    }

    /// <summary>
    /// Deletes (archives) a user by a given user id.
    /// Please note, this action is irreversible! 
    /// </summary>
    /// <param name="userId">Unique user identifier</param>
    [Authorize(Roles = Constants.AdminRole)]
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId) =>
        RenderResult<WriteResult>(await _usersService.GetUserById(userId));

    /// <summary>
    /// Create (Register) new user with specified user information 
    /// </summary>
    /// <param name="createUserRequest">User information</param>
    [HttpPost("Register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest createUserRequest) =>
        RenderResult(await _usersService.RegisterUser(_mapper.Map<CreateUserRequestModel>(createUserRequest)));

    /// <summary>
    /// Try to login and get access token
    /// </summary>
    /// <param name="loginRequest">User information</param>
    [HttpPost("Login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var request = _mapper.Map<LoginRequestModel>(loginRequest);
        var result = await _authenticationService.LoginAsync(request);
        return RenderResult<LoginResponse>(result);
    }

    /// <summary>
    /// Update user information
    /// </summary>
    /// <param name="id">Unique user identifier</param>
    /// <param name="request">A new user information</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserRequest request) =>
        RenderResult(await _usersService.UpdateUser(id, _mapper.Map<UpdateUserRequestModel>(request)));

    /// <summary>
    /// Change user password
    /// </summary>
    /// <param name="userId">Unique user identifier</param>
    /// <param name="request">Contains old and new passwords</param>
    [HttpPost("ChangePassword/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromRoute] string userId, [FromBody] ChangeUserPasswordRequest request)
    {
        var changePasswordRequest = _mapper.Map<ChangeUserPasswordRequestModel>(request);
        return RenderResult(await _usersService.ChangePassword(userId, changePasswordRequest));
    }
}