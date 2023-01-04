using RandomQuotes.Abstractions.Models.Common;

namespace RandomQuotes.Abstractions.Models.User;

/// <summary>
/// Class encapsulates filtering for GetUsers query
/// </summary>
public class GetUsersFilterModel : PagingSettingsModel
{
    public UserRoleFilter UserRoleFilter { get; set; }
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