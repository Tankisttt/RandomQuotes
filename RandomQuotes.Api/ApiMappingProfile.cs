﻿using AutoMapper;
using RandomQuotes.Contract;

namespace RandomQuotes.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateQuoteRequest, BusinessLogic.Models.CreateQuoteRequest>();
            
            CreateMap<BusinessLogic.Models.CreateQuoteResponse, CreateQuoteResponse>();
        }
    }
}