using System.Threading.Tasks;
using RandomQuotes.BusinessLogic.Interfaces;
using RandomQuotes.Contract;
using RandomQuotes.DataAccess.Interfaces;
using RandomQuotes.Resources;

namespace RandomQuotes.BusinessLogic.Services
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

            return WriteResult<Quote>.FromValue(new Quote { Text = randomQuote.Text, Author = randomQuote.Author });
        }
    }
}