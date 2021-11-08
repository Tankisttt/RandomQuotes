using System.Threading.Tasks;
using AutoMapper;
using RandomQuotes.Core.Interfaces;
using RandomQuotes.Core.Models;
using RandomQuotes.DataAccess.Interfaces;
using RandomQuotes.Resources;

namespace RandomQuotes.Core.Services
{
    /// <inheritdoc cref="IQuotesService"/>>
    public class QuotesService : IQuotesService
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly IMapper _mapper;

        public QuotesService(IQuotesRepository quotesRepository, IMapper mapper)
        {
            _quotesRepository = quotesRepository;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IQuotesService.GetRandomQuote"/>>
        public async Task<WriteResult<Quote>> GetRandomQuote()
        {
            var randomQuote = await _quotesRepository.GetRandomQuote();

            if (randomQuote == null || string.IsNullOrEmpty(randomQuote.Text))
                return WriteResult<Quote>.FromError(DefaultErrorModels.QuotesNotExist);

            return WriteResult<Quote>.FromValue(new Quote { Text = randomQuote.Text, Author = randomQuote.Author });
        }

        public async Task<WriteResult<CreateQuoteResponse>> CreateQuote(CreateQuoteRequest request)
        {
            var createQuoteRequest = _mapper.Map<DataAccess.Models.CreateQuoteRequest>(request);
            var response = await _quotesRepository.CreateQuote(createQuoteRequest);

            return WriteResult<CreateQuoteResponse>.FromValue(_mapper.Map<CreateQuoteResponse>(response));
        }
    }
}