using AutoMapper;
using RandomQuotes.DataAccess.Models;

namespace RandomQuotes.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Quote, Abstractions.Models.Quote>();
        }
    }
}