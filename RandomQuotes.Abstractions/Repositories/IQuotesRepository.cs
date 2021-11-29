using System.Collections.Generic;
using System.Threading.Tasks;
using RandomQuotes.Abstractions.Models;

namespace RandomQuotes.Abstractions.Repositories
{
    /// <summary>
    /// Get quotes from quotes collection
    /// </summary>
    public interface IQuotesRepository
    {
        /// <summary>
        /// Get random quote
        /// </summary>
        /// <returns></returns>
        Task<Quote> GetRandomQuote();
        
        /// <summary>
        /// Create quote
        /// </summary>
        /// <param name="request">Request parameters</param>
        /// <returns>Create quote response</returns>
        Task<CreateQuoteResponse> CreateQuote(CreateQuoteRequest request);

        /// <summary>
        /// Upload many quotes in one operation
        /// </summary>
        /// <param name="quotesRequest">Collection of quotes</param>
        Task BatchUpload(IEnumerable<CreateQuoteRequest> quotesRequest);
    }
}