using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using User.Shared.Models.Input;

namespace User.BussinessLogic.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateNewUserInputModel, User.Data.Database.User>();
        }
    }
}
