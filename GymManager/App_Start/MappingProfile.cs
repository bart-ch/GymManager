using AutoMapper;
using GymManager.Core.Domain;
using GymManager.Dtos;

namespace GymManager.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<Area, AreaDto>();
            Mapper.CreateMap<Core.Domain.Type, TypeDto>();
            Mapper.CreateMap<Supplement, SupplementDto>();
            Mapper.CreateMap<Flavor, FlavorDto>();
            Mapper.CreateMap<SupplementType, SupplementTypeDto>();

            Mapper.CreateMap<EquipmentDto, Equipment>()
                .ForMember(e => e.Id, opt => opt.Ignore());
            Mapper.CreateMap<AreaDto, Area>();
            Mapper.CreateMap<TypeDto, Core.Domain.Type>();
            Mapper.CreateMap<SupplementDto, Supplement>()
                .ForMember(s => s.Id, opt => opt.Ignore());
            Mapper.CreateMap<FlavorDto, Flavor>()
                .ForMember(f => f.Id, opt => opt.Ignore()); ;
            Mapper.CreateMap<SupplementTypeDto, SupplementType>();

        }
    }
}