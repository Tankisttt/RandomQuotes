﻿using System.Threading.Tasks;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Resources;

namespace RandomQuotes.Abstractions.Services
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

        /// <summary>
        /// Create quote
        /// </summary>
        /// <param name="request">Request parameters</param>
        /// <returns>Create quote response</returns>
        Task<WriteResult<CreateQuoteResponse>> CreateQuote(CreateQuoteRequest request);
    }
}