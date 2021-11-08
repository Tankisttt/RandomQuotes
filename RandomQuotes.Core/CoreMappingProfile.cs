using AutoMapper;
using RandomQuotes.Abstractions.Models;

namespace RandomQuotes.Core
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<CreateQuoteRequest, DataAccess.Models.Quote>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.CreatedAtUtc, o => o.Ignore());

            CreateMap<DataAccess.Models.Quote, CreateQuoteResponse>();

            CreateMap<DataAccess.Models.Quote, Quote>();
            CreateMap<Quote, DataAccess.Models.Quote>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}