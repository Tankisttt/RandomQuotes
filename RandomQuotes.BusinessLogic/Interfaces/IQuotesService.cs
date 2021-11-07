using System.Threading.Tasks;
using RandomQuotes.Contract;
using RandomQuotes.Resources;

namespace RandomQuotes.BusinessLogic.Interfaces
{
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
    }
}