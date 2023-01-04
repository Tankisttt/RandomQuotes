namespace RandomQuotes.DataAccess.Models.User;

/// <summary>
/// Type (Role) of user 
/// </summary>
public enum UserRole
{
    /// <summary>
    /// User with Admin UserRole has the ability to manage users, create users, delete all quotes and users
    /// </summary>
    Admin = 1,

    /// <summary>
    /// User with User UserRole has the ability to add, get quotes, delete himself quotes
    /// </summary>
    User = 2
}