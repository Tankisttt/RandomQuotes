namespace RandomQuotes.Contract
{
    /// <summary>
    /// Quote data access model using in database
    /// </summary>
    public class Quote
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