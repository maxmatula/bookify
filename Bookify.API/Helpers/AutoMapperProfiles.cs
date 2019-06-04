using AutoMapper;
using Bookify.API.DTO.Equipment.Return;
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
        }
    }
}