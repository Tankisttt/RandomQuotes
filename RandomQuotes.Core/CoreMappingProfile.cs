using AutoMapper;
using RandomQuotes.Core.Models;

namespace RandomQuotes.Core
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<CreateQuoteRequest, DataAccess.Models.CreateQuoteRequest>();
            
            CreateMap<DataAccess.Models.CreateQuoteResponse, CreateQuoteResponse>();
        }
    }
}