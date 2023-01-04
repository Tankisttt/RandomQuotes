namespace RandomQuotes.Contract.User.Requests;

/// <summary>
/// Class encapsulates information for creating User
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// User nickname
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    public string Password { get; set; }
}