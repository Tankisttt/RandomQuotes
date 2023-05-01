namespace RandomQuotes.Abstractions.Models.User;

public class UserInfoModel
{
    /// <summary>
    /// User unique identifier
    /// </summary>
    public string Id { get; set; }

    public UserRoleModel UserRole { get; set; }
    
    /// <summary>
    /// User nickname
    /// </summary>
    public string NickName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string PasswordSalt { get; set; }

    public bool IsDeleted { get; set; }
}