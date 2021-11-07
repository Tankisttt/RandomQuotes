using System;
using MongoDB.Bson.Serialization.Attributes;

namespace RandomQuotes.DataAccess.Models
{
    public class CreateQuoteRequest
    {
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
}