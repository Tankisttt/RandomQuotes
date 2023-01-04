#pragma warning disable CS8618

namespace RandomQuotes.Abstractions.Models.User;

public class LoginRequestModel
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}