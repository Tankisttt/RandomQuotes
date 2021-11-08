using AutoMapper;
using RandomQuotes.Contract;

namespace RandomQuotes.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateQuoteRequest, Core.Models.CreateQuoteRequest>();
            
            CreateMap<Core.Models.CreateQuoteResponse, CreateQuoteResponse>();
        }
    }
}