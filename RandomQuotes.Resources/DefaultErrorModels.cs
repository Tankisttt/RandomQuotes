namespace RandomQuotes.Resources
{
    public static class DefaultErrorModels
    {
        public static ErrorModel QuotesNotExist
            => ErrorModel.NotFound(ErrorModelKeys.QuotesNotExist, "Quotes not exist");

        public static ErrorModel QuotesNotFound
            => ErrorModel.NotFound(ErrorModelKeys.QuoteNotFound, "Quote not found");
    }
}