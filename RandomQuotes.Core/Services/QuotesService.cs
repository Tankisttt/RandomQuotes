using System.Collections.Generic;
using System.IO;
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

        /// <inheritdoc cref="IQuotesService.GetRandomQuote"/>
        public async Task<WriteResult<Quote>> GetRandomQuote()
        {
            var randomQuote = await _quotesRepository.GetRandomQuote();

            if (randomQuote == null || string.IsNullOrEmpty(randomQuote.Text))
                return WriteResult<Quote>.FromError(DefaultErrorModels.QuotesNotExist);

            return WriteResult<Quote>.FromValue(randomQuote);
        }

        /// <inheritdoc cref="IQuotesService.CreateQuote"/>
        public async Task<WriteResult<CreateQuoteResponse>> CreateQuote(CreateQuoteRequest request)
        {
            var response = await _quotesRepository.CreateQuote(request);
            return WriteResult<CreateQuoteResponse>.FromValue(response);
        }

        /// <inheritdoc cref="IQuotesService.BatchUpload"/>
        public async Task<WriteResult<BatchUploadResponse>> BatchUpload(StreamReader streamReader,
            string author = "Unknown author")
        {
            var quotes = new List<CreateQuoteRequest>();
            try
            {
                while (streamReader.Peek() >= 0)
                    quotes.Add(new()
                    {
                        Author = author,
                        Text = await streamReader.ReadLineAsync()
                    });
            }
            catch
            {
                if (quotes.Count == 0)
                    return WriteResult<BatchUploadResponse>.FromError(DefaultErrorModels.FileIsNotSupported);
            }

            await _quotesRepository.BatchUpload(quotes);
            return WriteResult<BatchUploadResponse>.FromValue(new() { Count = quotes.Count });
        }
    }
}