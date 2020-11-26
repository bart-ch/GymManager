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
            Mapper.CreateMap<Area, AreaDto>().ReverseMap();
            Mapper.CreateMap<Core.Domain.Type, TypeDto>().ReverseMap();
            Mapper.CreateMap<Supplement, SupplementDto>();
            Mapper.CreateMap<Flavor, FlavorDto>();
            Mapper.CreateMap<SupplementType, SupplementTypeDto>().ReverseMap();
            Mapper.CreateMap<Malfunction, MalfunctionDto>();
            Mapper.CreateMap<EquipmentOrder, EquipmentOrderDto>();
            Mapper.CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            Mapper.CreateMap<EquipmentDto, Equipment>()
                .ForMember(e => e.Id, opt => opt.Ignore());
            Mapper.CreateMap<SupplementDto, Supplement>()
                .ForMember(s => s.Id, opt => opt.Ignore());
            Mapper.CreateMap<FlavorDto, Flavor>()
                .ForMember(f => f.Id, opt => opt.Ignore());
            Mapper.CreateMap<MalfunctionDto, Malfunction>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<EquipmentOrderDto, EquipmentOrder>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}