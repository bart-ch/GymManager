using AutoMapper;
using GymManager.Core.Domain;
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
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<Area, AreaDto>();
            Mapper.CreateMap<Core.Domain.Type, TypeDto>();

            Mapper.CreateMap<EquipmentDto, Equipment>()
                .ForMember(e => e.Id, opt => opt.Ignore());
            Mapper.CreateMap<AreaDto, Area>();
            Mapper.CreateMap<TypeDto, Core.Domain.Type>();

        }
    }
}