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
            CreateMap<Article, AddNewArticleEvent>().ReverseMap();
        }
    }
}
