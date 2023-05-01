using AutoMapper;
using RandomQuotes.Abstractions.Models.Common;
using RandomQuotes.Abstractions.Models.User;
using RandomQuotes.Contract;
using RandomQuotes.Contract.Common;
using RandomQuotes.Contract.User;
using RandomQuotes.Contract.User.Requests;
using RandomQuotes.Contract.User.Responses;

namespace RandomQuotes.Api;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        #region Quotes

        CreateMap<CreateQuoteRequest, Abstractions.Models.CreateQuoteRequest>();
        CreateMap<Abstractions.Models.CreateQuoteResponse, CreateQuoteResponse>();
        CreateMap<Abstractions.Models.Quote, Quote>();
        CreateMap<PagingResultModel<Abstractions.Models.Quote>, PagingResult<Quote>>();
        CreateMap<Abstractions.Models.BatchUploadResponse, BatchUploadResponse>();
        
        #endregion

        #region Users

        CreateMap<UserRole, UserRoleModel>().ReverseMap();
        CreateMap<ChangeUserPasswordRequest, ChangeUserPasswordRequestModel>();
        CreateMap<CreateUserRequest, CreateUserRequestModel>();
        CreateMap<GetUsersFilter, GetUsersFilterModel>();
        CreateMap<LoginRequest, LoginRequestModel>();
        CreateMap<UpdateUserRequest, UpdateUserRequestModel>();

        CreateMap<AuthenticationResponseModel, LoginResponse>();
        CreateMap<UserInfoModel, UserInfo>();
        CreateMap<UserShortInfoModel, UserShortInfo>();
        CreateMap<PagingResultModel<UserShortInfoModel>, PagingResult<UserShortInfo>>();

        #endregion

        CreateMap<PagingSettingsModel, PagingSettings>().ReverseMap();
    }
}