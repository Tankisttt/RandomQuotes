using System.ComponentModel.DataAnnotations;

namespace RandomQuotes.Contract.User.Requests;

/// <summary>
/// Class encapsulates information for updating User
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// User nickname
    /// </summary>
    [Required]
    public string NickName { get; set; }
}