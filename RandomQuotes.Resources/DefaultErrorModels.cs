namespace RandomQuotes.Resources;

public static class DefaultErrorModels
{
    public static ErrorModel QuotesNotExist
        => ErrorModel.NotFound(ErrorModelKeys.QuotesNotExist, "Quotes not exist");

    public static ErrorModel QuotesNotFound
        => ErrorModel.NotFound(ErrorModelKeys.QuoteNotFound, "Quote not found");
        
    public static ErrorModel FileIsNotSupported
        => ErrorModel.Default(ErrorModelKeys.FileNotSupported, "File not supported for batch download");
    
    public static ErrorModel UserIsNotFound
        => ErrorModel.Default(ErrorModelKeys.UserNotFound, "User not found");
    
    public static ErrorModel UserAlreadyExists
        => ErrorModel.Default(ErrorModelKeys.UserAlreadyExists, "User already exists");
    
}