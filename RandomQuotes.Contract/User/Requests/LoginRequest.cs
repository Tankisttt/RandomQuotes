using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace RandomQuotes.Contract.User.Requests;

/// <summary>
/// Request to login
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// User login name
    /// </summary>
    [Required]
    public string Login { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [StringLength(maximumLength:12, MinimumLength = 4)]
    public string Password { get; set; }
}