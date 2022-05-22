using System.Threading.Tasks;
using RandomQuotes.DataAccess.Models;

namespace RandomQuotes.DataAccess.Interfaces
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
    }
}