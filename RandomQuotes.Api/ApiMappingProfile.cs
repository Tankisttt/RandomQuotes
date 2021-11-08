using AutoMapper;
using RandomQuotes.Contract;

namespace RandomQuotes.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateQuoteRequest, Abstractions.Models.CreateQuoteRequest>();
            
            CreateMap<Abstractions.Models.CreateQuoteResponse, CreateQuoteResponse>();
            
            CreateMap<Abstractions.Models.Quote, Quote>();
        }
    }
}