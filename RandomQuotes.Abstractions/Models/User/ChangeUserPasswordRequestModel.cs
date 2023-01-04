#pragma warning disable CS8618
namespace RandomQuotes.Abstractions.Models.User;

/// <summary>
/// Class encapsulates information for changing user password
/// </summary>
public class ChangeUserPasswordRequestModel
{
    /// <summary>
    /// The old user password
    /// </summary>
    public string OldPassword { get; set; }

    /// <summary>
    /// The new user password
    /// </summary>
    public string NewPassword { get; set; }
}