using System;

namespace RandomQuotes.Abstractions.Models.User;

/// <summary>
/// User Authentication Response
/// </summary>
public class AuthenticationResponseModel
{
    /// <summary>
    /// User access token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// User role
    /// </summary>
    public UserRoleModel Role { get; set; }
    
    /// <summary>
    /// Token expiration dateTime
    /// </summary>
    public DateTime ExpiresAt { get; set; }
}