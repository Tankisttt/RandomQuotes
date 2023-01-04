namespace RandomQuotes.Abstractions;

public class QuotesDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string QuotesCollectionName { get; set; } = null!;
}