namespace Bookify.API.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
    }
}