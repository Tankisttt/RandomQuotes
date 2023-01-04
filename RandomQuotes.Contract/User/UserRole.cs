namespace RandomQuotes.Contract.User;

/// <summary>
/// Role (Type) of user 
/// </summary>
public enum UserRole
{
    /// <summary>
    /// User with Admin UserRole has the ability to manage users, create hosts, see all sessions
    /// </summary>
    Admin = 1,

    /// <summary>
    /// User with User UserRole has the ability to create sessions and invites guests
    /// </summary>
    User = 2
}