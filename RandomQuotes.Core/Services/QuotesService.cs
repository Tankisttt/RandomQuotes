using System.Threading.Tasks;
using RandomQuotes.Abstractions.Repositories;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Resources;

namespace RandomQuotes.Core.Services
{
    /// <inheritdoc cref="IQuotesService"/>>
    public class QuotesService : IQuotesService
    {
        private readonly IQuotesRepository _quotesRepository;

        public QuotesService(IQuotesRepository quotesRepository)
        {
            _quotesRepository = quotesRepository;
        }

        /// <inheritdoc cref="IQuotesService.GetRandomQuote"/>>
        public async Task<WriteResult<Quote>> GetRandomQuote()
        {
            var randomQuote = await _quotesRepository.GetRandomQuote();

            if (randomQuote == null || string.IsNullOrEmpty(randomQuote.Text))
                return WriteResult<Quote>.FromError(DefaultErrorModels.QuotesNotExist);

            return WriteResult<Quote>.FromValue(randomQuote);
        }

        public async Task<WriteResult<CreateQuoteResponse>> CreateQuote(CreateQuoteRequest request)
        {
            var response = await _quotesRepository.CreateQuote(request);
            return WriteResult<CreateQuoteResponse>.FromValue(response);
        }
    }
}