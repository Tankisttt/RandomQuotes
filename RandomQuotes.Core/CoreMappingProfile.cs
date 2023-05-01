using AutoMapper;
using RandomQuotes.Abstractions.Models.User;

namespace RandomQuotes.Core;

public class CoreMappingProfile : Profile
{
    public CoreMappingProfile()
    {
        CreateMap<CreateUserRequestModel, UserInfoModel>();
    }
}