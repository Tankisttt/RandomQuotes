using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Abstractions.Repositories;
using Quote = RandomQuotes.DataAccess.Models.Quote;

namespace RandomQuotes.DataAccess.Repositories;

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
    public async Task<RandomQuotes.Abstractions.Models.Quote> GetRandomQuote()
    {
        var quote = await GetCollection().AsQueryable().Sample(1)
            .FirstOrDefaultAsync();
        return _mapper.Map<RandomQuotes.Abstractions.Models.Quote>(quote);
    }

    /// <inheritdoc cref="IQuotesRepository.CreateQuote"/>
    public async Task<CreateQuoteResponse> CreateQuote(CreateQuoteRequest request)
    {
        var quote = _mapper.Map<Quote>(request);
        await GetCollection().InsertOneAsync(quote);
        return _mapper.Map<CreateQuoteResponse>(quote);
    }

    /// <inheritdoc cref="IQuotesRepository.BatchUpload"/>
    public async Task BatchUpload(IEnumerable<CreateQuoteRequest> quotesRequest)
    {
        var quotes = _mapper.Map<IEnumerable<Quote>>(quotesRequest);
        await GetCollection().InsertManyAsync(quotes);
    }

    private IMongoCollection<Quote> GetCollection() =>
        _database.GetCollection<Quote>(CollectionName);
}