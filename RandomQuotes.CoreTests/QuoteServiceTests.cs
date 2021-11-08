using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoFixture;
using AutoMapper;
using RandomQuotes.Core;
using RandomQuotes.Core.Services;
using RandomQuotes.DataAccess.Interfaces;

namespace RandomQuotes.CoreTests
{
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
        public async Task GetRandomQuote_Successful()
        {
            // arrange
            var quotesRepositoryMock = new Mock<IQuotesRepository>();
            var quote = _fixture.Create<DataAccess.Models.Quote>();

            quotesRepositoryMock.Setup(x => x.GetRandomQuote())
                .ReturnsAsync(quote);

            var quoteService = new QuotesService(quotesRepositoryMock.Object, _mapper);

            // act
            var randomQuoteResponse = await quoteService.GetRandomQuote();
            // assert
            var randomQuote = randomQuoteResponse.ResultData;
            Assert.NotNull(randomQuoteResponse);
            Assert.True(randomQuoteResponse.IsSuccess);
            Assert.NotNull(randomQuote);
            Assert.Equal("", randomQuote.Text);
            Assert.Equal("", randomQuote.Author);
            Assert.Equal(DateTime.UtcNow, randomQuote.CreatedAtUtc);
        }
    }
}