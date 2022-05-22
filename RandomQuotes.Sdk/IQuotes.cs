using System.Threading.Tasks;
using RandomQuotes.Contract;
using Refit;

namespace RandomQuotes.Sdk
{
    /// <summary>
    /// Provides manipulations with Quotes.
    /// </summary>
    public interface IQuotes
    {
        /// <summary>
        /// Get random quote
        /// </summary>
        /// <returns>Quote model</returns>
        [Get("/v1/Quotes")]
        Task<ApiResponse<Quote>> GetRandomQuote();

        /// <summary>
        /// Add one quote
        /// </summary>
        /// <param name="createQuoteRequest">Create quote request model</param>
        /// <returns>created quote response model</returns>
        [Post("/v1/Quotes/Add")]
        Task<ApiResponse<CreateQuoteResponse>> AddQuote([Body] CreateQuoteRequest createQuoteRequest);

        /// <summary>
        /// Upload many quotes contain in single text file into database
        /// </summary>
        /// <param name="stream">File with list of quotes</param>
        /// <param name="author">Quotes author</param>
        /// <returns>Batch upload quotes response</returns>
        [Multipart]
        [Post("/v1/Quotes/BatchUpload")]
        Task<ApiResponse<BatchUploadResponse>> BatchUpload([AliasAs("file")] StreamPart stream, [Query] string author);
    }
}