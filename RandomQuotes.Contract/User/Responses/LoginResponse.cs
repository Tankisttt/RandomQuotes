using System;

namespace RandomQuotes.Contract.User.Responses;

/// <summary>
/// User login response
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// User access token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Token expiration dateTime
    /// </summary>
    public DateTime ExpiresAt { get; set; }
}