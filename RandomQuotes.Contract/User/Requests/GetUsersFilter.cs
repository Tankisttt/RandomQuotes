using RandomQuotes.Contract.Common;

namespace RandomQuotes.Contract.User.Requests;

/// <summary>
/// Class encapsulates filtering for GetUsers query
/// </summary>
public class GetUsersFilter : PagingSettings
{
    /// <inheritdoc cref="Requests.UserRoleFilter"/>
    public UserRoleFilter UserRoleFilter { get; set; } = UserRoleFilter.All;
}

/// <summary>
/// Filtering the selection of users by their UserRole
/// </summary>
public enum UserRoleFilter
{
    /// <summary>
    /// Get all users
    /// </summary>
    All = 0,

    /// <summary>
    /// Get only users with Admin UserRole
    /// </summary>
    Admin = 1,

    /// <summary>
    /// Get only users with User UserRole
    /// </summary>
    User = 2
}