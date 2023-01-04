using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Abstractions.Repositories;
using RandomQuotes.Core;
using RandomQuotes.Core.Services;
using RandomQuotes.Resources;

namespace RandomQuotes.CoreTests;

/// <summary>
/// Quotes service core unit tests
/// </summary>
public class QuotesServiceTests
{
    private readonly Fixture _fixture = new();
    private readonly IMapper _mapper;

    public QuotesServiceTests()
    {
        _mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(new CoreMappingProfile())));
    }

    [Fact]
    public void CoreMappingProfileTest()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public async Task GetRandomQuote_Successful()
    {
        // arrange
        var quotesRepositoryMock = new Mock<IQuotesRepository>();
        var quote = _fixture.Create<Quote>();

        quotesRepositoryMock.Setup(x => x.GetRandomQuote()).ReturnsAsync(quote);

        var quoteService = new QuotesService(quotesRepositoryMock.Object);

        // act
        var randomQuoteResponse = await quoteService.GetRandomQuote();
        // assert
        var randomQuote = randomQuoteResponse.ResultData;
        Assert.NotNull(randomQuoteResponse);
        Assert.True(randomQuoteResponse.IsSuccess);
        Assert.NotNull(randomQuote);
        Assert.Equal(quote.Text, randomQuote.Text);
        Assert.Equal(quote.Author, randomQuote.Author);
        Assert.Equal(quote.CreatedAtUtc, randomQuote.CreatedAtUtc);
    }
        
    [Fact]
    public async Task GetRandomQuote_NoQuotesInDatabase_ReturnsNotFound()
    {
        // arrange
        var quotesRepositoryMock = new Mock<IQuotesRepository>();

        quotesRepositoryMock.Setup(x => x.GetRandomQuote()).ReturnsAsync(default(Quote));

        var quoteService = new QuotesService(quotesRepositoryMock.Object);

        // act
        var randomQuoteResponse = await quoteService.GetRandomQuote();
        // assert
        Assert.NotNull(randomQuoteResponse);
        Assert.False(randomQuoteResponse.IsSuccess);
        Assert.Null(randomQuoteResponse.ResultData);
        Assert.NotNull(randomQuoteResponse.Error);
        Assert.Equal(DefaultErrorModels.QuotesNotExist.Key, randomQuoteResponse.Error.Key);
        Assert.Equal(DefaultErrorModels.QuotesNotExist.Kind, randomQuoteResponse.Error.Kind);
        Assert.Equal(DefaultErrorModels.QuotesNotExist.Message, randomQuoteResponse.Error.Message);
    }
}