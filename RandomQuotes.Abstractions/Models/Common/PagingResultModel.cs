using System.Collections.Generic;

namespace RandomQuotes.Abstractions.Models.Common;

/// <summary>
///     Paging Result
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagingResultModel<T>
{
    /// <summary>
    ///     Result Items
    /// </summary>
    public List<T> Items { get; set; }

    /// <summary>
    ///     Total items count
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    ///     Empty result
    /// </summary>
    public static PagingResultModel<T> Empty => new() { Items = new List<T>(0), Total = 0 };
}