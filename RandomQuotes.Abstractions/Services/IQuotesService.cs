using System.IO;
using System.Threading.Tasks;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Resources;

namespace RandomQuotes.Abstractions.Services;

/// <summary>
/// Quotes service for manipulations with quotes
/// </summary>
public interface IQuotesService
{
    /// <summary>
    /// Get random quote
    /// </summary>
    /// <returns>Quote contract model</returns>
    Task<WriteResult<Quote>> GetRandomQuote();

    /// <summary>
    /// Create quote
    /// </summary>
    /// <param name="request">Request parameters</param>
    /// <returns>Create quote response</returns>
    Task<WriteResult<CreateQuoteResponse>> CreateQuote(CreateQuoteRequest request);
        
    /// <summary>
    /// Upload many quotes in the one text file
    /// </summary>
    /// <param name="streamReader">Quotes</param>
    /// <param name="author">Author of the quotes</param>
    /// <returns>BatchDownloadResponse contains count of upload quotes</returns>
    Task<WriteResult<BatchUploadResponse>> BatchUpload(StreamReader streamReader, string author);
}