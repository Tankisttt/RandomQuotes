using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
// ReSharper disable ClassNeverInstantiated.Global

namespace RandomQuotes.DataAccess.Models;

/// <summary>
/// Quote data access model using in database
/// </summary>
[BsonIgnoreExtraElements]
public class MongoQuote
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Quote text
    /// </summary>
    [BsonElement("text")]
    public string Text { get; set; }

    /// <summary>
    /// Quote author
    /// </summary>
    [BsonElement("author")]
    public string Author { get; set; }
        
    [BsonElement("created_at")]
    public DateTime CreatedAtUtc { get; set; }
}