using AutoMapper;
using RandomQuotes.BusinessLogic.Models;

namespace RandomQuotes.BusinessLogic
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