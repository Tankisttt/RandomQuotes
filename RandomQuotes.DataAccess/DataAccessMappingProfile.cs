using System;
using AutoMapper;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.DataAccess.Models.User;

namespace RandomQuotes.DataAccess;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<Quote, Models.MongoQuote>();
            
        CreateMap<CreateQuoteRequest, Models.MongoQuote>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAtUtc, o => o.Ignore())
            .AfterMap((_, d) => d.CreatedAtUtc = DateTime.UtcNow);

        CreateMap<Models.MongoQuote, CreateQuoteResponse>();

        CreateMap<Models.MongoQuote, Quote>();
            
        CreateMap<Quote, Models.MongoQuote>()
            .ForMember(x => x.Id, o => o.Ignore());

        #region Users

        CreateMap<UserRole, UserRoleModel>().ReverseMap();
        CreateMap<MongoUser, UserInfoModel>().ReverseMap();
        CreateMap<MongoUser, UserShortInfoModel>();

        #endregion
    }
}