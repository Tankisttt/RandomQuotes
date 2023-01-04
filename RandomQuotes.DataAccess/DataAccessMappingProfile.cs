using System;
using AutoMapper;
using RandomQuotes.Abstractions.Models;

namespace RandomQuotes.DataAccess;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<Quote, DataAccess.Models.Quote>();
            
        CreateMap<CreateQuoteRequest, DataAccess.Models.Quote>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAtUtc, o => o.Ignore())
            .AfterMap((s, d) => d.CreatedAtUtc = DateTime.UtcNow);

        CreateMap<DataAccess.Models.Quote, CreateQuoteResponse>();

        CreateMap<DataAccess.Models.Quote, Quote>();
            
        CreateMap<Quote, DataAccess.Models.Quote>()
            .ForMember(x => x.Id, o => o.Ignore());
    }
}