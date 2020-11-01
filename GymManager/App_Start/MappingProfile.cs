using AutoMapper;
using GymManager.Dtos;
using GymManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManager.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Area, AreaDto>();

            Mapper.CreateMap<AreaDto, Area>();
        }
    }
}