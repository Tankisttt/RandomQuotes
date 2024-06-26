﻿namespace RandomQuotes.Abstractions.Models
{
    public class CreateQuoteRequest
    {
        /// <summary>
        /// Quote text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Quote author
        /// </summary>
        public string Author { get; set; }
    }
}