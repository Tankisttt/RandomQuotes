using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#pragma warning disable CS8618

namespace RandomQuotes.DataAccess.Models.User;

[BsonIgnoreExtraElements]
public class MongoUser
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <inheritdoc cref="RandomQuotes.DataAccess.Models.User.UserRole"/>
    [BsonElement("user_role")]
    public UserRole UserRole { get; set; }

    [BsonElement("nick_name")]
    public string NickName { get; set; }

    [BsonElement("login")]
    public string Login { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("password_salt")]
    public string PasswordSalt { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; }
}