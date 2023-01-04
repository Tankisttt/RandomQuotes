namespace RandomQuotes.Resources;

public static class DefaultErrorModels
{
    public static ErrorModel QuotesNotExist
        => ErrorModel.NotFound(ErrorModelKeys.QuotesNotExist, "Quotes not exist");

    public static ErrorModel QuotesNotFound
        => ErrorModel.NotFound(ErrorModelKeys.QuoteNotFound, "Quote not found");
        
    public static ErrorModel FileIsNotSupported
        => ErrorModel.Default(ErrorModelKeys.FileNotSupported, "File not supported for batch download");
}