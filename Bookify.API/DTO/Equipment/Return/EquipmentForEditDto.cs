using Bookify.API.DTO.House;

namespace Bookify.API.DTO.Equipment.Return
{
    public class EquipmentForEditDto
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public int HouseId { get; set; }
        public HouseDto House { get; set; }
    }
}