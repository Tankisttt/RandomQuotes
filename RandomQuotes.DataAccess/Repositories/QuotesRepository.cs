using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RandomQuotes.DataAccess.Interfaces;
using RandomQuotes.DataAccess.Models;

namespace RandomQuotes.DataAccess.Repositories
{
    /// <inheritdoc cref="IQuotesRepository"/>
    public class QuotesRepository : IQuotesRepository
    {
        private const string CollectionName = "Quotes";
        private readonly IMongoDatabase _database;

        public QuotesRepository(IMongoDatabase database)
        {
            _database = database;
        }

        /// <inheritdoc cref="IQuotesRepository.GetRandomQuote"/>
        public async Task<Quote> GetRandomQuote()
            => await _database.GetCollection<Quote>(CollectionName).AsQueryable().Sample(1)
                .FirstOrDefaultAsync();
    }
}