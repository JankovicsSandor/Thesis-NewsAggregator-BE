using AutoMapper;
using News.DataAccess.Database;
using News.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddNewArticleEvent, Article>().ForMember(dest => dest.Id, opt => opt.Ignore())
                                                    .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
