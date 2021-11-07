using System;
using AutoMapper;
using RandomQuotes.DataAccess.Models;

namespace RandomQuotes.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<CreateQuoteRequest, Quote>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Text, o => o.MapFrom(s => s.Text))
                .ForMember(x => x.Author, o => o.MapFrom(s => s.Author))
                .ForMember(x => x.CreatedAtUtc, o => o.Ignore())
                .AfterMap((_, d) => d.CreatedAtUtc = DateTime.UtcNow);

            CreateMap<Quote, CreateQuoteResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id));
        }
    }
}