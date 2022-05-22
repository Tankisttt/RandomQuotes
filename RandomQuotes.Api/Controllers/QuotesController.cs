using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Contract;

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
        [ProducesResponseType(typeof(Quote), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRandomQuote() =>
            RenderResult<Quote>(await _quotesService.GetRandomQuote());

        [HttpPost("Add")]
        [ProducesResponseType(typeof(CreateQuoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddQuote([FromBody] CreateQuoteRequest request)
        {
            var createQuoteRequest = Mapper.Map<Abstractions.Models.CreateQuoteRequest>(request);
            return RenderResult<CreateQuoteResponse>(await _quotesService.CreateQuote(createQuoteRequest));
        }

        [HttpPost("BatchUpload")]
        [ProducesResponseType(typeof(BatchUploadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> BatchUpload(IFormFile file, [FromQuery] string author)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            using var reader = new StreamReader(file.OpenReadStream());
            var downloadResult = await _quotesService.BatchUpload(reader, author);
            
            return RenderResult<BatchUploadResponse>(downloadResult);
        }
    }
}