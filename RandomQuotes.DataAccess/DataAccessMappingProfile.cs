using System;
using AutoMapper;
using RandomQuotes.Abstractions.Models;

namespace RandomQuotes.DataAccess;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<Quote, DataAccess.Models.MongoQuote>();
            
        CreateMap<CreateQuoteRequest, DataAccess.Models.MongoQuote>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAtUtc, o => o.Ignore())
            .AfterMap((s, d) => d.CreatedAtUtc = DateTime.UtcNow);

        CreateMap<DataAccess.Models.MongoQuote, CreateQuoteResponse>();

        CreateMap<DataAccess.Models.MongoQuote, Quote>();
            
        CreateMap<Quote, DataAccess.Models.MongoQuote>()
            .ForMember(x => x.Id, o => o.Ignore());
    }
}