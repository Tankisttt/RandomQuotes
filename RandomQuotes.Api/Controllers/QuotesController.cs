using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomQuotes.BusinessLogic.Interfaces;
using Quote = RandomQuotes.Contract.Quote;

namespace RandomQuotes.Api.Controllers
{
    [Route("v{version}/[controller]")]
    public class QuotesController : VersionedControllerBase
    {
        private readonly IQuotesService _quotesService;

        public QuotesController(IQuotesService quotesService, IMapper mapper) : base(mapper)
        {
            _quotesService = quotesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Contract.Quote), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRandomQuote() =>
            RenderResult<Quote>(await _quotesService.GetRandomQuote());
    }
}