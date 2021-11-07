using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public QuotesRepository(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IQuotesRepository.GetRandomQuote"/>
        public async Task<Quote> GetRandomQuote()
            => await GetCollection().AsQueryable().Sample(1)
                .FirstOrDefaultAsync();

        public async Task<CreateQuoteResponse> CreateQuote(CreateQuoteRequest request)
        {
            var quote = _mapper.Map<Quote>(request);
            await GetCollection().InsertOneAsync(quote);
            return _mapper.Map<CreateQuoteResponse>(quote);
        }

        private IMongoCollection<Quote> GetCollection() => _database.GetCollection<Quote>(CollectionName);
    }
}