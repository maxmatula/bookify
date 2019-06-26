using AutoMapper;
using Bookify.API.DTO.Client;
using Bookify.API.DTO.Equipment.Return;
using Bookify.API.DTO.House;
using Bookify.API.Models;

namespace Bookify.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Equipment, EquipmentDto>();
            CreateMap<EquipmentDto, Equipment>();
            CreateMap<EquipmentForCreateDto, Equipment>();
            CreateMap<EquipmentForEditDto, Equipment>();

            CreateMap<Client, ClientDto>()
            .ForMember(c => c.Bookings, opt => opt.MapFrom(x => x.Bookings));
            CreateMap<ClientDto, Client>()
            .ForMember(c => c.Bookings, opt => opt.MapFrom(x => x.Bookings));
            CreateMap<ClientForCreateDto, Client>();
            CreateMap<ClientForEditDto, Client>();

            CreateMap<House, HouseDto>()
            .ForMember(m => m.Equipment, opt => opt.MapFrom(x => x.Equipment))
            .ForMember(m => m.Bookings, opt => opt.MapFrom(x => x.Bookings));
            CreateMap<HouseDto, House>()
            .ForMember(m => m.Equipment, opt => opt.MapFrom(x => x.Equipment))
            .ForMember(m => m.Bookings, opt => opt.MapFrom(x => x.Bookings));
            CreateMap<HouseForCreateDto, House>();
            CreateMap<HouseForEditDto, House>();
        }
    }
}