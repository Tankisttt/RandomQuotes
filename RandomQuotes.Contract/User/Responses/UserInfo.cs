namespace RandomQuotes.Contract.User.Responses;

/// <summary>
/// Class encapsulates information about user
/// </summary>
public class UserInfo
{
    /// <summary>
    /// User unique identifier
    /// </summary>
    public string Id { get; set; }

    /// <inheritdoc cref="User.UserRole"/>
    public UserRole UserRole { get; set; }

    /// <summary>
    /// User nickname
    /// </summary>
    public string NickName { get; set; }
}