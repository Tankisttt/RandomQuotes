using System.ComponentModel.DataAnnotations;

namespace RandomQuotes.Contract.User.Requests;

/// <summary>
/// Class encapsulates information for changing user password
/// </summary>
public class ChangeUserPasswordRequest
{
    /// <summary>
    /// The old user password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [StringLength(maximumLength:12, MinimumLength = 4)]
    public string OldPassword { get; set; }

    /// <summary>
    /// The new user password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [StringLength(maximumLength:12, MinimumLength = 4)]
    public string NewPassword { get; set; }
}