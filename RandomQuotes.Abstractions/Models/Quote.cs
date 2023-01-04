using System;

namespace RandomQuotes.Abstractions.Models;

/// <summary>
/// Quote data access model using in database
/// </summary>
public class Quote
{
    /// <summary>
    /// Quote text
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Quote author
    /// </summary>
    public string Author { get; set; }
        
    /// <summary>
    /// Created datetime
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }
}